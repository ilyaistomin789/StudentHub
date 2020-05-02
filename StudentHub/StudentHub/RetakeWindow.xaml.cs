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

namespace StudentHub
{
    /// <summary>
    /// Логика взаимодействия для RetakeWindow.xaml
    /// </summary>
    public partial class RetakeWindow : Window
    {
        private Student _student;
        public RetakeWindow()
        {
            InitializeComponent();
        }

        public RetakeWindow(Student student)
        {
            InitializeComponent();
            _student = student;
        }

        private void R_sendRequestButton_OnClick(object sender, RoutedEventArgs e)
        {
            string addRetakeProcedure = "ADD_RETAKE";
            try
            {
                using (SqlConnection connection = new SqlConnection(SqlDataBaseConnection.data))
                {
                    connection.Open();
                    SqlCommand addRetakeCommand = new SqlCommand(addRetakeProcedure, connection);
                    addRetakeCommand.CommandType = CommandType.StoredProcedure;
                    SqlParameter studentIdParameter = new SqlParameter
                    {
                        ParameterName = "@StudentId",
                        Value = _student.StudentId
                    };
                    SqlParameter subjectNameParameter = new SqlParameter
                    {
                        ParameterName = "@SubjectName",
                        Value = r_subjectComboBox.Text
                    };
                    SqlParameter rDateParameter = new SqlParameter
                    {
                        ParameterName = "@RDate",
                        Value = r_adjustmentDateCalendar.SelectedDate
                    };
                    addRetakeCommand.Parameters.Add(studentIdParameter);
                    addRetakeCommand.Parameters.Add(subjectNameParameter);
                    addRetakeCommand.Parameters.Add(rDateParameter);
                    var done = addRetakeCommand.ExecuteNonQuery();
                    MessageBox.Show("Done");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
