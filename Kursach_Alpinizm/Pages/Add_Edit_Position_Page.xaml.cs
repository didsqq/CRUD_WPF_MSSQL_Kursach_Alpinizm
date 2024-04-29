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
    /// Логика взаимодействия для Add_Edit_Position_Page.xaml
    /// </summary>
    public partial class Add_Edit_Position_Page : Page
    {
        private position _currentposition = new position();
        public Add_Edit_Position_Page(position selectedposition)
        {
            InitializeComponent();

            if (selectedposition != null)
                _currentposition = selectedposition;
            DataContext = _currentposition;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentposition.Title))
                errors.AppendLine("Укажите Название");
            if (string.IsNullOrWhiteSpace(_currentposition.Description_of))
                errors.AppendLine("Укажите Описание");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentposition.ID_position == 0)
                AlpinizmEntities.GetContext().position.Add(_currentposition);
            try
            {
                AlpinizmEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                NavigationService.Navigate(new Page_Position());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
