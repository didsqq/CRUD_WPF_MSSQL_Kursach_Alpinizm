using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Kursach_Alpinizm
{
    public class LogIn
    {
        public static bool logIn(string login, string pass)
        {
            foreach (team user in AlpinizmEntities.GetContext().team)
            {
                if (login == user.Login_ && GetHashString(pass) == user.Password_)
                {
                    return true;
                }
            }
            return false;
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
    }
}
