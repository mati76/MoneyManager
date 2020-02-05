using MoneyManager.Integrations.CSV;
using MoneyManager.Integrations.CSV.Model;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using TransactionsImporter.Model;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using TransactionsImporter.IO;
using System;

namespace TransactionsImporter
{
	public class AppService : IAppService
	{
		private readonly ICsvFileReader _reader;
		private List<Model.Transaction> _transactions = new List<Model.Transaction>();
		public AppService(ICsvFileReader reader)
		{
			_reader = reader;
		}

		public AppService() : this(new CsvFileReader()) { }

		public async Task ImportTransactionsAsync(string fileName, FileType fileType, Encoding encoding)
		{
			var uploaded = (await _reader.ReadFile(fileName, fileType, encoding)).Select(t => new Model.Transaction
			{
				Amount = t.Amount,
				Date = t.Date,
				Description = t.Description + " " + t.ExtraDescription,
				OriginalCategory = t.Category
			}).ToList();

			foreach (var t in uploaded)
			{
				var ruleMet = new MappingRules().FirstOrDefault(r => r.Predicate(t));
				if (t.Amount < 0 && ruleMet != null)
					t.Category = ruleMet.Category;
			}

			_transactions.AddRange(uploaded);
		}

		public IEnumerable<Model.Transaction> GetNotMatchedTransactions(string category, bool onlyExpences) => 
			_transactions.Where(t => t.Category == null)
				.Where(t => t.OriginalCategory == category || string.IsNullOrEmpty(category))
				.Where(t => onlyExpences && t.Amount < 0 || !onlyExpences)
				.OrderByDescending(t => t.Date);

		public IEnumerable<CategoryTotals> GetCategoryTotals() => _transactions.Where(t => t.Category != null).GroupBy(tt => tt.Category, (key, g)
			=> new CategoryTotals
			{
				Category = key,
				Amount = g.Sum(t => t.Amount),
				Count = g.Count()
			}).OrderBy(t => t.Amount);

		public IEnumerable<string> GetUnmatchedCatgories(bool onlyExpences)
		{
			return _transactions.Where(t => t.Category == null)
				.Where(t => onlyExpences && t.Amount < 0 || !onlyExpences)
				.GroupBy(t => t.OriginalCategory).Select(g => g.Key)
				.OrderBy(g => g);
		}

		public void DeleteSelected()
		{
			_transactions.RemoveAll(t => t.IsSelected);
		}

		public IEnumerable<Model.Transaction> GetTransactionsByCategory(Category category)
		{
			return _transactions.Where(t => t.Category == category).OrderByDescending(t => t.Date);
		}

		public IEnumerable<Category> GetAllCategories()
		{
			return typeof(Category)
			 .GetFields(BindingFlags.Public | BindingFlags.Static)
			 .Where(f => f.FieldType == typeof(Category))
			 .Select(f => (Category)f.GetValue(null));
		}

		public void ClearTransactions()
		{
			_transactions.Clear();
		}

		public async Task LoadTransactionsAsync(string path)
		{
			_transactions = new List<Model.Transaction>();
			using (var sr = new StreamReader(path))
			{
				while(!sr.EndOfStream)
				{
					// skip header
					sr.ReadLine();

					var transaction = new Model.Transaction();
					var fields = new Fields();
					var line = (await sr.ReadLineAsync())?.Split(';');
					if (line != null && line.Length >= fields.Count())
					{
						foreach (var field in fields)
							field.WriteTo(transaction, line[field.Index]);

						_transactions.Add(transaction);
					}
				}
			}
		}

		public async Task SaveTransactionsAsync(string path)
		{
			var sb = new StringBuilder();

			foreach (var field in new Fields().OrderBy(f => f.Index))
				sb.Append(field.Name).Append(';');

			sb.Append(Environment.NewLine);
			_transactions.ForEach(t =>
			{
				foreach(var field in new Fields().OrderBy(f => f.Index))
				{
					sb.Append(field.ReadFrom(t)).Append(';');
				}
				sb.Append(Environment.NewLine);
			});

			using (var sr = new StreamWriter(path, false, Encoding.UTF8))
			{
				await sr.WriteAsync(sb);
			}
		}
	}
}
