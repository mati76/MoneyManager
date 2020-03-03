using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using TransactionsImporter.Model;
using System;

namespace TransactionsImporter.Windows.EditTransaction
{
	public class EditTransactionViewModel : ModelBase
	{
		private readonly IAppService _appService;
		private ICommand _closeCommand;
		private ICommand _saveTransactionCommand;

		public EditTransactionViewModel(Transaction transaction, IAppService appService) : this(transaction, appService, new EditTransactionWindow()) { }
		public EditTransactionViewModel(Transaction transaction, IAppService appService, IView view) : base(view)
		{
			_appService = appService;
			Transaction = transaction;
			view.SetViewModel(this);
		}

		public IEnumerable<Model.Category> Categories => _appService.GetAllCategories();

		public Transaction Transaction { get; }

		public ICommand CloseCommand
		{
			get
			{
				if (_closeCommand == null)
				{
					_closeCommand = new Command(
						(o) => _view.GetWindow().Close(),
						() => true);
				}
				return _closeCommand;
			}
		}

		public ICommand SaveTransactionCommand
		{
			get
			{
				if (_saveTransactionCommand == null)
				{
					_saveTransactionCommand = new Command(
						(t) =>
						{
							_appService.SaveTransaction(Transaction);
							_view.GetWindow().Close();
						}, () => IsFormValid());
				}
				return _saveTransactionCommand;
			}
		}

		private bool IsFormValid()
		{
			return Transaction.Amount != 0 && Transaction.Category != null && Transaction.Date <= DateTime.Now;
		}
	}
}
