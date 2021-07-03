using Core.Classes;
using CWWebApi.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CWWebApi.Data
{

  
    public class PropertyAccessEnumerableRepository<T> : IPropertyAccessEnumerableRepository<T> where T : class, IEntity
    {
        protected readonly DbContext context;
        protected readonly DbSet<T> entities;

        protected virtual private List<T> List
        {
            get => entities.AsNoTracking().ToList();
        }

        public IEnumerator<T> GetEnumerator() => List.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => (List as IEnumerable).GetEnumerator();

        public PropertyAccessEnumerableRepository(DbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        

        }
        public virtual IEnumerable<T> GetAll() => List;
        public virtual T Get(int id)
        {

            return   List.SingleOrDefault(s => s.Id == id);
          
 

        }
        public virtual void Add(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            entities.Add(entity);
            context.SaveChanges();
        }
        public virtual void Update(T entity)
        {

            if (entity is null) throw new ArgumentNullException(nameof(entity));

            entities.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
        public virtual void Delete(int id)
        {


            T entity = entities.SingleOrDefault(s => s.Id == id);
            entities.Remove(entity);
            context.SaveChanges();
        }

       virtual public IEnumerable<T> GetAllWithPropertiesIncluded()
            =>
             entities.Include(context.GetIncludePaths<T>()).ToList();
     
    }
}