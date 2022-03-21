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

#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        public abstract void Execute(object parameter);
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    }
}
