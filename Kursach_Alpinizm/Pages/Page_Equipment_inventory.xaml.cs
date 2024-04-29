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
    /// Логика взаимодействия для Page_Equipment_inventory.xaml
    /// </summary>
    public partial class Page_Equipment_inventory : Page
    {
        public Page_Equipment_inventory()
        {
            InitializeComponent();
            Equipment_inventory.ItemsSource = AlpinizmEntities.GetContext().equipment_inventory.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_equipment_inventory_Page(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_equipment_inventory_Page((sender as Button).DataContext as equipment_inventory));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var equipment_inventoryforremoving = Equipment_inventory.SelectedItems.Cast<equipment_inventory>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {equipment_inventoryforremoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AlpinizmEntities.GetContext().equipment_inventory.RemoveRange(equipment_inventoryforremoving);
                    AlpinizmEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    Equipment_inventory.ItemsSource = AlpinizmEntities.GetContext().equipment_inventory.ToList();
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
                Equipment_inventory.ItemsSource = AlpinizmEntities.GetContext().equipment_inventory.ToList();
            }
        }
    }
}
