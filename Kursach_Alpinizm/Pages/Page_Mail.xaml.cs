using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Логика взаимодействия для Page_Mail.xaml
    /// </summary>
    public partial class Page_Mail : Page
    {
        public Page_Mail()
        {
            InitializeComponent();
        }

        private void BtnMail_Click(object sender, RoutedEventArgs e)
        {
            MailAddress from = new MailAddress("didsqq@yandex.ru", "Adel");

            try
            {

                MailAddress to = new MailAddress(Mail.Text);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Восстановление пароля";
                using (AlpinizmEntities db = new AlpinizmEntities())
                {
                    foreach (team user in db.team)
                    {
                        if (Mail.Text == user.Address_)
                        {
                            string password = GeneratePassword();

                            m.Body = "<h1>Пароль: " + password + "</h1>";
                            string pass_hash = Login_Page.GetHashString(password);
                            user.Password_ = pass_hash;

                            MessageBox.Show("Пароль отправлен на почту");
                            NavigationService.Navigate(new Login_Page());
                        }
                    }
                    db.SaveChanges();
                }
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
                smtp.Credentials = new NetworkCredential("didsqq@yandex.ru", "lhybtwljdlunlgjk");
                smtp.EnableSsl = true;
                smtp.Send(m);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private string GeneratePassword()
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random random = new Random();
            char[] password = new char[10];

            for (int i = 0; i < 10; i++)
            {
                password[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(password);
        }
    }
}
