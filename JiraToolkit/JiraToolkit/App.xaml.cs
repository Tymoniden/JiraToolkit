using System;
using System.Windows;
using Shuriken.Monitoring;

namespace JiraToolkit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();

            var applicationMonitorScope = new ApplicationMonitorScope(new WpfNotificationContext(app.Dispatcher));
            try
            {
                app.Run();
            }
            finally
            {
                applicationMonitorScope.Dispose().GetAwaiter().GetResult();
            }
        }
    }
}
