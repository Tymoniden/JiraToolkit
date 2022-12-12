using JiraToolkit.Commands;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace JiraToolkit.ViewModels
{
    internal class QueryViewModel : BaseViewModel
    {
        public QueryViewModel() => OpenQueryCommand = new OpenQueryCommand(string.Empty);

        public string Name 
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Url { get; set; }

        public string Parameter 
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        
        public ICommand OpenQueryCommand { get; }

        bool CanExecuteOpenQueryCommand() => !string.IsNullOrWhiteSpace(Parameter) && !string.IsNullOrWhiteSpace(Url);

        void ExecuteOpenQueryCommand()
        {
            var query = Url?.Replace("{{param}}", Parameter);
            if (query == null)
            {
                throw new InvalidOperationException();
            }

            var process = new Process { StartInfo = new ProcessStartInfo(query) };
            process.Start();
        }
    }
}