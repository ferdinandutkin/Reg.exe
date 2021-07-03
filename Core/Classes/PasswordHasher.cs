using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Classes
{
    public static class PasswordHasher
    {

        public static string ComputeHash(string password)
        {
            var bytes = Encoding.ASCII.GetBytes(password);
            using var sha = SHA1.Create();

            var hash = sha.ComputeHash(sha.ComputeHash(bytes));
            return Encoding.ASCII.GetString(hash);


        }
    }
}
