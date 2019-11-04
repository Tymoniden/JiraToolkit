using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using log4net;

namespace JiraToolkit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    internal partial class App : Application
    {
        static readonly ILog logger = LogManager.GetLogger(typeof(Application));

        static MainWindow _mainWindow;

        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.DispatcherUnhandledException += App_DispatcherUnhandledException;
            app.InitializeComponent();
            app.Run();
        }

        static void OnNotifyIconClick(object sender, System.EventArgs e)
        {
            if (_mainWindow == null)
            {
                _mainWindow = new MainWindow();
            }

            if (_mainWindow.IsVisible)
            {
                _mainWindow.Hide();
                return;
            }

            _mainWindow.ShowActivated = true;
            _mainWindow.Show();

            _mainWindow.Left = System.Windows.Forms.Control.MousePosition.X;
            _mainWindow.Top = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - _mainWindow.Height;
        }

        static void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            logger.Fatal("An exception which terminates the application occurred", e.Exception);

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

        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            var notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JiraToolkit.exe")), // TODO: Get filename from better way.
                Text = "Jira Toolkit",
                Visible = true
            };

            notifyIcon.Click += OnNotifyIconClick;
            notifyIcon.DoubleClick += OnNotifyIconClick;
        }
    }
}
