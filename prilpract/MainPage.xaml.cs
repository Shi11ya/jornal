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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
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

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new UsrPage());
        }
    }
}
