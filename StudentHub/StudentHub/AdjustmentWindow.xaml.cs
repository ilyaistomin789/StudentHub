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
    /// Логика взаимодействия для AdjustmentWindow.xaml
    /// </summary>
    public partial class AdjustmentWindow : Window
    {
        private UniversityEssence university = UniversityEssence.GetInstance();
        private Student _student;
        public AdjustmentWindow()
        {
            InitializeComponent();
        }

        public AdjustmentWindow(Student student)
        {
            InitializeComponent();
            _student = student;
            foreach (var t in university.subjects)
            {
                a_subjectComboBox.Items.Add(t);
            }
        }

        private void A_sendRequestButton_OnClick(object sender, RoutedEventArgs e)
        {
            string addAdjustmentProcedure = "ADD_ADJUSTMENT";
            try
            {
                using (SqlConnection connection = new SqlConnection(SqlDataBaseConnection.data))
                {
                    connection.Open();
                    SqlCommand addAdjustmentCommand = new SqlCommand(addAdjustmentProcedure, connection);
                    addAdjustmentCommand.CommandType = CommandType.StoredProcedure;
                    SqlParameter studentIdParameter = new SqlParameter
                    {
                        ParameterName = "@StudentId",
                        Value = _student.StudentId
                    };
                    SqlParameter subjectNameParameter = new SqlParameter
                    {
                        ParameterName = "@SubjectName",
                        Value = a_subjectComboBox.Text
                    };
                    SqlParameter aDateParameter = new SqlParameter
                    {
                        ParameterName = "@ADate",
                        Value = a_adjustmentDateCalendar.SelectedDate
                    };
                    addAdjustmentCommand.Parameters.Add(studentIdParameter);
                    addAdjustmentCommand.Parameters.Add(subjectNameParameter);
                    addAdjustmentCommand.Parameters.Add(aDateParameter);
                    var done = addAdjustmentCommand.ExecuteNonQuery();
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
