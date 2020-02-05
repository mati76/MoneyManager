using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace TransactionsImporter
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.Message);
			// Process unhandled exception

			// Prevent default unhandled exception processing
			e.Handled = true;
		}
	}
}
