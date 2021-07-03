using CWWebApi.Security;
using System;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            var client = new ServerInteraction("https://localhost:5001/");

            var pword = "123445";
            var login = "ifiif";
            client.User.Register(login, pword);



            bool isRegister = client.User.IsRegistered(login);
            Console.WriteLine(isRegister);

            Console.WriteLine(client.User.Login(login, pword));

            var res = client.QuestionControllerCallsWrapper.Get();
           
            int i = 3;

            



        }
    }
}
