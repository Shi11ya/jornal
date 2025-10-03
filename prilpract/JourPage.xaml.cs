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
    /// Логика взаимодействия для JourPage.xaml
    /// </summary>
    public partial class JourPage : Page
    {
        public JourPage()
        {
            InitializeComponent();
            JoDG.ItemsSource = RealNav.context.Journ.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new JoAdd(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var delSpr23 = JoDG.SelectedItems.Cast<Journ>().ToList();
            if (MessageBox.Show($"Удалить {delSpr23.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                RealNav.context.Journ.RemoveRange(delSpr23);
            try
            {
                RealNav.context.SaveChanges();
                JoDG.ItemsSource = RealNav.context.Journ.ToList();
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
            Nav.MainFrame.Navigate(new JoAdd((sender as Button).DataContext as Journ));
        }
    }
}
