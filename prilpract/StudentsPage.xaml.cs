using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using Microsoft.Win32;

namespace prilpract
{
    /// <summary>
    /// Логика взаимодействия для StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {
        private ICollectionView _studentsView;

        public StudentsPage()
        {
            InitializeComponent();
            LoadStudentsData();
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new StudentsAdd(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var delSpr23 = StuDG.SelectedItems.Cast<Students>().ToList();
            if (MessageBox.Show($"Удалить {delSpr23.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                RealNav.context.Students.RemoveRange(delSpr23);
            try
            {
                RealNav.context.SaveChanges();
                StuDG.ItemsSource = RealNav.context.Teachers.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new StudentsAdd((sender as Button).DataContext as Students));
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StuDG.ItemsSource = RealNav.context.Students.ToList();
        }
        private void LoadStudentsData() 
        {
            var stu = RealNav.context.Students.ToList();
            _studentsView = CollectionViewSource.GetDefaultView(stu);
            _studentsView.Filter = StuFilter;
            StuDG.ItemsSource = _studentsView;
        }

        private bool StuFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
                return true;

            var stu = item as Students;
            if (stu == null) return false;

            string searchText = SearchTextBox.Text.ToLower();

            return (stu.studentFIO != null && stu.studentFIO.ToLower().Contains(searchText));

        }

        // Обработчик кнопки поиска
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filteredTeachers = RealNav.context.Students.Where(t => t.studentFIO != null && t.studentFIO.Contains(searchText)).ToList();
            StuDG.ItemsSource = filteredTeachers;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filteredTeachers = RealNav.context.Students.Where(t => t.studentFIO != null && t.studentFIO.Contains(searchText)).ToList();
            StuDG.ItemsSource = filteredTeachers;
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    Title = "Сохранить отчет",
                    FileName = "Отчет_по_студентам.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    Excel.Application excel = new Excel.Application();
                    Excel.Workbook workbook = excel.Workbooks.Add();
                    Excel.Worksheet worksheet = workbook.ActiveSheet;

                    // Add headers
                    worksheet.Cells[1, 1] = "ФИО студента";
                    worksheet.Cells[1, 2] = "Класс";
                    worksheet.Cells[1, 3] = "Средний балл";

                    // Format headers
                    Excel.Range headerRange = worksheet.Range["A1:C1"];
                    headerRange.Font.Name = "Times New Roman";
                    headerRange.Font.Size = 14;
                    headerRange.Font.Bold = true;

                    // Get students with their average grades and class information
                    var studentsWithGrades = RealNav.context.Students
                        .Select(s => new
                        {
                            StudentFIO = s.studentFIO,
                            ClassName = RealNav.context.Classes
                                .Where(c => c.classid == s.classid)
                                .Select(c => c.classname)
                                .FirstOrDefault() ?? "Не указан",
                            AverageGrade = RealNav.context.Grades
                                .Where(g => g.studentid == s.studentid)
                                .Average(g => g.grade) ?? 0
                        })
                        .ToList();

                    // Add data
                    int row = 2;
                    foreach (var student in studentsWithGrades)
                    {
                        worksheet.Cells[row, 1] = student.StudentFIO;
                        worksheet.Cells[row, 2] = student.ClassName;
                        worksheet.Cells[row, 3] = Math.Round(student.AverageGrade, 2);
                        row++;
                    }

                    // Format data cells
                    Excel.Range dataRange = worksheet.Range[$"A2:C{row - 1}"];
                    dataRange.Font.Name = "Times New Roman";
                    dataRange.Font.Size = 14;

                    // Auto-fit columns
                    worksheet.Columns.AutoFit();

                    // Save and close
                    workbook.SaveAs(saveFileDialog.FileName);
                    workbook.Close();
                    excel.Quit();

                    // Release COM objects
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);

                    MessageBox.Show("Отчет успешно сформирован!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
