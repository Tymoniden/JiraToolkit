using System.Diagnostics;
using Shuriken;

namespace JiraToolkit.ViewModels
{
    internal class QueryViewModel : ObservableObject
    {
        public QueryViewModel() => OpenQueryCommand = new Command(ExecuteOpenQueryCommand,CanExecuteOpenQueryCommand);

        [Observable]
        public string Name { get; set; }

        [Observable]
        public string Url { get; set; }

        [Observable]
        public string Parameter { get; set; }

        [Observable]
        public Command OpenQueryCommand { get; }

        bool CanExecuteOpenQueryCommand() => !string.IsNullOrWhiteSpace(Parameter) && !string.IsNullOrWhiteSpace(Url);

        void ExecuteOpenQueryCommand()
        {
            var query = Url.Replace("{{param}}", Parameter);
            var process = new Process { StartInfo = new ProcessStartInfo(query)};
            process.Start();
        }
    }
}
