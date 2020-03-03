using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using TransactionsImporter.Model;

namespace TransactionsImporter.Windows.TransactionList
{
	public class TransactionsViewModel : ModelBase
	{
		private readonly IAppService _appService;
		private ICommand _closeCommand;
		private ICommand _removeTransactionCommand;

		public TransactionsViewModel(Category category, IAppService appService) : this(category, appService, new TransactionsWindow()) { }
		public TransactionsViewModel(Category category, IAppService appService, IView view) : base(view)
		{
			_appService = appService;
			Category = category;
			view.SetViewModel(this);
		}

		public IEnumerable<Model.Transaction> Transactions => _appService.GetTransactionsByCategory(Category);

		public decimal TotalAmount => _appService.GetTransactionsByCategory(Category).Sum(t => t.Amount);

		public Category Category { get; }

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

		public ICommand RemoveTransactionCommand
		{
			get
			{
				if (_removeTransactionCommand == null)
				{
					_removeTransactionCommand = new Command(
						(t) =>
						{
							((Transaction)t).Category = null;
							OnPropertyChanged("Transactions");
						},
						() => true);
				}
				return _removeTransactionCommand;
			}
		}
	}
}
