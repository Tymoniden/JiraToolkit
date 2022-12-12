using JiraToolkit.Commands;
using System.Diagnostics;
using System.Windows.Input;

namespace JiraToolkit.ViewModels
{
    internal class EnvironmentEntryViewModel : BaseViewModel
    {
        public EnvironmentEntryViewModel()
        {
            OpenTicketCommand = new OpenTicketCommand(string.Empty, string.Empty);
        }

        public string Prefix { get; set; }

        public string Root { private get; set; }

        public string TicketNumber 
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public ICommand OpenTicketCommand { get; }
    }
}