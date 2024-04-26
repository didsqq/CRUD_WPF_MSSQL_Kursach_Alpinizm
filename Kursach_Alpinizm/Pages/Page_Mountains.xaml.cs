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

namespace Kursach_Alpinizm
{
    /// <summary>
    /// Логика взаимодействия для Page_Mountains.xaml
    /// </summary>
    public partial class Page_Mountains : Page
    {
        public Page_Mountains()
        {
            InitializeComponent();
            Mountains.ItemsSource = AlpinizmEntities.GetContext().mountain.ToList();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_Page((sender as Button).DataContext as mountain));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_Page(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var mountainsforremoving = Mountains.SelectedItems.Cast<mountain>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {mountainsforremoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try 
                {
                    AlpinizmEntities.GetContext().mountain.RemoveRange(mountainsforremoving);
                    AlpinizmEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    Mountains.ItemsSource = AlpinizmEntities.GetContext().mountain.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                AlpinizmEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                Mountains.ItemsSource = AlpinizmEntities.GetContext().mountain.ToList();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Main_Page());
        }
    }
}
