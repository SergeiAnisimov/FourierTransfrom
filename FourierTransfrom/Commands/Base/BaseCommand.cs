using System;
using System.Windows.Input;

namespace FourierTransfrom.Commands.Base
{
    internal abstract class BaseCommand : ICommand
    {
        protected Predicate<object> _canExecute;

        protected Action<object> _execute;

        protected BaseCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }
}
