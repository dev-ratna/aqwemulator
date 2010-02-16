using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AQWE.Security
{
    public static class Hash
    {
        public static string SHA1(string Text)
        {
            return Convert.ToBase64String(new System.Security.Cryptography.SHA1CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(Text)));
        }

        public static string MD5(string Text)
        {
            return Convert.ToBase64String(new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(Text)));
        }
    }
}
