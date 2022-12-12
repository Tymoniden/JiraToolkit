using System;
using System.Diagnostics;
using System.Windows.Input;

namespace JiraToolkit.Commands
{
    public class OpenTicketCommand : ICommand
    {
        readonly string _root;
        readonly string _prefix;

        public event EventHandler CanExecuteChanged;

        public OpenTicketCommand(string root, string prefix)
        {
            _root = root;
            _prefix = prefix;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is string ticketNumber)
            {
                return !string.IsNullOrWhiteSpace(ticketNumber);
            }

            return false;
        }

        public void Execute(object parameter)
        {
            if(parameter is string ticketNumber)
            {
                var process = new Process { StartInfo = new ProcessStartInfo($"{_root}browse/{_prefix}-{ticketNumber}") };
                process.Start();
            }
        }
    }
}
