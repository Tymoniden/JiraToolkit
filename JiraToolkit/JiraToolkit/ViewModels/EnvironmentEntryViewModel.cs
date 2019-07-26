using System.Diagnostics;
using Shuriken;

namespace JiraToolkit.ViewModels
{
    internal class EnvironmentEntryViewModel : ObservableObject
    {
        public EnvironmentEntryViewModel() => OpenTicketCommand = new Command(ExecuteOpenTicket, CanExecuteOpenTicket);

        public string Prefix { get; set; }

        public string Root { private get; set; }

        [Observable]
        public string TicketNumber { get; set; }

        [Observable]
        public Command OpenTicketCommand { get; }

        bool CanExecuteOpenTicket() => !string.IsNullOrWhiteSpace(TicketNumber);

        void ExecuteOpenTicket()
        {
            var process = new Process { StartInfo = new ProcessStartInfo($"{Root}browse/{Prefix}-{TicketNumber}") };
            process.Start();
        }
    }
}