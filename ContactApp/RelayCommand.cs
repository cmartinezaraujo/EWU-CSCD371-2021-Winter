using System;
using System.Windows.Input;

namespace ContactApp
{
    public class RelayCommand : ICommand
    {
        private Action ExecuteDelegate { get; }
        private Func<bool> CanExecuteDelegate { get; }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => CanExecuteDelegate.Invoke();
        public void Execute(object parameter) => ExecuteDelegate.Invoke();
        public RelayCommand(Action executeDelegate, Func<bool> canExecuteDelegate)
        {
            ExecuteDelegate = executeDelegate ?? throw new ArgumentNullException(nameof(executeDelegate));
            CanExecuteDelegate = canExecuteDelegate ?? throw new ArgumentNullException(nameof(canExecuteDelegate));
        }
    }

}
