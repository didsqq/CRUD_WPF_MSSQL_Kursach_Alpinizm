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
    }
}
