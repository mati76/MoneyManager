using System.ComponentModel;

namespace TransactionsImporter.Model
{
	public abstract class ModelBase : INotifyPropertyChanged
	{
		public IView View => _view;
		public event PropertyChangedEventHandler PropertyChanged;

		protected readonly IView _view;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public ModelBase() { }
		public ModelBase(IView view)
		{
			_view = view;
		}
	}
}
