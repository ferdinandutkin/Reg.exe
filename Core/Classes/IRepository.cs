using System.Collections.Generic;

namespace Core.Classes
{
    public interface IRepository<T> where T : class, IEntity
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        public IEnumerable<T> GetAllWithPropertiesIncluded();

    }

}
