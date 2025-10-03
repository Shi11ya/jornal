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
    /// Логика взаимодействия для UsrPage.xaml
    /// </summary>
    public partial class UsrPage : Page
    {
        public UsrPage()
        {
            InitializeComponent();
            UsDG.ItemsSource = RealNav.context.Users.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new UsrAdd(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var delSpr23 = UsDG.SelectedItems.Cast<Users>().ToList();
            if (MessageBox.Show($"Удалить {delSpr23.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                RealNav.context.Users.RemoveRange(delSpr23);
            try
            {
                RealNav.context.SaveChanges();
                UsDG.ItemsSource = RealNav.context.Users.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
        private void ChangeAdd_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new UsrAdd((sender as Button).DataContext as Users));
        }
    }
}
