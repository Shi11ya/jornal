using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для GradesPage.xaml
    /// </summary>
    public partial class GradesPage : Page
    {
        List<Grades> sales;

        public GradesPage()
        {
            InitializeComponent();
            GradeDG.ItemsSource = RealNav.context.Grades.ToList();
            var cl = RealNav.context.Grades.ToList();
            cl.Insert(0, new Grades() { });
            FiltrCmd.ItemsSource = cl;
            FiltrCmd.SelectedIndex = 0;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new GradesAdd(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var delSpr23 = GradeDG.SelectedItems.Cast<Grades>().ToList();
            if (MessageBox.Show($"Удалить {delSpr23.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                RealNav.context.Grades.RemoveRange(delSpr23);
            try
            {
                RealNav.context.SaveChanges();
                GradeDG.ItemsSource = RealNav.context.Grades.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new GradesAdd((sender as Button).DataContext as Grades));
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GradeDG.ItemsSource = RealNav.context.Grades.ToList();
        }
        void Update()
        {
            sales = RealNav.context.Grades.ToList();
            if (FiltrCmd.SelectedIndex > 0)
                sales = sales.Where(sale => sale.gradedate == (FiltrCmd.SelectedItem as Grades).gradedate).ToList();
            GradeDG.ItemsSource = sales;
        }

        private void FiltrCmd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
    }
}
