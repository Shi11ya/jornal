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
using static prilpract.LogPage;


namespace prilpract
{
    /// <summary>
    /// Логика взаимодействия для LogPage.xaml
    /// </summary>
    public partial class LogPage : Page
    {
        public LogPage()
        {
            InitializeComponent();

        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text;
            string password = PasswordBox.Password;

            // Проверка логина и пароля
            if ((login == "admin" && password == "123") ||
                (login == "3" && password == "111"))
            {
                // Успешная авторизация для админов
                ErrorText.Visibility = Visibility.Collapsed;
                MessageBox.Show($"Добро пожаловать, {login}!", "Успешная авторизация",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                // Переход на страницу администратора
                Nav.MainFrame.Navigate(new AdminPage());
            }
            else if (login == "teacher" && password == "222")
            {
                // Успешная авторизация для учителя
                ErrorText.Visibility = Visibility.Collapsed;
                MessageBox.Show($"Добро пожаловать, {login}!", "Успешная авторизация",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                // Переход на главную страницу
                Nav.MainFrame.Navigate(new MainPage());
            }
            else
            {
                // Неверные данные
                ErrorText.Visibility = Visibility.Visible;
                PasswordBox.Password = "";
                LoginBox.Focus();
            }
        }
    }
}