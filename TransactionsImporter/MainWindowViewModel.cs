using Microsoft.Win32;
using MoneyManager.Integrations.CSV.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TransactionsImporter.Model;
using System;
using System.Windows;
using System.Text;
using System.Globalization;

namespace TransactionsImporter
{
    public class MainWindowViewModel : ModelBase
    {
        private readonly IWindowService _windowService;
        private readonly IAppService _appService;
        private ICommand _copyToClipboardCommand;
        private ICommand _openFileCommand;
        private ICommand _deleteTransactionCommand;
        private ICommand _onCheckCommand;
        private ICommand _showTransactionsCommand;
        private ICommand _editCategoryCommand;
        private ICommand _selectionChangedCommand;
        private ICommand _categoryChangedCommand;
        private ICommand _clearCommand;
        private ICommand _loadTransactionsCommand;
        private ICommand _saveTransactionsCommand;
        private bool _showOnlyExpenses;
        private bool isEditing;
        private string _selectedCategory;

        public MainWindowViewModel(IAppService appService, IWindowService windowService)
        {
            _windowService = windowService;
            _appService = appService;
        }

        public MainWindowViewModel() : this(new AppService(), new WindowService()) { }

        #region Commands
        public ICommand EditCategoryCommand
        {
            get
            {
                if (_editCategoryCommand == null)
                {
                    _editCategoryCommand = new Command(t => IsEditing = true, () => true);
                }
                return _editCategoryCommand;
            }
        }
        public ICommand LoadTransactionsCommand
        {
            get
            {
                if (_loadTransactionsCommand == null)
                {
                    _loadTransactionsCommand = new Command(async o =>
                    {
                        if (_windowService.OpenFileDialog(out string fileName))
                        {
                            try
                            {
                                await _appService.LoadTransactionsAsync(fileName);
                                Refresh();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.ToString());
                            }
                        }
                    },
                    () => true);
                }
                return _loadTransactionsCommand;
            }
        }
        public ICommand SaveTransactionsCommand
        {
            get
            {
                if (_saveTransactionsCommand == null)
                {
                    _saveTransactionsCommand = new Command(async o =>
                    {
                        if (_windowService.SaveFileDialog(out string fileName))
                        {
                            try
                            {
                                await _appService.SaveTransactionsAsync(fileName);
                            }
                            catch (AggregateException e)
                            {
                                MessageBox.Show(e.Message, e.InnerExceptions.First().StackTrace);
                            }
                        }
                    },
                    () => Totals.Any());

                }
                return _saveTransactionsCommand;
            }
        }
        public ICommand CopyToClipboardCommand
        {
            get
            {
                if (_copyToClipboardCommand == null)
                {
                    _copyToClipboardCommand = new Command(t => CopyTotalsToClipboard(), () => true);
                }
                return _copyToClipboardCommand;
            }
        }
        public ICommand SelectionChangedCommand
        {
            get
            {
                if (_selectionChangedCommand == null)
                {
                    _selectionChangedCommand = new Command(o => o = IsEditing = false, () => true);
                }
                return _selectionChangedCommand;
            }
        }
        public ICommand CategoryChangedCommand
        {
            get
            {
                if (_categoryChangedCommand == null)
                {
                    _categoryChangedCommand = new Command(o => Refresh(), () => true);
                }
                return _categoryChangedCommand;
            }
        }
        public ICommand OpenFileCommand
        {
            get
            {
                if (_openFileCommand == null)
                {
                    _openFileCommand = new Command(async (o) => await ExecuteOpenFileAsync(), () => true);
                }
                return _openFileCommand;
            }
        }

