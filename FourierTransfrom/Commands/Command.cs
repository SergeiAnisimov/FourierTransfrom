using FourierTransfrom.Commands.Base;
using System;

namespace FourierTransfrom.Commands
{
    internal class Command : BaseCommand
    {
        public Command(Action<object> execute,
                       Predicate<object> canExecute = null)
            : base(execute, canExecute)
        { }

        public override bool CanExecute(object parameter)
            => _canExecute == null ? true : _canExecute(parameter);

        public override void Execute(object parameter)
            =>_execute?.Invoke(parameter);
    }
}
