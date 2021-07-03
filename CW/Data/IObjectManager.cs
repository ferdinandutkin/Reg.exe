using Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW.Data
{
    public interface IObjectManager<T> : IObjectManager where T : INotifiableEntity
    {
        int Add(T obj);

        void Delete(T obj);
    }


    public interface IObjectManager
    {
        int Add(object obj);
        object Get(int id, string path);

        void Delete(object obj);

        void Update(int id, string path, object newValue);
    }

}
