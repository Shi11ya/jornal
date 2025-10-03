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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prilpract
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new UsrPage());
        }

        private void StudentsButton_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new StudentsPage());
        }
        private void TeachersButton_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new TeachersPage());
        }

        private void SubjectsButton_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new SubjectPage());
        }

        private void GradesButton_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new GradesPage());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }

        private void JournButton_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new JourPage());
        }
    }
}
