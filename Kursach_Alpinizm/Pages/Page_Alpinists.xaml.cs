using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ClosedXML.Excel;
using Microsoft.Win32;

namespace Kursach_Alpinizm.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page_Alpinists.xaml
    /// </summary>
    public partial class Page_Alpinists : Page
    {
        public Page_Alpinists()
        {
            InitializeComponent();
            Alpinists.ItemsSource = AlpinizmEntities.GetContext().alpinists.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_Alpinist_Page(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_Alpinist_Page((sender as Button).DataContext as alpinists));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var alpinistforremoving = Alpinists.SelectedItems.Cast<alpinists>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {alpinistforremoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AlpinizmEntities.GetContext().alpinists.RemoveRange(alpinistforremoving);
                    AlpinizmEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    Alpinists.ItemsSource = AlpinizmEntities.GetContext().alpinists.ToList();
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

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AlpinizmEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                Alpinists.ItemsSource = AlpinizmEntities.GetContext().alpinists.ToList();
            }
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
                        worksheet.Cell(i + 1, 1).Value = "Фамилия";
                        worksheet.Cell(i + 1, 2).Value = "Имя";
                        worksheet.Cell(i + 1, 3).Value = "Адрес";
                        worksheet.Cell(i + 1, 4).Value = "Телефон";
                        worksheet.Cell(i + 1, 5).Value = "Пол";
                        worksheet.Cell(i + 1, 6).Value = "Разряд";

                        foreach (alpinists alpinist in AlpinizmEntities.GetContext().alpinists)
                        {
                            int j = 0;
                            worksheet.Cell(i + 2, j + 1).Value = alpinist.Surname;
                            worksheet.Cell(i + 2, j + 2).Value = alpinist.Name_;
                            worksheet.Cell(i + 2, j + 3).Value = alpinist.Address_;
                            worksheet.Cell(i + 2, j + 4).Value = alpinist.Phone;
                            worksheet.Cell(i + 2, j + 5).Value = alpinist.Sex;
                            worksheet.Cell(i + 2, j + 6).Value = alpinist.sport_category.Title;
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
