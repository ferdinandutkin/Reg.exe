namespace CWWebApi.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public void Deconstruct(out string login, out string passwordHash) =>
            (login, passwordHash) = (Login, PasswordHash);
    }
}
