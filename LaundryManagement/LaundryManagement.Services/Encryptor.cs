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
    }
}
