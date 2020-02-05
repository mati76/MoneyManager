using MoneyManager.Integrations.CSV.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Transaction = MoneyManager.Integrations.CSV.Model.Transaction;

namespace MoneyManager.Integrations.CSV
{
	public class CsvFileReader : ICsvFileReader
	{
		public async Task<IList<Transaction>> ReadFile(string path, FileType type)
		{
			return await ReadFile(path, type, Encoding.UTF8);
		}
		public async Task<IList<Transaction>> ReadFile(string path, FileType type, Encoding encoding)
		{
			var lineNumber = 1;
			var transactions = new List<Transaction>();

			using (var reader = new StreamReader(path, encoding))
			{
				while (!reader.EndOfStream)
				{
					var line = await reader.ReadLineAsync();
					if (type.Settings.DetectFirstLine != null)
					{
						if (type.Settings.DetectFirstLine(line))
						{
							type.Settings.FirstLine = ++lineNumber;
							continue;
						}
					}

					if (type.Settings.FirstLine.HasValue)
						ReadLine(new InputLine(lineNumber, ConvertToUnicode(line, encoding)), type, transactions);

					lineNumber++;
				}
			}

			return transactions;
		}

		private void ReadLine(InputLine line, FileType type, IList<Transaction> transactions)
		{
			if (string.IsNullOrWhiteSpace(line.Text) || line.LineNumber < type.Settings.FirstLine)
				return;

			var items = line.Text.Split(type.Settings.Delimiter);
			transactions.Add(type.ReaderImplementation.ParseLine(items));
		}

		private string ConvertToUnicode(string text, Encoding originalEncoding)
		{
			if (originalEncoding == Encoding.UTF8)
				return text;

			var utf8Bytes = Encoding.Convert(originalEncoding, Encoding.UTF8, originalEncoding.GetBytes(text));
			return Encoding.UTF8.GetString(utf8Bytes);
		}
	}
}
