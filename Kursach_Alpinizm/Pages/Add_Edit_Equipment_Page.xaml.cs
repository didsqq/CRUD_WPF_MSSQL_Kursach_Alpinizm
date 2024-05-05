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
    /// Логика взаимодействия для Add_Edit_Equipment_Page.xaml
    /// </summary>
    public partial class Add_Edit_Equipment_Page : Page
    {
        private equipment _currentequipment = new equipment();
        public Add_Edit_Equipment_Page(equipment selectedequipment)
        {
            InitializeComponent();

            if (selectedequipment != null)
                _currentequipment = selectedequipment;
            DataContext = _currentequipment;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentequipment.Title))
                errors.AppendLine("Укажите название снаряжение");

            if (_currentequipment.Quantity_available < 1 || _currentequipment.Quantity_available  > 2147483647)
                errors.AppendLine("Укажите количество");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentequipment.ID_equipment == 0)
                AlpinizmEntities.GetContext().equipment.Add(_currentequipment);
            try
            {
                AlpinizmEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                NavigationService.Navigate(new Page_Equipment());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_Equipment());
        }
    }
}
