using MoneyManager.Integrations.CSV.Model;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransactionsImporter.Model;

namespace TransactionsImporter
{
	public interface IAppService
	{
		Task ImportTransactionsAsync(string fileName, FileType fileType, Encoding encoding);
		IEnumerable<Model.Transaction> GetNotMatchedTransactions(string category, bool onlyExpences);
		IEnumerable<Model.Transaction> GetTransactionsByCategory(Category category);
		IEnumerable<CategoryTotals> GetCategoryTotals();
		IEnumerable<string> GetUnmatchedCatgories(bool onlyExpences);
		IEnumerable<Category> GetAllCategories();
		void DeleteSelected();
		void ClearTransactions();
		Task LoadTransactionsAsync(string path);
		Task SaveTransactionsAsync(string path);
		void SplitTransaction(TransactionSplit transactionSplit);
	}
}