using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Infrastructer
{
    public static class PasswordEncryptor
    {
        public static string Encrpt(string password)
        {
            using var md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(password); //şifreyi şifrelemek üzere byte'lara çeviriyoruz 
            byte[] hashBytes = md5.ComputeHash(inputBytes); //buyt'ları hash'liyoruz.

            return Convert.ToHexString(hashBytes);

        }

    }
}
