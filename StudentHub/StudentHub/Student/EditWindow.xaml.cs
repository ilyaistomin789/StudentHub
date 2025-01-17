﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            string getSpecQuery = "SELECT Specialization FROM Specialization";
            string getFacultyQuery = "Select Faculty FROM Faculty";
            try
            {
                using (SqlConnection connection = new SqlConnection(SqlDataBaseConnection.data))
                {
                    connection.Open();
                    SqlCommand getSpecCommand = new SqlCommand(getSpecQuery,connection);
                    SqlCommand getFacultyCommand = new SqlCommand(getFacultyQuery,connection);
                    getSpecCommand.CommandType = CommandType.Text;
                    getFacultyCommand.CommandType = CommandType.Text;
                    var specializations = getSpecCommand.ExecuteReader();
                    if (specializations.HasRows)
                    {
                        while (specializations.Read())
                        {
                            e_specializationComboBox.Items.Add(specializations.GetString(0));
                        }
                        specializations.Close();
                    }
                    var faculties = getFacultyCommand.ExecuteReader();
                    if (faculties.HasRows)
                    {
                        while (faculties.Read())
                        {
                            e_facultyComboBox.Items.Add(faculties.GetString(0));
                        }
                        faculties.Close();
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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        private void E_editInformationButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(e_fioTextBox.Text,"^[a-zA-Z\\s]{2,39}$"))
            {
                MessageBox.Show("Incorrect Student FIO");
                return;
            }
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
