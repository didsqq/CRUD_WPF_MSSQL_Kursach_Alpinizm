using ClosedXML.Excel;
using Microsoft.Win32;
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

namespace Kursach_Alpinizm.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page_Climbs.xaml
    /// </summary>
    public partial class Page_Climbs : Page
    {
        public Page_Climbs()
        {
            InitializeComponent();
            Climbs.ItemsSource = AlpinizmEntities.GetContext().mountain_climbs.ToList();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AlpinizmEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                Climbs.ItemsSource = AlpinizmEntities.GetContext().mountain_climbs.ToList();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_Climb_Page((sender as Button).DataContext as mountain_climbs));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_Climb_Page(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var climbforremoving = Climbs.SelectedItems.Cast<mountain_climbs>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {climbforremoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AlpinizmEntities.GetContext().mountain_climbs.RemoveRange(climbforremoving);
                    AlpinizmEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    Climbs.ItemsSource = AlpinizmEntities.GetContext().mountain_climbs.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Main_Page());
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*",
                FileName = "Report.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                using (var workbook = new XLWorkbook())
                {
                    try
                    {
                        var worksheet = workbook.Worksheets.Add("Report");
                        int i = 0;
                        worksheet.Cell(i + 1, 1).Value = "Группа";
                        worksheet.Cell(i + 1, 2).Value = "Гора";
                        worksheet.Cell(i + 1, 3).Value = "Категория сложности";
                        worksheet.Cell(i + 1, 4).Value = "Дата начала";
                        worksheet.Cell(i + 1, 5).Value = "Дата конца";
                        worksheet.Cell(i + 1, 6).Value = "Итог";

                        foreach (mountain_climbs climb in AlpinizmEntities.GetContext().mountain_climbs)
                        {
                            int j = 0;
                            worksheet.Cell(i + 2, j + 1).Value = climb.groups.ID_groups;
                            worksheet.Cell(i + 2, j + 2).Value = climb.mountain.Title;
                            worksheet.Cell(i + 2, j + 3).Value = climb.category_of_difficulty.Title;
                            worksheet.Cell(i + 2, j + 4).Value = climb.Start_date_;
                            worksheet.Cell(i + 2, j + 4).Style.DateFormat.Format = "yyyy-mm-dd";  // Устанавливаем формат даты
                            worksheet.Cell(i + 2, j + 5).Value = climb.End_date_;
                            worksheet.Cell(i + 2, j + 5).Style.DateFormat.Format = "yyyy-mm-dd";
                            worksheet.Cell(i + 2, j + 6).Value = climb.Total;
                            i++;
                        }

                        workbook.SaveAs(filePath);
                        MessageBox.Show("Отчет успешно сохранен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
