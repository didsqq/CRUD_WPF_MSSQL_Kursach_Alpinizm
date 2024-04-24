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
            Mountains.ItemsSource = AlpinizmEntities.GetContext().mountain_climbs.ToList();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Edit_Page());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                AlpinizmEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                Mountains.ItemsSource = AlpinizmEntities.GetContext().mountain_climbs.ToList();
            }
        }
    }
}
