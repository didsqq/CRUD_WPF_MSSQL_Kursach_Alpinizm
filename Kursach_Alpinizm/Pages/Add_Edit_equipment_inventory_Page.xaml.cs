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
    /// Логика взаимодействия для Add_Edit_equipment_inventory_Page.xaml
    /// </summary>
    public partial class Add_Edit_equipment_inventory_Page : Page
    {
        private equipment_inventory _currentequipment_inventory = new equipment_inventory();
        public Add_Edit_equipment_inventory_Page( equipment_inventory selectedequipment_inventory)
        {
            InitializeComponent();
            ComboEquipment.ItemsSource = AlpinizmEntities.GetContext().equipment.ToList();
            ComboGroup.ItemsSource = AlpinizmEntities.GetContext().groups.ToList();

            if (selectedequipment_inventory != null)
                _currentequipment_inventory = selectedequipment_inventory;
            DataContext = _currentequipment_inventory;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            //if (string.IsNullOrWhiteSpace(Convert.ToString(_currentequipment_inventory.Quantity_taken)))
              //  errors.AppendLine("Укажите количество");

            if (ComboGroup == null)
                errors.AppendLine("Выберите группу");

            if (ComboEquipment == null)
                errors.AppendLine("Выберите снаряжение");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentequipment_inventory.ID_entry == 0)
                AlpinizmEntities.GetContext().equipment_inventory.Add(_currentequipment_inventory);
            try
            {
                AlpinizmEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                NavigationService.Navigate(new Page_Equipment_inventory());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
