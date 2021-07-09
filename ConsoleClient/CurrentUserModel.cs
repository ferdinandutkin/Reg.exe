using System;
using System.Linq;
using Core.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ConsoleClient
{
    public class CurrentUserModel : ReactiveObject
    {
        [Reactive] public User User { get; init; }

        [Reactive] public UserRoles[] Roles { get; init; } = Array.Empty<UserRoles>();


        public bool IsInRole(UserRoles role)
        {
            return Roles.Contains(role);
        }

        public bool IsInRoleOrHigher(UserRoles role)
        {
            return Roles.Any(currentRole => currentRole >= role);
        }
    }
}