using System.Windows;
using TransactionsImporter.Model;

namespace TransactionsImporter.Windows.TransactionList
{
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
