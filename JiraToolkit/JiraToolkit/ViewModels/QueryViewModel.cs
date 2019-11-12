using System;
using System.Diagnostics;

namespace JiraToolkit.ViewModels
{
    internal class QueryViewModel : BaseViewModel
    {
        readonly OptionsViewModel _options;
        
        string _parameter;

        public QueryViewModel(OptionsViewModel options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));

            OpenQueryCommand = new Command(ExecuteOpenQueryCommand, CanExecuteOpenQueryCommand);
        }

        public string Name { get; set; }

        public string Url { private get; set; }

        public string Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value;
                NotifyPropertyChanged();
            }
        }

        public Command OpenQueryCommand { get; }

        bool CanExecuteOpenQueryCommand() => !string.IsNullOrWhiteSpace(Parameter) && !string.IsNullOrWhiteSpace(Url);

        void ExecuteOpenQueryCommand()
        {
            var query = Url?.Replace("{{param}}", Parameter);
            if (query == null)
            {
                throw new InvalidOperationException();
            }

            using (Process.Start(query))
            {
            }

            if (_options.ClearInputFieldAfterEnter)
            {
                Parameter = string.Empty;
            }
        }
    }
}