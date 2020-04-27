using System.Windows;
using System.Windows.Input;


namespace StudentHub.Account
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        private Window _window;
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void Reg_GoToLogin_OnClick(object sender, RoutedEventArgs e)
        {
            _window = new Login();
            _window.Show();
            this.Close();
        }

        private void Reg_SignUp_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
