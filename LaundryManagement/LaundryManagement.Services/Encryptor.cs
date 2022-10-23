using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using LaundryManagement.Interfaces.Domain.Entities;
using System.ComponentModel;
using System.IO;

namespace LaundryManagement.Services
{
    public class Encryptor
    {
        public static string HashToString(string value)
        {
            var hashedData = HashToByteArray(value);
            return Convert.ToBase64String(hashedData);
        }

        public static byte[] HashToByteArray(string value)
        {
            var md5 = MD5.Create();
            return md5.ComputeHash(Encoding.ASCII.GetBytes(value));            
        }

        public static byte[] HashToByteArrayFromList(IEnumerable<byte[]> values)
        {
            var md5 = MD5.Create();
            byte[] hashValue;
            using (var memoryStream = new MemoryStream())
            {
                foreach (var item in values)
                    memoryStream.Write(item, 0, 16);

                memoryStream.Position = 0;

                hashValue = md5.ComputeHash(memoryStream);
            }

            return hashValue;
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
