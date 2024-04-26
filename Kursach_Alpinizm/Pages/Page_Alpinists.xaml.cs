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
    }
}
