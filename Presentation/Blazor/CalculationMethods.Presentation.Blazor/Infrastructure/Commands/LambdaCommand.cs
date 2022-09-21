using CalculationMethods.Presentation.Blazor.Infrastructure.Commands.Base;

namespace CalculationMethods.Presentation.Blazor.Infrastructure.Commands
{
    public class LambdaCommand : Command
    {
        private Func<object, bool> _canExecute;
        private Action<object> _execute;

        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        protected override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        protected override void Execute(object parameter) => _execute?.Invoke(parameter);
    }
}
