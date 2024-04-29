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
    /// Логика взаимодействия для Page_Group.xaml
    /// </summary>
    public partial class Page_Group : Page
    {
        public Page_Group()
        {
            InitializeComponent();
            Groups.ItemsSource = AlpinizmEntities.GetContext().groups.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_Groups_Page(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_Groups_Page((sender as Button).DataContext as groups));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var groupforremoving = Groups.SelectedItems.Cast<groups>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {groupforremoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AlpinizmEntities.GetContext().groups.RemoveRange(groupforremoving);
                    AlpinizmEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    Groups.ItemsSource = AlpinizmEntities.GetContext().groups.ToList();
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
                Groups.ItemsSource = AlpinizmEntities.GetContext().groups.ToList();
            }
        }
    }
}
