using Microsoft.Win32;
using TransactionsImporter.Model;

namespace TransactionsImporter
{
	public class WindowService : IWindowService
	{
		public bool OpenFileDialog(out string fileName)
		{
			fileName = string.Empty;
			var dialog = new OpenFileDialog { Title = "Save transactions to .csv file", Filter = "csv files (*.csv)|*.csv" };
			if (dialog.ShowDialog() == true)
			{
				fileName = dialog.FileName;
				return true;
			}
			return false;
		}

		public bool SaveFileDialog(out string fileName)
		{
			fileName = string.Empty;
			var dialog = new OpenFileDialog { Title = "Pick transactions .csv file", Filter = "csv files (*.csv)|*.csv" };
			if (dialog.ShowDialog() == true)
			{ 
				fileName = dialog.FileName;
				return true;
			}
			return false;
		}

		public void ShowModal(ModelBase viewModel)
		{
			viewModel.View.GetWindow().ShowDialog();
		}
	}
}
