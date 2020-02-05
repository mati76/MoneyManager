using System;
using System.Windows.Input;

namespace TransactionsImporter
{
    public class Command : ICommand
    {
        private readonly Func<bool> _canExecute;
        private readonly Action<object> _execute;

        public Command(Action<object> execute, Func<bool> canExecute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute()
        {
            return _canExecute();
        }

        public void Execute()
        {
            _execute(null);
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
