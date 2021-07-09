using System.Security.Cryptography;
using System.Text;

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
