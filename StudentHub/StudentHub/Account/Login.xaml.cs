using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using StudentHub.DataBase;
using StudentHub.University;

namespace StudentHub.Account
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private Window _window;
        private readonly Student _student = new Student();
        private readonly AdminAccount _admin = new AdminAccount();
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

        private bool IsAdmin(int userId, SqlConnection connection)
        {
            string isAdminProcedure = "IS_ADMIN";
            SqlCommand isAdminCommand = new SqlCommand(isAdminProcedure, connection);
            isAdminCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter isAdminParameter = new SqlParameter
            {
                ParameterName = "@UserId",
                Value = userId
            };
            isAdminCommand.Parameters.Add(isAdminParameter);
            return Convert.ToInt32(isAdminCommand.ExecuteScalar()) > 0;
        }

        private void SetStudentFields(int userId, SqlConnection connection)
        {
            string getStudentFieldsProcedure = "GET_STUDENT_FIELDS";
            SqlCommand getStudentFields = new SqlCommand(getStudentFieldsProcedure, connection);
            getStudentFields.CommandType = CommandType.StoredProcedure;
            SqlParameter userIdParameter = new SqlParameter
            {
                ParameterName = "UserId",
                Value = userId
            };
            getStudentFields.Parameters.Add(userIdParameter);
            var currentStudent = getStudentFields.ExecuteReader();
            if (currentStudent.HasRows)
            {
                while (currentStudent.Read())
                {
                    _student.StudentId = currentStudent.GetInt32(0);
                    _student.UserId = currentStudent.GetInt32(1);
                    _student.Name = currentStudent.GetString(2);
                    _student.StudentStatus = currentStudent.GetString(3);
                    _student.Course = currentStudent.GetInt32(4);
                    _student.Group = currentStudent.GetInt32(5);
                    _student.Specialization = currentStudent.GetString(6);
                    _student.Faculty = currentStudent.GetString(7);
                    _student.Birthday = currentStudent.GetDateTime(8).ToString("d");
                    _student.Email = currentStudent.GetString(9);
                }
                currentStudent.Close();
            }

        }

        private void SetAdminFields(int userId, SqlConnection connection)
        {
            string getAdminFieldsQuery = "SELECT * FROM admin WHERE UserId = @UserId";
            SqlCommand getAdminFieldsCommand = new SqlCommand(getAdminFieldsQuery, connection);
            getAdminFieldsCommand.CommandType = CommandType.Text;
            SqlParameter userIdParameter = new SqlParameter
            {
                ParameterName = "@UserId",
                Value = userId
            };
            getAdminFieldsCommand.Parameters.Add(userIdParameter);
            var currentAdmin = getAdminFieldsCommand.ExecuteReader();
            if (currentAdmin.HasRows)
            {
                while (currentAdmin.Read())
                {
                    _admin.AdminId = currentAdmin.GetInt32(0);
                    _admin.AdminName = currentAdmin.GetString(1);
                    _admin.UserId = currentAdmin.GetInt32(2);
                }
                currentAdmin.Close();
            }

        }

        private void LogInButton_OnClick(object sender, RoutedEventArgs e)
        {
            bool userExist = false;
            User currentUser = new User();
            string getUserProcedure = "GET_USER";
            if (logIn_UserName.Text == String.Empty)
            {
                MessageBox.Show("Please, enter the User name");
                return;
            }

            if (logIn_Password.Password == String.Empty)
            {
                MessageBox.Show("Please, enter the Password");
                return;
            }

            if (logIn_Password.Password.Length < 5)
            {
                MessageBox.Show("Allowed password length: 5 characters");
                return;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(SqlDataBaseConnection.data))
                {
                    connection.Open();
                    SqlCommand getUserCommand = new SqlCommand(getUserProcedure,connection);
                    getUserCommand.CommandType = CommandType.StoredProcedure;
                    var users = getUserCommand.ExecuteReader();
                    if (users.HasRows)
                    {
                        while (users.Read())
                        {
                            if (logIn_UserName.Text != users.GetString(1) ||
                                User.GetHashPassword(logIn_Password.Password) != users.GetString(2)) continue;
                            userExist = true;
                            currentUser.UserId = users.GetInt32(0);
                            currentUser.UserName = users.GetString(1);
                            currentUser.Password = users.GetString(2);
                        }
                        users.Close();
                    }

                    if (userExist)
                    {
                        if (IsAdmin(Convert.ToInt32(currentUser.UserId),connection))
                        {
                            SqlDataBaseConnection.ApplyAdminPrivileges();
                            SetAdminFields(Convert.ToInt32(currentUser.UserId),connection);
                            _window = new AdminWindow(_admin.AdminName);
                            _window.Show();
                            this.Close();
                        }
                        else
                        {
                            SqlDataBaseConnection.ApplyUserPrivileges();
                            SetStudentFields(Convert.ToInt32(currentUser.UserId),connection);
                            _window = new MainWindow(_student);
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please, check that the information you entered is correct");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
