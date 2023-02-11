using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace JiraToolkit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    internal partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        static void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var result = MessageBox.Show(
                $"Do you want to create a bug report?{Environment.NewLine}This will open the log file and a browser to create a bug ticket.",
                "An unknown error occurred. The application shuts down",
                MessageBoxButton.YesNo,
                MessageBoxImage.Error);

            if (result == MessageBoxResult.Yes)
            {
                Process.Start($"{AppDomain.CurrentDomain.BaseDirectory}/jtk.log");
                Process.Start("https://github.com/Tymoniden/JiraToolkit/issues/new");
            }

            Environment.Exit(-1);
        }
    }
}
