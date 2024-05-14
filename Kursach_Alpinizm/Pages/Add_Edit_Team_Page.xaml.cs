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
    /// Логика взаимодействия для Add_Edit_Team_Page.xaml
    /// </summary>
    public partial class Add_Edit_Team_Page : Page
    {
        private team _currentteam = new team();
        public Add_Edit_Team_Page(team selectedteam)
        {
            InitializeComponent();
            ComboPosition.ItemsSource = AlpinizmEntities.GetContext().position.ToList();

            if (selectedteam != null)
                _currentteam = selectedteam;
            DataContext = _currentteam;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            
            if (string.IsNullOrWhiteSpace(_currentteam.Surname_name))
                errors.AppendLine("Укажите Фамилию имя");
            if (_currentteam.Date_of_birth == null)
                errors.AppendLine("Укажите Дату рождения");
            if (string.IsNullOrWhiteSpace(_currentteam.Address_))
                errors.AppendLine("Укажите Адрес");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentteam.Phone)))
                errors.AppendLine("Укажите номер телефона");
            if (string.IsNullOrWhiteSpace(_currentteam.Login_))
                errors.AppendLine("Укажите Логин");
            if (string.IsNullOrWhiteSpace(_currentteam.Password_))
                errors.AppendLine("Укажите Пароль");
            if (ComboPosition.SelectedItem == null)
                errors.AppendLine("Выберите должность");

            foreach (team team in AlpinizmEntities.GetContext().team)
            {
                if (_currentteam.Login_ == team.Login_)
                {
                    MessageBox.Show("Пользователь с таким логином существует");
                    return;
                }
                if (_currentteam.Phone == team.Phone)
                {
                    MessageBox.Show("Пользователь с таким номером телефона существует");
                    return;
                }
                if (_currentteam.Address_ == team.Address_)
                {
                    MessageBox.Show("Пользователь с такой почтой уже существует");
                    return;
                }
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentteam.ID_team_member == 0)
                AlpinizmEntities.GetContext().team.Add(_currentteam);
            try
            {
                _currentteam.Password_ = Login_Page.GetHashString(_currentteam.Password_);
                AlpinizmEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                NavigationService.Navigate(new Page_Team());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_Team());
        }
    }
}
