using Core.Classes;

namespace CWWebApi.Data
{
    public interface IPropertyAccessRepository<T> : IRepository<T> where T : class, IEntity
    {

        public object Get(int id, string property)
        {
            var entity = Get(id);
            return entity?.GetType().GetProperty(property)?.GetValue(entity);
        }



        public void Update(int id, string property, object newValue)
        {
            var entity = Get(id);

            entity.GetType().GetProperty(property)?.SetValue(entity, newValue);

            Update(entity);
        }

    }

    public interface IPropertyAccessEnumerableRepository<T> : IEnumerableRepository<T>, IPropertyAccessRepository<T> where T : class, IEntity
    {

    }
}