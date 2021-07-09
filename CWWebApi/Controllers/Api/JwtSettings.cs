using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CWWebApi
{
    public static class JwtSettings
    {

        public readonly static string Audience = "https://regexe.azurewebsites.net";

        public readonly static string Issuer = "https://regexe.azurewebsites.net";
        public static SecurityKey GetSecurityKey() => new SymmetricSecurityKey(Secret);
        private static readonly byte[] Secret = Encoding.ASCII.GetBytes("Simple spell but quite unbreakable");
    }
}
