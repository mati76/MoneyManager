using MoneyManager.Integrations.CSV.Model;

namespace MoneyManager.Integrations.CSV.Reader
{
	public interface IReaderImplmentation
	{
		Transaction ParseLine(string[] line);
	}
}
