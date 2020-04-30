using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using StudentHub.DataBase;


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
        private bool CheckUser(string inputUserName)
        {
            string getUserProcedure = "GET_USERNAME";
            try
            {
                using (SqlConnection connection = new SqlConnection(SqlDataBaseConnection.data))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(getUserProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    var users = command.ExecuteReader();
                    if (users.HasRows)
                    {
                        while (users.Read())
                        {
                            if (inputUserName == users.GetString(0))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return false;
        }

        private void Reg_SignUp_OnClick(object sender, RoutedEventArgs e)
        {
            if (reg_UserName.Text == String.Empty)
            {
                MessageBox.Show("Enter the User name");
                return;
            }

            if (reg_Password.Password == String.Empty)
            {
                MessageBox.Show("Enter the Password");
                return;
            }

            if (reg_PasswordConfirm.Password == String.Empty)
            {
                MessageBox.Show("Enter the Confirm password");
                return;
            }
            if (reg_Password.Password != reg_PasswordConfirm.Password)
            {
                MessageBox.Show("Passwords don't match");
                return;
            }
            try
            {
                if (CheckUser(reg_UserName.Text))
                {
                    MessageBox.Show("This user exists");
                    return;
                }
                string addUserProcedure = "ADD_USER";
                using (SqlConnection connection = new SqlConnection(SqlDataBaseConnection.data))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(addUserProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter userNameParameter = new SqlParameter
                    {
                        ParameterName = "@UserName",
                        Value = reg_UserName.Text
                    };
                    SqlParameter passwordParameter = new SqlParameter
                    {
                        ParameterName = "@UserPassword",
                        Value = reg_Password.Password
                    };
                    command.Parameters.Add(userNameParameter);
                    command.Parameters.Add(passwordParameter);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Done");
                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }
    }
}
