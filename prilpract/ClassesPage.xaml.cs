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
    /// Логика взаимодействия для ClassesPage.xaml
    /// </summary>
    public partial class ClassesPage : Page
    {
        public ClassesPage()
        {
            InitializeComponent();
            ClDG.ItemsSource = RealNav.context.Classes.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new ClassAdd(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var delSpr23 = ClDG.SelectedItems.Cast<Classes>().ToList();
            if (MessageBox.Show($"Удалить {delSpr23.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                RealNav.context.Classes.RemoveRange(delSpr23);
            try
            {
                RealNav.context.SaveChanges();
               ClDG.ItemsSource = RealNav.context.Teachers.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdBtn_Click(object sender, RoutedEventArgs e)
        {
           Nav.MainFrame.Navigate(new ClassAdd((sender as Button).DataContext as Classes));
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ClDG.ItemsSource = RealNav.context.Classes.ToList();
        }
    }
}
