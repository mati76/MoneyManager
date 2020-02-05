using System.Windows;
using TransactionsImporter.Model;

namespace TransactionsImporter
{
	public interface IView
	{
		void SetViewModel(ModelBase viewModel);
		Window GetWindow();
	}
	public partial class TransactionsWindow : Window, IView
	{
		public TransactionsWindow()
		{
			InitializeComponent();
		}

		public void SetViewModel(ModelBase viewModel)
		{
			this.DataContext = viewModel;
		}

		public Window GetWindow()
		{
			return this;
		}
	}
}
