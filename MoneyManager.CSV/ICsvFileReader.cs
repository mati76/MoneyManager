using MoneyManager.Integrations.CSV.Model;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Integrations.CSV
{
	public interface ICsvFileReader
	{
		Task<IList<Transaction>> ReadFile(string path, FileType type, Encoding encoding);
		Task<IList<Transaction>> ReadFile(string path, FileType type);
	}
}