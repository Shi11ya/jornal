using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для TeachersPage.xaml
    /// </summary>
    public partial class TeachersPage : Page
    {
        private ICollectionView _teachersView;

        public TeachersPage()
        {
            InitializeComponent();
            TeaDG.ItemsSource = RealNav.context.Teachers.ToList();
            LoadTeachersData();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new TeachersAdd(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var delSpr23 = TeaDG.SelectedItems.Cast<Teachers>().ToList();
            if (MessageBox.Show($"Удалить {delSpr23.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                RealNav.context.Teachers.RemoveRange(delSpr23);
            try
            {
                RealNav.context.SaveChanges();
                TeaDG.ItemsSource = RealNav.context.Teachers.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new TeachersAdd((sender as Button).DataContext as Teachers));
        }
        private void ChangeAdd_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new TeachersAdd((sender as Button).DataContext as Teachers));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TeaDG.ItemsSource = RealNav.context.Teachers.ToList();
        }

        private void LoadTeachersData()
        {
            var teachers = RealNav.context.Teachers.ToList();
            _teachersView = CollectionViewSource.GetDefaultView(teachers);
            _teachersView.Filter = TeacherFilter;
            TeaDG.ItemsSource = _teachersView;
        }

        private bool TeacherFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
                return true;

            var teacher = item as Teachers;
            if (teacher == null) return false;

            string searchText = SearchTextBox.Text.ToLower();

            return (teacher.teacherFIO != null && teacher.teacherFIO.ToLower().Contains(searchText)) ||
                  
                   (teacher.subjectspecialization != null && teacher.subjectspecialization.ToLower().Contains(searchText)) ||
                   teacher.teacherid.ToString().Contains(searchText);
        }

        // Обработчик кнопки поиска
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filteredTeachers = RealNav.context.Teachers.Where(t => t.teacherFIO != null && t.teacherFIO.Contains(searchText)).ToList();
            TeaDG.ItemsSource = filteredTeachers;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filteredTeachers = RealNav.context.Teachers.Where(t => t.teacherFIO != null && t.teacherFIO.Contains(searchText)).ToList();
            TeaDG.ItemsSource = filteredTeachers;
        }

        private void CalculateAge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var teachers = RealNav.context.Teachers.ToList();
                var currentDate = DateTime.Now;
                var teachersWithAge = teachers.Select(t => new
                {
                    ФИО = t.teacherFIO,
                    Возраст = t.birthdate.HasValue ? 
                        (currentDate.Year - t.birthdate.Value.Year - 
                        (currentDate.Month < t.birthdate.Value.Month || 
                        (currentDate.Month == t.birthdate.Value.Month && 
                         currentDate.Day < t.birthdate.Value.Day) ? 1 : 0)) : 0
                }).ToList();

                var message = "Возраст преподавателей:\n\n";
                foreach (var teacher in teachersWithAge)
                {
                    message += $"{teacher.ФИО}: {teacher.Возраст} лет\n";
                }

                MessageBox.Show(message, "Возраст преподавателей", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при расчете возраста: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}