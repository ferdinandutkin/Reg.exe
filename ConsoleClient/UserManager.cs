using Core.Classes;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ConsoleClient
{
    public partial class ServerInteraction
    {
        public class UserManager : BasicApiControllerInteraction
        {
            readonly ServerInteraction parent;
  




            public UserManager(ServerInteraction parent) : base(parent, "User")
            {
                this.parent = parent;
                
            }

          

            public bool LoginAsAnonymous() => Login("Anonymous", "Anon");
            public bool IsRegistered(string login)
            {
                return BasicGet<bool>("is-registered", login);
            }

            public bool Register(string login, string password)
            {
 
                bool result = default;


                var request = new HttpRequestMessage(HttpMethod.Get, $"{routeToController}/register/")
                {
                    Content = new StringContent(JsonSerializer.Serialize(new { Login = login, Password = password }), Encoding.UTF8, "application/json")
                };




                var response = client.SendAsync(request).Result;

                Console.WriteLine(response.Content);
                if (response.IsSuccessStatusCode)
                {

                    var read = response.Content.ReadAsStringAsync().Result;

                    result = JsonSerializer.Deserialize<bool>(read);


                }

                return result;

            }

            public bool Login(string login, string password)
            {

                string token = string.Empty;


                var passwordHash = PasswordHasher.ComputeHash(password);

               

                var request = new HttpRequestMessage(HttpMethod.Get, $"{routeToController}/login/")
                {
                    Content = new StringContent(JsonSerializer.Serialize(new { Login = login, PasswordHash = passwordHash }), Encoding.UTF8, "application/json")
                };




                var response = client.SendAsync(request).Result;

                Console.WriteLine(response.Content);
                if (response.IsSuccessStatusCode)
                {

                    var read = response.Content.ReadAsStringAsync().Result;

                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadJwtToken(read);



                    parent.Token = jsonToken;

                    token = jsonToken.RawData;

                }



                return token != string.Empty;
            }

        }


    }
}
