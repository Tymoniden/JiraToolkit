using System.Diagnostics;

namespace JiraToolkit.ViewModels
{
    internal class EnvironmentEntryViewModel : BaseViewModel
    {
        private readonly OptionsViewModel _options;
        private string _ticketNumber;

        public EnvironmentEntryViewModel(OptionsViewModel options)
        {
            _options = options ?? throw new System.ArgumentNullException(nameof(options));

            OpenTicketCommand = new Command(ExecuteOpenTicket, CanExecuteOpenTicket);
        }

        public string Prefix { get; set; }

        public string Root { private get; set; }

        public string TicketNumber
        {
            get => _ticketNumber;
            set
            {
                _ticketNumber = value;
                NotifyPropertyChanged();
            }
        }

        public Command OpenTicketCommand { get; }

        bool CanExecuteOpenTicket() => !string.IsNullOrWhiteSpace(TicketNumber);

        void ExecuteOpenTicket()
        {
            using (Process.Start($"{Root}browse/{Prefix}-{TicketNumber}"))
            {
            }

            if (_options.ClearInputFieldAfterEnter)
            {
                TicketNumber = string.Empty;
            }
        }
    }
}