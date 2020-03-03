using System.Windows;
using TransactionsImporter.Model;

namespace TransactionsImporter.Windows
{
	public interface IView
	{
		void SetViewModel(ModelBase viewModel);
		Window GetWindow();
	}
}
