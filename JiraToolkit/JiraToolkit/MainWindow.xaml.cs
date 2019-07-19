using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using JiraToolkit.ViewModels;

namespace JiraToolkit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow
    {
        readonly MainViewModel _viewmodel;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = _viewmodel = new MainViewModel();
        }

        void OpenTicketNumber(object sender, KeyEventArgs e)
        {
            if (e != null && e.Key != Key.Enter)
            {
                return;
            }

            if (sender is TextBox textBox)
            {
                var entry = textBox.DataContext as EnvironmentEntry;
                Debug.Assert(entry != null, nameof(entry) + " != null");
                Debug.Assert(_viewmodel != null, nameof(_viewmodel) + " != null");

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo(entry.Root + "browse/" + entry.Prefix + $"-{textBox.Text}")
                };
                process.Start();
            }
        }
    }
}