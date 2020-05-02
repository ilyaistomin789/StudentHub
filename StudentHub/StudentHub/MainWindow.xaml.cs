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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudentHub.Account;
using StudentHub.DataBase;
using StudentHub.University;

namespace StudentHub
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Student _student = new Student();
        private Window _window;
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Student student)
        {
            InitializeComponent();
            _student = student;
            if (_student.StudentStatus == "Elder")
            {
                putGapsButton.Visibility = Visibility.Visible;
                setRatingsButton.Visibility = Visibility.Visible;
            }
            studentNameTextBlock.Text = " " + _student.Name;
            GetStudentRatings();
        }

        private void GetStudentRatings()
        {
            string getRatingsQuery = "SELECT SubjectName [Subject], convert(varchar,PDate,104) [Date of issue], Note FROM Progress where StudentId = @StudentId";
            try
            {
                using (SqlConnection connection = new SqlConnection(SqlDataBaseConnection.data))
                {
                    connection.Open();
                    SqlCommand getRatingsCommand = new SqlCommand(getRatingsQuery, connection);
                    getRatingsCommand.CommandType = CommandType.Text;
                    SqlParameter studentIdParameter = new SqlParameter
                    {
                        ParameterName = "@StudentId",
                        Value = _student.StudentId
                    };
                    getRatingsCommand.Parameters.Add(studentIdParameter);
                    getRatingsCommand.ExecuteNonQuery();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(getRatingsCommand);
                    DataTable dt = new DataTable("Progress");
                    dataAdapter.Fill(dt);
                    dg_Progress.ItemsSource = dt.DefaultView;
                    dataAdapter.Update(dt);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void EditInformationButton_OnClick(object sender, RoutedEventArgs e)
        {
            _window = new EditWindow(_student);
            _window.Show();
        }

        private void AdjustmentButton_OnClick(object sender, RoutedEventArgs e)
        {
            _window = new AdjustmentWindow(_student);
            _window.Show();
        }

        private void RetakeButton_OnClick(object sender, RoutedEventArgs e)
        {
            _window = new RetakeWindow();
            _window.Show();
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LogOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            _window = new Login();
            _window.Show();
            this.Close();
        }

        private void SetRatingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            _window = new SetRatingsWindow(_student);
            _window.Show();
        }

        private void PutGapsButton_OnClick(object sender, RoutedEventArgs e)
        {
            _window = new PutGapsWindow(_student);
            _window.Show();
        }

        private void ShowInfoButton_OnClick(object sender, RoutedEventArgs e)
        {
            _window = new ShowInformationWindow(_student);
            _window.Show();
        }
    }
}
