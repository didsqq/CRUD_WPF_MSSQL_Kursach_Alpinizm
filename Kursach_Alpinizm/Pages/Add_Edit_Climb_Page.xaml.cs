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
    /// Логика взаимодействия для Add_Edit_Climb_Page.xaml
    /// </summary>
    public partial class Add_Edit_Climb_Page : Page
    {
        private mountain_climbs _currentClimb = new mountain_climbs();
        public Add_Edit_Climb_Page(mountain_climbs selectedmountain_climbs)
        {
            InitializeComponent();
            ComboDificult.ItemsSource = AlpinizmEntities.GetContext().category_of_difficulty.ToList();
            ComboGroups.ItemsSource = AlpinizmEntities.GetContext().groups.ToList();
            ComboMountain.ItemsSource = AlpinizmEntities.GetContext().mountain.ToList();

            if (selectedmountain_climbs != null)
                _currentClimb = selectedmountain_climbs;
            DataContext = _currentClimb;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (ComboGroups.SelectedItem == null)
                errors.AppendLine("Укажите Группу");

            if (ComboMountain.SelectedItem == null)
                errors.AppendLine("Укажите Гору");

            if (ComboDificult.SelectedItem == null)
                errors.AppendLine("Укажите Сложность");

            if (_currentClimb.Start_date_ == null)
                errors.AppendLine("Укажите Дату начала");
            if (_currentClimb.End_date_ == null)
                errors.AppendLine("Укажите Дату конца");
            if (string.IsNullOrWhiteSpace(_currentClimb.Total))
                errors.AppendLine("Укажите Итог");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentClimb.ID_mountain_climbs == 0)
                AlpinizmEntities.GetContext().mountain_climbs.Add(_currentClimb);
            try
            {
                AlpinizmEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                NavigationService.Navigate(new Page_Climbs());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
