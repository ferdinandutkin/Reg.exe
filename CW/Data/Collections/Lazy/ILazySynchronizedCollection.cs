using Core.Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CW.Data
{
    public interface ILazySynchronizedCollection<T> : ISynchronizedCollection<T>, IList<T> where T : INotifiableEntity
    {

        ILazySynchronizedCollection<T> Include(int id, string propertyName);

       

        ILazySynchronizedCollection<T>  Include<U>(int id, Expression<Func<T, U>> selector);
      

        ILazySynchronizedCollection<T> Include(string propertyName);




        ILazySynchronizedCollection<T> Include<U>(Expression<Func<T, U>> selector);
       


    }
}
