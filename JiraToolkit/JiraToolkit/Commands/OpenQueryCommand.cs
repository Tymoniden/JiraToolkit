using System;
using System.Diagnostics;
using System.Windows.Input;

namespace JiraToolkit.Commands
{
    public class OpenQueryCommand : ICommand
    {
        private readonly string _url;

        public event EventHandler CanExecuteChanged;

        public OpenQueryCommand(string url)
        {
            _url = url;
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            if (parameter is string param)
            {
                var query = _url?.Replace("{{param}}", param);
                if (query == null)
                {
                    throw new InvalidOperationException();
                }

                var process = new Process { StartInfo = new ProcessStartInfo(query) };
                process.Start();
            }
        }
    }
}
