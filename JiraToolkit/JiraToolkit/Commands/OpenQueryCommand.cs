﻿using System;
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
            if(parameter is string param)
            {
                if (!string.IsNullOrEmpty(param))
                {
                    return true;
                }
            }

            return false;
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

                var process = new Process { StartInfo = new ProcessStartInfo(query) { UseShellExecute = true } };
                process.Start();
            }
        }
    }
}