        public ICommand ShowTransactionsCommand
        {
            get
            {
                if (_showTransactionsCommand == null)
                {
                    _showTransactionsCommand = new Command((c) =>
                    {
                        _windowService.ShowModal(new TransactionsViewModel(((CategoryTotals)c).Category, _appService));
                        Refresh();
                    },
                    () => true);
                }
                return _showTransactionsCommand;
            }
        }
        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new Command(o =>
                    {
                        _appService.ClearTransactions();
                        Refresh();
                    },
                    () => _appService.GetCategoryTotals().Any());
                }
                return _clearCommand;
            }
        }
        public ICommand DeleteTransactionCommand
        {
            get
            {
                if (_deleteTransactionCommand == null)
                {
                    _deleteTransactionCommand = new Command(
                        (o) =>
                        {
                            _appService.DeleteSelected();
                            Refresh();
                        },
                        () => NotMatchedTransactions.Any(t => t.IsSelected));
                }
                return _deleteTransactionCommand;
            }
        }

        public ICommand OnCheckedCommand
        {
            get
            {
                if (_onCheckCommand == null)
                {
                    _onCheckCommand = new Command(
                        (o) => OnPropertyChanged("TransactionsSelected"),
                        () => true);
                }
                return _onCheckCommand;
            }
        }
        #endregion

        #region Properties
        public IEnumerable<Category> Categories => _appService.GetAllCategories();
        public int TransactionsSelected
        {
            get => _appService.GetNotMatchedTransactions(string.Empty, false).Count(t => t.IsSelected);
        }

        public bool IsAllSelected
        {
            get
            {
                var transactions = _appService.GetNotMatchedTransactions(string.Empty, false).ToList();
                return transactions.Any() && transactions.All(t => t.IsSelected);
            }
            set
            {
                _appService.GetNotMatchedTransactions(string.Empty, false).ToList().ForEach(t => t.IsSelected = value);
                OnPropertyChanged("TransactionsSelected");
            }
        }

        public IEnumerable<Model.Transaction> NotMatchedTransactions
        {
            get => _appService.GetNotMatchedTransactions(_selectedCategory, _showOnlyExpenses);
        }

        public IEnumerable<string> UnmatchedCategories
        {
            get => new string[] { string.Empty }.Concat(_appService.GetUnmatchedCatgories(_showOnlyExpenses));
        }
        public ObservableCollection<CategoryTotals> Totals
        {
            get => new ObservableCollection<CategoryTotals>(_appService.GetCategoryTotals());
        }

        public IEnumerable<Model.EncodingInfo> Encodings => Model.EncodingInfo.Encodings;

        public Model.EncodingInfo Encoding { get; set; } = Model.EncodingInfo.Encodings.FirstOrDefault(e => e.Name == "Windows 1250");

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("NotMatchedTransactions");
            }
        }

        public bool IsEditing
        {
            get => isEditing;
            set
            {
                isEditing = value;
                OnPropertyChanged("IsEditing");
            }
        }

        public FileType FileType { get; set; } = FileType.MbankCSV;

        public decimal TotalAmount => Totals.Sum(t => t.Amount);

        public decimal TransactionsCount => Totals.Sum(t => t.Count);

        public bool ShowOnlyExpences
        {
            get => _showOnlyExpenses;
            set
            {
                _showOnlyExpenses = value;
                OnPropertyChanged("Categories");
                OnPropertyChanged("NotMatchedTransactions");
            }
        }
        #endregion

        private async Task ExecuteOpenFileAsync()
        {
            if (_windowService.OpenFileDialog(out string fileName))
            {
                try
                {
                    await _appService.ImportTransactionsAsync(fileName, FileType, Encoding.Encoding);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                Refresh();
            }
        }
        
        private void Refresh()
        {
            OnPropertyChanged("NotMatchedTransactions");
            OnPropertyChanged("Totals");
            OnPropertyChanged("UnmatchedCategories");
            OnPropertyChanged("TransactionsSelected");
            OnPropertyChanged("TotalAmount");
            OnPropertyChanged("TransactionsCount");
        }

        private void CopyTotalsToClipboard()
        {
            var sb = new StringBuilder();
            foreach(var line in Totals)
            {
                sb.Append(line.Category.Name).Append('\t');
                sb.Append(line.Amount.ToString(CultureInfo.CurrentCulture)).Append('\t');
                sb.Append(line.Count).Append('\t');
                sb.Append(Environment.NewLine);
            }

            Clipboard.SetText(sb.ToString());
        }
    }
}
