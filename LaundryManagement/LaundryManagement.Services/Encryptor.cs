using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace LaundryManagement.Services
{
    public class Encryptor
    {
        public static string Hash(string value)
        {
            var md5 = MD5.Create();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(value));
            return (new ASCIIEncoding()).GetString(md5data);
        }

        public static string GenerateRandom()
        {
            Random rand = new Random();

            string source =
            "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            char[] chars = new char[8];
            for (int i = 0; i < 8; i++)
            {
                chars[i] = source[rand.Next(source.Length)];
            }
            return new string(chars);
        }
    }
}
