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

namespace StudentHub.Account
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private Window _window;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void ExitButton_OnClick(object sender, RoutedEventArgs e) => this.Close();

        private void SignUpButton_OnClick(object sender, RoutedEventArgs e)
        {
            _window = new SignUp();
            _window.Show();
            this.Close();
        }

        private void LogInButton_OnClick(object sender, RoutedEventArgs e)
        {
            _window = new MainWindow();
            _window.Show();
        }
    }
}
