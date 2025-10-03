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
    /// Логика взаимодействия для JoAdd.xaml
    /// </summary>
    public partial class JoAdd : Page
    {
       Journ lawer;
        bool checkNew;
        public JoAdd(Journ e)
        {
            InitializeComponent();
            if (e == null)
            {
                e = new Journ();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = lawer = e;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            if (checkNew)
            {
                RealNav.context.Journ.Add(lawer);
            }

            try
            {
                RealNav.context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Nav.MainFrame.GoBack();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
    }
}
