using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        private Student student = new Student();
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
            string setStudentFieldsProcedure = "SET_STUDENT_FIELDS";
            SqlCommand setStudentFields = new SqlCommand(setStudentFieldsProcedure, connection);
            setStudentFields.CommandType = CommandType.StoredProcedure;
            SqlParameter userIdParameter = new SqlParameter
            {
                ParameterName = "UserId",
                Value = userId
            };
            setStudentFields.Parameters.Add(userIdParameter);
            var currentStudent = setStudentFields.ExecuteReader();
            if (currentStudent.HasRows)
            {
                while (currentStudent.Read())
                {
                    student.StudentId = currentStudent.GetInt32(0);
                    student.UserId = currentStudent.GetInt32(1);
                    student.Name = currentStudent.GetString(2);
                    student.StudentStatus = currentStudent.GetString(3);
                    student.Course = currentStudent.GetInt32(4);
                    student.Group = currentStudent.GetInt32(5);
                    student.Specialization = currentStudent.GetString(6);
                    student.Faculty = currentStudent.GetString(7);
                    student.Birthday = currentStudent.GetDateTime(8).ToString();
                }
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
                            if (logIn_UserName.Text == users.GetString(1) &&
                                logIn_Password.Password == users.GetString(2))
                            {
                                userExist = true;
                                currentUser.UserId = users.GetInt32(0);
                                currentUser.UserName = users.GetString(1);
                                currentUser.Password = users.GetString(2);
                            }
                        }
                        users.Close();
                    }

                    if (userExist)
                    {
                        if (IsAdmin(Convert.ToInt32(currentUser.UserId),connection))
                        {
                            SqlDataBaseConnection.ApplyAdminPrivileges();
                            _window = new AdminWindow();
                            _window.Show();
                            this.Close();
                        }
                        else
                        {
                            SqlDataBaseConnection.ApplyUserPrivileges();
                            SetStudentFields(Convert.ToInt32(currentUser.UserId),connection);
                            _window = new MainWindow(student);
                            _window.Show();
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
