using Core.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Splat;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace CW.Data
{



    public class LazyNotifiableEntityCollection<T> : LazyNotifiableEntityCollectionBase<T> where T :
        class, INotifiableEntity, new()
    {

        public Dictionary<int, HashSet<string>> included = new();


        bool IsIncluded(int id, string propertyName) =>
            included.TryGetValue(id, out var includedProups) && includedProups.Contains(propertyName);


        void MarkAsIncluded(int id, string propertyName)
        {
            if (included.ContainsKey(id))
            {
                included[id].Add(propertyName);
            }
            else
            {
                included[id] = new() { propertyName };
            }
        }



        public LazyNotifiableEntityCollection(IEnumerable<T> collection) : base(collection)
        {

        }


        bool TryMakeGenericType(Type genericType, out Type res, params Type[] genericArguments)
        {
            try
            {
                res = genericType.MakeGenericType(genericArguments);
            }
            catch (ArgumentException)
            {
                res = default;
                return false;
            }
            return true;

        }

        public override ILazySynchronizedCollection<T> Include(int id, string propertyName)
        {
            var prop = typeof(T).GetProperty(propertyName);


            if (CanBeReplacedByCollection(prop))

            {

                if (!IsIncluded(id, propertyName))
                {
                    var genericCollectionType = typeof(LazyNotifiableEntityCollection<>)
                  .MakeGenericType(prop.PropertyType.GetGenericArguments()[0]);

                    prop.SetValue(this.SingleOrDefault(entity => entity.Id == id),
                Activator.CreateInstance(genericCollectionType, LoadingManager.Get(id, propertyName)));

                    MarkAsIncluded(id, propertyName);
                }


            }


            else
            {
                if (!IsIncluded(id, propertyName))
                {

                    prop.SetValue(this.SingleOrDefault(entity => entity.Id == id),
                LoadingManager.Get(id, propertyName));

                    MarkAsIncluded(id, propertyName);
                }
            }

            return this;

        }
        static bool CanBeReplacedByCollection(PropertyInfo property)
        {
            try
            {
                if (typeof(INotifyCollectionChanged).IsAssignableFrom(property.PropertyType))
                {
                    var genericArgument = property.PropertyType.GetGenericArguments()?[0];



                    return  genericArgument is not null
                         &&
                         typeof(INotifiableEntity).IsAssignableFrom(genericArgument)
                         &&
                         Locator.Current.GetService(typeof(IObjectManager<>).MakeGenericType(genericArgument)) is not null;

                    
                }
                else return false;



            }
            catch (ArgumentException)
            {
                return false;
            }

        }


        public override ILazySynchronizedCollection<T> Include(string propertyName)
        {
            var prop = typeof(T).GetProperty(propertyName);


            if (CanBeReplacedByCollection(prop))
            {

                var collectionType = typeof(LazyNotifiableEntityCollection<>)
               .MakeGenericType(prop.PropertyType.GetGenericArguments()[0]);
                foreach (var entity in this.Where(e => !IsIncluded(e.Id, propertyName)))
                {
                    prop.SetValue(entity,
                Activator.CreateInstance(collectionType, LoadingManager.Get(entity.Id, propertyName)));

                    MarkAsIncluded(entity.Id, propertyName);

                }


            }


            else
            {
                foreach (var entity in this.Where(e => !IsIncluded(e.Id, propertyName)))
                {

                    prop.SetValue(entity, LoadingManager.Get(entity.Id, propertyName));

                    MarkAsIncluded(entity.Id, propertyName);


                }

            }
            return this;
        }






    }
}

