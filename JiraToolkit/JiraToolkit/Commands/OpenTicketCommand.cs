using JiraToolkit.ViewModels;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JiraToolkit.Commands
{
    public class OpenTicketCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public OpenTicketCommand()
        {

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
            var process = new Process { StartInfo = new ProcessStartInfo($"{Root}browse/{Prefix}-{TicketNumber}") };
            process.Start();
        }
    }
}
