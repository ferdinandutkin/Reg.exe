using Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW.Data
{
    public interface ILazyRepository<T> : IRepository<T> where T : class, INotifiableEntity
    {
        new ILazySynchronizedCollection<T> GetAll();

        new ISynchronizedCollection<T> GetAllWithPropertiesIncluded();

    }

}
