using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StudentHub.Account;
using StudentHub.Admin;

namespace StudentHub
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private Window _window;
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void AdminWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();
        private void AdjustmentWorkButton_OnClick(object sender, RoutedEventArgs e)
        {
            _window = new AdjustmentWorkWindow();
            _window.Show();
        }

        private void GapsWorkButton_OnClick(object sender, RoutedEventArgs e)
        {
            _window = new GapsWorkWindow();
            _window.Show();
        }

        private void RetakeWorkButton_OnClick(object sender, RoutedEventArgs e)
        {
            _window = new RetakeWorkWindow();
            _window.Show();
        }

        private void SearchQueryButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StudentProgressButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ReportButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EmailButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LogOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            _window = new Login();
            _window.Show();
            this.Close();
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
