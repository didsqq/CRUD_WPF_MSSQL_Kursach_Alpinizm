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
    /// Логика взаимодействия для Page_Equipment.xaml
    /// </summary>
    public partial class Page_Equipment : Page
    {
        public Page_Equipment()
        {
            InitializeComponent();
            Equipment.ItemsSource = AlpinizmEntities.GetContext().equipment.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_Equipment_Page(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var equipmentforremoving = Equipment.SelectedItems.Cast<equipment>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {equipmentforremoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AlpinizmEntities.GetContext().equipment.RemoveRange(equipmentforremoving);
                    AlpinizmEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    Equipment.ItemsSource = AlpinizmEntities.GetContext().equipment.ToList();
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

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_Equipment_Page((sender as Button).DataContext as equipment));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AlpinizmEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                Equipment.ItemsSource = AlpinizmEntities.GetContext().equipment.ToList();
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
                        worksheet.Cell(i + 1, 1).Value = "Название";
                        worksheet.Cell(i + 1, 2).Value = "Количество";


                        foreach (equipment equipment in AlpinizmEntities.GetContext().equipment)
                        {
                            int j = 0;
                            worksheet.Cell(i + 2, j + 1).Value = equipment.Title;
                            worksheet.Cell(i + 2, j + 2).Value = equipment.Quantity_available;
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
