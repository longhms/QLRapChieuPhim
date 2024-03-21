using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Classes
{
    internal class Common
    {
        public string CreatePassword(string text)
        {
            string pass = "";
            SHA1Managed sha1 = new SHA1Managed();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder hashSb = new StringBuilder();
            foreach (byte b in hash)
                hashSb.Append(b.ToString());
            pass = hashSb.ToString();
            return pass;
        }
    }
}
