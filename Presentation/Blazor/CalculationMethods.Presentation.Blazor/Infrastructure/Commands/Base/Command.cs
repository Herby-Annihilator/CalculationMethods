using System.Windows.Input;

namespace CalculationMethods.Presentation.Blazor.Infrastructure.Commands.Base
{
    public abstract class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        bool ICommand.CanExecute(object? parameter) => CanExecute(parameter);
        void ICommand.Execute(object? parameter)
        {
            if (CanExecute(parameter))
                Execute(parameter);
        }

        protected abstract void Execute(object parameter);
        protected virtual bool CanExecute(object parameter) => true;
    }
}
