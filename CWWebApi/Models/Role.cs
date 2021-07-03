using Core.Classes;
using Core.Models;
using CWWebApi.Models;

namespace CWWebApi.Security
{

    public class Role : IEntity
    //не то чтобы это было прямо сильно нужно 
    //но энамы-сущности почему-то можно только в обычном ef
    {
        public ApiUser User { get; set; }
        public int Id { get; set; }

        public UserRoles UserRole
        {
            get; set;
        }
    }
}