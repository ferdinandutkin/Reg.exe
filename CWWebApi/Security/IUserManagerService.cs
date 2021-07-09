using Core.Models;
using System.Collections.Generic;

namespace CWWebApi.Security
{
    public interface IUserManagerService
    {
        bool IsRegistered(string login);

        bool Login(string login, string passwordHash);


        bool Register(string login, string password, IEnumerable<Role> roles);

        public IEnumerable<Role> GetRoles(string login);




        User FindUser(string login);




    }



}
