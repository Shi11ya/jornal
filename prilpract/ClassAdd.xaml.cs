using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Логика взаимодействия для ClassAdd.xaml
    /// </summary>
    public partial class ClassAdd : Page
    {
        Classes cl;
        bool checkNew;
        public ClassAdd(Classes e)
        {
            InitializeComponent();
            if (e == null)
            {
                e = new Classes();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = cl = e;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (checkNew)
            {
                RealNav.context.Classes.Add(cl);
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
