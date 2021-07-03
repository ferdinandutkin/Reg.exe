using Core.Classes;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CW.Data
{
    public abstract class LazyNotifiableEntityCollectionBase<T> : SynchronizedCollection<T>,  ILazySynchronizedCollection<T>

         where T : class, INotifiableEntity, new()

    {


        public LazyNotifiableEntityCollectionBase(IEnumerable<T> collection) : base(collection)
        {

        }

        public abstract ILazySynchronizedCollection<T> Include(int id, string propertyName);
        public abstract ILazySynchronizedCollection<T> Include(string propertyName);

        public ILazySynchronizedCollection<T> Include<U>(int id, Expression<Func<T, U>> selector)
            => Include(id, ResolveNameFromSelector(selector));

        static string ResolveNameFromSelector<U>(Expression<Func<T, U>> selector)
        {
            var expression = selector.Body as MemberExpression;
            return expression.Member.Name;
        }

        public ILazySynchronizedCollection<T> Include<U>(Expression<Func<T, U>> selector)
            => Include(ResolveNameFromSelector(selector));
    }
}
