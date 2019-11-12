using System;
using System.Windows.Input;

namespace JiraToolkit.ViewModels
{
    public class Command : ICommand
    {
        private readonly Action<object> _executeCallback;
        private readonly Func<object, bool> _canExecuteCallback;

        public Command(Action executeCallback) : this(executeCallback, null)
        {
        }

        public Command(Action<object> executeCallback) : this(executeCallback, null)
        {
        }

        public Command(Action<object> executeCallback, Func<object, bool> canExecuteCallback)
        {
            _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
            _canExecuteCallback = canExecuteCallback; // Can execute is optional.
        }

        public Command(Action executeCallback, Func<bool> canExecuteCallback)
        {
            if (executeCallback is null) throw new ArgumentNullException(nameof(executeCallback));

            _executeCallback = (_) => executeCallback();

            if (canExecuteCallback != null) // Can execute is optional.
            {
                _canExecuteCallback = (_) => canExecuteCallback();
            }
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecuteCallback?.Invoke(parameter) == true;
        }

        public void Execute(object parameter)
        {
            _executeCallback.Invoke(parameter);
        }

        public void Execute()
        {
            Execute(null);
        }
    }
}
