using ConsoleClient;
using Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static ConsoleClient.ServerInteraction;

namespace CW.Data
{
    public  class ObjectManager<T> : IObjectManager<T>  where T : class, INotifiableEntity
    {
        readonly ApiControllerWithPropertyAccessInteraction<T> crudController;
        public ObjectManager(ApiControllerWithPropertyAccessInteraction<T> crudController )
        {
            this.crudController = crudController;
        }
        public int Add(object obj) => crudController.Add((T)obj);

        public void Delete(T obj) => Task.Run(() => crudController.Delete(obj.Id));

        public void Delete(object obj) =>  this.Delete(obj as T);



        public object Get(int id, string path) => crudController.GetProperty(id, path);


        public void Update(int id, string path, object newValue) =>  crudController.UpdateProperty(id, path, newValue);



        public int Add(T obj) => crudController.Add(obj);
        
    }
}
