using System;
using System.Diagnostics;
using Shuriken;

namespace JiraToolkit.ViewModels
{
    internal class QueryViewModel : ObservableObject
    {
        public QueryViewModel() => OpenQueryCommand = new Command(ExecuteOpenQueryCommand, CanExecuteOpenQueryCommand);

        [Observable]
        public string Name { get; set; }

        public string Url { private get; set; }

        [Observable]
        public string Parameter { get; set; }

        [Observable]
        public Command OpenQueryCommand { get; }

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