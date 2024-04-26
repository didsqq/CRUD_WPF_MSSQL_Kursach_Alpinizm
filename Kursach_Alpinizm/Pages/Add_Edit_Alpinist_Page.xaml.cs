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
    /// Логика взаимодействия для Add_Edit_Alpinist_Page.xaml
    /// </summary>
    public partial class Add_Edit_Alpinist_Page : Page
    {
        private alpinists _currentAlpinist = new alpinists();

        public Add_Edit_Alpinist_Page(alpinists selectedalpinists)
        {
            InitializeComponent();
            ComboRazryad.ItemsSource = AlpinizmEntities.GetContext().sport_category.ToList();

            if (selectedalpinists != null)
                _currentAlpinist = selectedalpinists;
            DataContext = _currentAlpinist;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentAlpinist.Surname))
                errors.AppendLine("Укажите Фамилию");
            if (string.IsNullOrWhiteSpace(_currentAlpinist.Name_))
                errors.AppendLine("Укажите Имя");
            if (string.IsNullOrWhiteSpace(_currentAlpinist.Address_))
                errors.AppendLine("Укажите Адрес");
            if (string.IsNullOrWhiteSpace(_currentAlpinist.Phone))
                errors.AppendLine("Укажите номер телефона");
            if (string.IsNullOrWhiteSpace(_currentAlpinist.Sex))
                errors.AppendLine("Укажите пол");
            if (_currentAlpinist.Sex != "ж" && _currentAlpinist.Sex != "м")
                errors.AppendLine("Пол должен быть либо 'ж' либо 'м'");
            if(ComboRazryad == null)
                errors.AppendLine("Выберите разряд");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentAlpinist.ID_alpinist == 0)
                AlpinizmEntities.GetContext().alpinists.Add(_currentAlpinist);
            try
            {
                AlpinizmEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                NavigationService.Navigate(new Page_Alpinists());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
