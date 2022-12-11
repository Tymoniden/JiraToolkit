using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
//using log4net;

namespace JiraToolkit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    internal partial class App : Application
    {
        //static readonly ILog logger = LogManager.GetLogger(typeof(Application));

        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.DispatcherUnhandledException += App_DispatcherUnhandledException;
            app.InitializeComponent();

            app.Run();
        }

        static void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //logger.Fatal("An exception which terminates the application occurred", e.Exception);

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
