using Kursach_Alpinizm.Pages;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Kursach_Alpinizm
{
    /// <summary>
    /// Логика взаимодействия для Login_Page.xaml
    /// </summary>
    public partial class Login_Page : Page
    {
        public Login_Page()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            /*                foreach (team user in AlpinizmEntities.GetContext().team)
                            {
                                if (Login.Text == user.Login_ && GetHashString(Password.Text) == user.Password_)
                                {
                                    MessageBox.Show("Вход успешен!");
                                    NavigationService.Navigate(new Main_Page());
                                    return;
                                }
                            }
                            MessageBox.Show("Логин или пароль указан неверно! ");*/
            if( LogIn.logIn(Login.Text, Password.Text))
            {
                MessageBox.Show("Успешный вход");
                NavigationService.Navigate(new Main_Page());
            }
            else
            {
                MessageBox.Show("Логин или пароль указан неверно!");
            }
            
        }

        private void BtnMail_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_Mail());
        }

    }
}
