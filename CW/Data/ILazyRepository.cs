using Core.Classes;

namespace CW.Data
{
    public interface ILazyRepository<T> : IRepository<T> where T : class, INotifiableEntity
    {
        new ILazySynchronizedCollection<T> GetAll();

        new ISynchronizedCollection<T> GetAllWithPropertiesIncluded();

    }

}
