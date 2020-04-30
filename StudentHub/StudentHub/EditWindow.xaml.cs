using System;
using System.Collections.Generic;
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
using StudentHub.University;

namespace StudentHub
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Student _student = new Student();
        public EditWindow()
        {
            InitializeComponent();
        }

        public EditWindow(Student student)
        {
            InitializeComponent();
            _student = student;
        }

        private void E_editInformationButton_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
