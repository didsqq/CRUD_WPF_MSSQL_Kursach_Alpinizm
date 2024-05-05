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
    /// Логика взаимодействия для Main_Page.xaml
    /// </summary>
    public partial class Main_Page : Page
    {
        public Main_Page()
        {
            InitializeComponent();
        }

        private void BtnMountain_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_Mountains());
        }

        private void BtnAlpinists_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Page_Alpinists());
        }

        private void BtnEquipment_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Page_Equipment());
        }

        private void BtnClimbs_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Page_Climbs());
        }

        private void BtnPosition_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Page_Position());
        }

        private void BtnTeam_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Page_Team());
        }

        private void BtnEquipment_inventory_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Page_Equipment_inventory());
        }

        private void BtnGroup_inventory_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Page_Group());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login_Page());
        }
    }
}
