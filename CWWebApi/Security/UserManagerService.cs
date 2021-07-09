using Core.Classes;
using Core.Models;
using CWWebApi.Data;
using CWWebApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CWWebApi.Security
{
    public class UserManagerService : IUserManagerService
    {

        private readonly IEnumerableRepository<User> userRepository;
        private readonly IEnumerableRepository<ApiUser> apiUserRepo;
        private readonly ILogger<UserManagerService> logger;


        public UserManagerService(IEnumerableRepository<User> userRepository,
            IEnumerableRepository<ApiUser> apiUserRepo, ILogger<UserManagerService> logger)
        {
            this.apiUserRepo = apiUserRepo;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public bool IsRegistered(string login)
        {
            var ret = userRepository.Any(u => u.Name == login);
            logger.LogInformation($"Пользователь {login} уже существует? {ret}");

            return ret;

        }


        public User FindUser(string login) => userRepository.FirstOrDefault(user => user.Name == login);

        public IEnumerable<Role> GetRoles(string login)
        {
            if (IsRegistered(login))
            {
                return apiUserRepo.GetAllWithPropertiesIncluded()
                     .FirstOrDefault(apiUser => apiUser?.User?.Name == login)?.Roles ?? Enumerable.Empty<Role>();
            }
            return Enumerable.Empty<Role>();
        }

        public bool Login(string login, string passwordHash)
        {
            var ret = apiUserRepo.GetAllWithPropertiesIncluded()
                .FirstOrDefault(apiUser => apiUser?.User?.Name == login)?.Password is string password &&
                PasswordHasher.ComputeHash(password) == passwordHash;

            logger.LogInformation($"Пользователь {login} с хешем пароля {passwordHash} залогинился? {ret}");

            return ret;

        }

        public bool Register(string login, string password, IEnumerable<Role> roles)
        {
            try
            {
                apiUserRepo.Add(new ApiUser { Password = password, Roles = roles.ToList(), User = new User() { Name = login } });
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
