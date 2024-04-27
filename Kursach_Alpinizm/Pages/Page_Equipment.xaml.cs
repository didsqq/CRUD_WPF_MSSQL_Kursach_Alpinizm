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
    }
}
