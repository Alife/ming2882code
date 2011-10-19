using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace MC.Mvc.Web.Helpers.Encoders
{
    public static class MD5
    {
        public static string Encode(string text)
        {
            char[] chars = text.ToCharArray();
            var bytes = Encoding.Unicode.GetBytes(text.ToCharArray());
            var hash = new MD5CryptoServiceProvider().ComputeHash(bytes);
            return hash.Aggregate(new StringBuilder(32), (sb, b) => sb.Append(b.ToString("X2"))).ToString();
        }
    }
}
