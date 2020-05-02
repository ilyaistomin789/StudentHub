﻿using System;
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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Student _student;
        private string connectionString = SqlDataBaseConnection.data;
        private UniversityEssence university = UniversityEssence.GetInstance();

        public EditWindow(Student student)
        {
            InitializeComponent();
            _student = student;
            InitializeComboBox();
            e_fioTextBox.Text = student.Name;
            e_facultyComboBox.Text = student.Faculty;
            e_specializationComboBox.Text = student.Specialization;
            e_courseComboBox.Text = student.Course.ToString();
            e_groupComboBox.Text = student.Group.ToString();
            e_birthdayCalendar.SelectedDate = DateTime.Parse(student.Birthday);
        }

        private void InitializeComboBox()
        {
            foreach (var t in university.faculties)
            {
                e_facultyComboBox.Items.Add(t);
            }

            foreach (var t in university.specializations)
            {
                e_specializationComboBox.Items.Add(t);
            }

            foreach (var t in university.courses)
            {
                e_courseComboBox.Items.Add(t);
            }

            foreach (var t in university.groups)
            {
                e_groupComboBox.Items.Add(t);
            }

        }

        private void E_editInformationButton_OnClick(object sender, RoutedEventArgs e)
        {
            string setStudentFieldsProcedure = "SET_STUDENT_FIELDS";
            try
            {
                SqlDataBaseConnection.ApplyUserPrivileges();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand setStudentFieldsCommand = new SqlCommand(setStudentFieldsProcedure, connection);
                    setStudentFieldsCommand.CommandType = CommandType.StoredProcedure;
                    SqlParameter studentIdParameter = new SqlParameter
                    {
                        ParameterName = "@StudentId",
                        Value = _student.StudentId
                    };
                    SqlParameter studentNameParameter = new SqlParameter
                    {
                        ParameterName = "@StudentName",
                        Value = e_fioTextBox.Text
                    };
                    SqlParameter courseParameter = new SqlParameter
                    {
                        ParameterName = "@Course",
                        Value = Convert.ToInt32(e_courseComboBox.Text)
                    };
                    SqlParameter groupIdParameter = new SqlParameter
                    {
                        ParameterName = "@GroupId",
                        Value = Convert.ToInt32(e_groupComboBox.Text)
                    };
                    SqlParameter specParameter = new SqlParameter
                    {
                        ParameterName = "@Specialization",
                        Value = e_specializationComboBox.Text
                    };
                    SqlParameter facultyParameter = new SqlParameter
                    {
                        ParameterName = "@Faculty",
                        Value = e_facultyComboBox.Text
                    };
                    SqlParameter birthdayParameter = new SqlParameter
                    {
                        ParameterName = "@Birthday",
                        Value = e_birthdayCalendar.SelectedDate.Value
                    };

                    setStudentFieldsCommand.Parameters.Add(studentIdParameter);
                    setStudentFieldsCommand.Parameters.Add(studentNameParameter);
                    setStudentFieldsCommand.Parameters.Add(courseParameter);
                    setStudentFieldsCommand.Parameters.Add(groupIdParameter);
                    setStudentFieldsCommand.Parameters.Add(specParameter);
                    setStudentFieldsCommand.Parameters.Add(facultyParameter);
                    setStudentFieldsCommand.Parameters.Add(birthdayParameter);
                    var done = setStudentFieldsCommand.ExecuteReader();
                    if (done.HasRows)
                    {
                        while (done.Read())
                        {
                            _student.Name = done.GetString(2);
                            _student.Course = done.GetInt32(4);
                            _student.Group = done.GetInt32(5);
                            _student.Specialization = done.GetString(6);
                            _student.Faculty = done.GetString(7);
                            _student.Birthday = done.GetDateTime(8).ToString("d");
                        }
                        done.Close();
                    }
                    
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
