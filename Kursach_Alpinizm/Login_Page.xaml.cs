using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для Login_Page.xaml
    /// </summary>
    public partial class Login_Page : Page
    {
        public Login_Page()
        {
            InitializeComponent();
        }

        public static string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = "";
            foreach (byte b in byteHash)
            {
                hash += string.Format("{0:x2}", b);
            }
            return hash;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
                foreach (team user in AlpinizmEntities.GetContext().team)
                {
                    if (Login.Text == user.Login_ && Password.Text == user.Password_)
                    {
                        MessageBox.Show("Вход успешен!");
                        NavigationService.Navigate(new Main_Page());
                        return;
                    }
                }
                MessageBox.Show("Логин или пароль указан неверно! ");
        }

        private void BtnMail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
