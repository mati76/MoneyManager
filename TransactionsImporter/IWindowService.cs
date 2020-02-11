using TransactionsImporter.Model;

namespace TransactionsImporter
{
	public interface IWindowService
	{
		void ShowModal(ModelBase viewModel);

		bool OpenFileDialog(out string fileName);

		bool SaveFileDialog(out string fileName);
	}
}