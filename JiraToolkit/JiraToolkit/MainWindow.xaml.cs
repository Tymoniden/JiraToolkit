using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using JiraToolkit.ViewModels;

namespace JiraToolkit
{
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
                var entry = textBox.DataContext as EnvironmentEntryViewModel;
                if (entry != null)
                {
                    entry.OpenTicketCommand?.Execute();
                    e.Handled = true;
                }
            }
        }

        void OpenQueryCommand(object sender, KeyEventArgs e)
        {
            if (e != null && e.Key != Key.Enter)
            {
                return;
            }

            if (sender is TextBox textBox)
            {
                var query = textBox.DataContext as QueryViewModel;
                if (query != null)
                {
                    query.OpenQueryCommand?.Execute();
                    e.Handled = true;
                }
            }
        }

        async void Initialize(object sender, RoutedEventArgs e) => await _viewmodel.Initialize().ConfigureAwait(false);

        void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed || e.RightButton != MouseButtonState.Released || e.MiddleButton != MouseButtonState.Released)
            {
                return;
            }
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            DragMove();
        }

        void RefreshConfiguration(object sender, KeyEventArgs e)
        {
            if (e?.Key == Key.F5)
            {
                _viewmodel?.UpdateConfiguration();
                e.Handled = true;
            }
        }
    }
}