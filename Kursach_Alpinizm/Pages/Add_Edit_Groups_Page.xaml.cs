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
    /// Логика взаимодействия для Add_Edit_Groups_Page.xaml
    /// </summary>
    public partial class Add_Edit_Groups_Page : Page
    {
        private groups _currentgroup = new groups();
        public Add_Edit_Groups_Page(groups selectedgroup)
        {
            InitializeComponent();
            if (selectedgroup != null)
                _currentgroup = selectedgroup;
            DataContext = _currentgroup;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            //if (_currentgroup.Number_of_participants )
              //  errors.AppendLine("Укажите Количество");

            if (_currentgroup.ID_groups == 0)
                AlpinizmEntities.GetContext().groups.Add(_currentgroup);
            try
            {
                AlpinizmEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                NavigationService.Navigate(new Page_Group());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_Group());
        }
    }
}
