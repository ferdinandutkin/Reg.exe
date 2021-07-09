using System;

namespace ConsoleClient
{
    public interface IEntityPropertyManager
    {
        int Add(object obj);
        object Get(Type ownerType, int id, string path);

        void Delete(object obj);

        void Update(Type ownerType, int id, string path, object newValue);
    }
}
