using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TransactionsImporter.Model;

namespace TransactionsImporter.Windows.EditTransaction
{
	public partial class EditTransactionWindow : Window, IView
	{
		public EditTransactionWindow()
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

		private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = !IsTextAllowed(e.Text);
		}

		private static readonly string _regex = "[^0-9{0}-]+";
		private static bool IsTextAllowed(string text)
		{
			var regex = new Regex(string.Format(_regex, CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));

			return !regex.IsMatch(text);
		}
	}
}
