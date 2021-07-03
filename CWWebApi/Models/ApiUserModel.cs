
using Core.Classes;
using Core.Models;
using CWWebApi.Security;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CWWebApi.Models
{

    public class ApiUser : IEntity
    {
        public User User { get; set; }
      

        [JsonIgnore]
        public string Password { get; set; }

        public List<Role> Roles { get; set; }
        public int Id { get; set; }
    }
}
