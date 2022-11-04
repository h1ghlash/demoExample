using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace demotest.AppDataFile
{
    class HashPassword
    {
        public static string hashPassword(string password)
        {
            MD5 mD5 = MD5.Create();

            byte[] b = Encoding.ASCII.GetBytes(password);
            byte[] hash = mD5.ComputeHash(b);

            StringBuilder stringBuilder = new StringBuilder();
            foreach(var a in hash)
            {
                stringBuilder.Append(a.ToString("X2"));
            }
            return Convert.ToString(stringBuilder);
        }
    }
}
