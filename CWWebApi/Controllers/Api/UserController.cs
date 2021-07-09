using Core.Classes;
using Core.Models;
using CWWebApi.Data;
using CWWebApi.Models;
using CWWebApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace CWWebApi.Controllers
{

    [ApiController]

    [Route("api/[controller]")]
    public class UserController : BasicCRUDController<User>
    {
        readonly IUserManagerService userManager;
        readonly ILogger<UserController> logger;



        public UserController(
            IUserManagerService userManagerService,
            IEnumerableRepository<User> userRepostitory,
            ILogger<UserController> logger
            ) : base(userRepostitory, logger)
        {
            userManager = userManagerService;


            this.logger = logger;
        }



        [AllowAnonymous]
        [HttpGet]
        [Route("is-registered/{login}")]

        public bool IsRegistered(string login)
        {
            var ret = userManager.IsRegistered(login);
            return ret;
        }






        [AllowAnonymous]
        [HttpGet]
        [Route("login/")]

        public ActionResult Login([FromBody] LoginModel loginModel)
        {
            if (userManager.Login(loginModel.Login, loginModel.PasswordHash))
            {

                var (login, passwordHash) = loginModel;
                var tokenHandler = new JwtSecurityTokenHandler();




                var tokenDescriptor = new SecurityTokenDescriptor
                {

                    Subject =
                    new ClaimsIdentity(userManager.GetRoles(login)
                    .Select(role => new Claim(ClaimTypes.Role, role.UserRole.ToString()))
                    .Prepend(new Claim(ClaimTypes.Name, loginModel.Login))
                    .Prepend(new Claim(ClaimTypes.SerialNumber, userManager.FindUser(login).Id.ToString())
                    ))
                   ,


                    Issuer = JwtSettings.Issuer,
                    Audience = JwtSettings.Audience,
                    Expires = DateTime.UtcNow.AddDays(3),
                    SigningCredentials = new SigningCredentials(JwtSettings.GetSecurityKey(), SecurityAlgorithms.HmacSha256)

                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);


                logger.LogInformation($"Залогинился: {login}, {passwordHash}");
                return Ok(tokenString);
            }


            else return Unauthorized("Не удалось войти");

        }



        [AllowAnonymous]

        [HttpGet]
        [Route("register/")]

        public bool Register([FromBody] RegisterModel registerModel)
        {
            var (login, password) = registerModel;
            var ret = userManager.Register(login, password,
                new System.Collections.Generic.List<Role>
                { new() { UserRole = UserRoles.User },  new() { UserRole = UserRoles.Anonymous } });

            logger.LogInformation($"{login}, {password}, хеш : {PasswordHasher.ComputeHash(password)}");
            return ret;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("register-admin/")]

        public bool RegisterAdmin([FromBody] RegisterModel registerModel)
        {
            var (login, password) = registerModel;

            var ret = userManager.Register(login, password, new Role[] {
                    new Role() {UserRole = UserRoles.Anonymous},
                    new Role() { UserRole = UserRoles.Admin },
                    new Role() { UserRole = UserRoles.User }

                });

            logger.LogInformation($"Удалось зарегистрировать {login}, {password}? {ret}");
            return ret;


        }


    }
}


