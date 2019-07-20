﻿using System.Diagnostics;
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
                var entry = textBox.DataContext as EnvironmentEntryViewModel;
                entry.OpenTicketCommand.Execute();
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
                query.OpenQueryCommand.Execute();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            DragMove();
        }
    }
}