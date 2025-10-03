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
    /// Логика взаимодействия для SubjectAdd.xaml
    /// </summary>
    public partial class SubjectAdd : Page
    {
        Subjects Sub;
        bool checkNew;
        public SubjectAdd(Subjects e)
        {
            InitializeComponent();
            if (e == null)
            {
                e = new Subjects();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = Sub = e;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (checkNew)
            {
                RealNav.context.Subjects.Add(Sub);
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
