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
    /// Логика взаимодействия для Page_Team.xaml
    /// </summary>
    public partial class Page_Team : Page
    {
        public Page_Team()
        {
            InitializeComponent();
            Team.ItemsSource = AlpinizmEntities.GetContext().team.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_Team_Page(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_Team_Page((sender as Button).DataContext as team));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var teamforremoving = Team.SelectedItems.Cast<team>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {teamforremoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AlpinizmEntities.GetContext().team.RemoveRange(teamforremoving);
                    AlpinizmEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    Team.ItemsSource = AlpinizmEntities.GetContext().team.ToList();
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
                Team.ItemsSource = AlpinizmEntities.GetContext().team.ToList();
            }
        }
    }
}
