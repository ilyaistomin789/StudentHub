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
    /// Логика взаимодействия для ShowInformationWindow.xaml
    /// </summary>
    public partial class ShowInformationWindow : Window
    {
        public ShowInformationWindow(Student student)
        {
            InitializeComponent();
            s_fioTextBlock.Text = student.Name;
            s_facultyTextBlock.Text = student.Faculty;
            s_specializationTextBlock.Text = student.Specialization;
            s_courseTextBlock.Text = student.Course.ToString();
            s_groupTextBlock.Text = student.Group.ToString();
            s_birthdayTextBlock.Text = student.Birthday;
        }
    }
}
