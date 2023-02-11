using JiraToolkit.Commands;
using System.Diagnostics;
using System.Windows.Input;

namespace JiraToolkit.ViewModels
{
    internal class EnvironmentEntryViewModel : BaseViewModel
    {

        public EnvironmentEntryViewModel(string root, string prefix)
        {
            Prefix = prefix;
            OpenTicketCommand = new OpenTicketCommand(root, prefix);
        }

        public string Prefix { get; set; }

        public string TicketNumber 
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public ICommand OpenTicketCommand { get; }
    }
}