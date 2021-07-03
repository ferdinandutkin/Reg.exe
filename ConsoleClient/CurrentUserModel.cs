using Core.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public  class CurrentUserModel : ReactiveObject
    {

        [Reactive]
        public User User
        {
            get;
            init;
        }

        [Reactive]
        public UserRoles[] Roles
        {
            get;
            init;
        } = Array.Empty<UserRoles>();
       
    
        public bool IsInRole(UserRoles role) => Roles.Contains(role);

        public bool IsInRoleOrHigher(UserRoles role) => Roles.Any(currentRole => currentRole >= role);

    }
}
