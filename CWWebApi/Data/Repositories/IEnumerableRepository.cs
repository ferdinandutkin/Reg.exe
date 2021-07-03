using Core.Classes;
using System.Collections.Generic;

namespace CWWebApi.Data
{



    public interface IEnumerableRepository<T> : IRepository<T>, IEnumerable<T> where T : class, IEntity
    {
         
       
    }

    
   
}

