using Core.Classes;
using Splat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CW.Data
{
    public class SynchronizedCollection<T> : ObservableCollection<T>, ISynchronizedCollection<T>
        where T : class, INotifiableEntity
    {

        public IObjectManager LoadingManager { get; set; }
        public bool ISSynchronizationEnabled { get; set; } = true;

        static bool CanSubscribeToCollection(PropertyInfo property)
        {
            try
            {
                if (typeof(INotifyCollectionChanged).IsAssignableFrom(property.PropertyType))
                {
                    var genericArgument = property.PropertyType.GetGenericArguments()?[0];



                    return genericArgument is not null
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
        static bool CanSubscribeToProperty(PropertyInfo property)

        {
            try
            {
                return typeof(INotifiableEntity).IsAssignableFrom(property.PropertyType)
                                     &&
                                     Locator.Current.GetService(typeof(IObjectManager<>).MakeGenericType(property.PropertyType)) is not null;
            }

            catch (ArgumentException)
            {
                return false;
            }

        }

        public void SubscribeToInnerPropertiesChanged(object o)
        {
            if (o is null)
                return;

            var objectType = o.GetType();
            foreach (var prop in objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite && p.CanRead && p.GetIndexParameters().Length == 0))
            {
                var propValue = prop.GetValue(o);

                if (propValue is null)
                    return;

                if (CanSubscribeToProperty(prop))
                {
                    (propValue as INotifiableEntity).PropertyChanged -= NestedPropertyChanged;
                    (propValue as INotifiableEntity).PropertyChanged += NestedPropertyChanged;
                }
                if (CanSubscribeToCollection(prop))
                {
                    (propValue as INotifyCollectionChanged).CollectionChanged += NestedCollectionChanged;
                    if (propValue is IEnumerable entites)
                    {
                        foreach (INotifiableEntity notifiable in entites)
                        {
                            notifiable.PropertyChanged -= NestedPropertyChanged;
                            notifiable.PropertyChanged += NestedPropertyChanged;
                            SubscribeToInnerPropertiesChanged(notifiable);
                        }
                    }
                    
                }

                //SubscribeToInnerPropertiesChanged(propValue);
    
            }
        }
        public SynchronizedCollection(IEnumerable<T> entities) : this()
        {
            this.CollectionChanged -= LazyObservableCollection_CollectionChanged;
            foreach (T entity in entities)
            {
              
                Add(entity);
                entity.PropertyChanged += Item_PropertyChanged;
                SubscribeToInnerPropertiesChanged(entity);
            }
            this.CollectionChanged += LazyObservableCollection_CollectionChanged;

        }
        public SynchronizedCollection()
        {
            LoadingManager = Locator.Current.GetService<IObjectManager<T>>();

            this.CollectionChanged += LazyObservableCollection_CollectionChanged;
        }

        private void LazyObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ISSynchronizationEnabled)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (T item in e.NewItems)
                    {

                        item.Id = LoadingManager.Add(item);
                        item.PropertyChanged += Item_PropertyChanged;
                        SubscribeToInnerPropertiesChanged(item);
                    }
                }

                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (T item in e.OldItems)
                    {
                        item.PropertyChanged -= Item_PropertyChanged;
                        LoadingManager.Delete(item);
                    }
                }
            }


        }

        private void NestedCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ISSynchronizationEnabled)
            {
                var service = Locator.Current.GetService(typeof(IObjectManager<>).MakeGenericType(sender.GetType().GetGenericArguments()[0]))
                    as IObjectManager;

                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (INotifiableEntity item in e.NewItems)
                    {

                        item.Id = service.Add(item);
                        item.PropertyChanged += NestedPropertyChanged;
                        SubscribeToInnerPropertiesChanged(item);
                    }
                }

                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (INotifiableEntity item in e.OldItems)
                    {
                        item.PropertyChanged -= NestedPropertyChanged;
                        service.Delete(item);
                    }
                }
            }


        }


        private void NestedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ISSynchronizationEnabled)
            {
                var senderType = sender.GetType();
                var service = Locator.Current.GetService(typeof(IObjectManager<>).MakeGenericType(senderType)) as IObjectManager;
                var newValue = senderType.GetProperty(e.PropertyName).GetValue(sender);
                service.Update((sender as IEntity).Id, e.PropertyName, newValue);

                SubscribeToInnerPropertiesChanged(newValue);


            }
        }


        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ISSynchronizationEnabled)
            {
                var newValue = typeof(T).GetProperty(e.PropertyName).GetValue(sender);
                LoadingManager.Update((sender as T).Id, e.PropertyName, newValue);

                SubscribeToInnerPropertiesChanged(newValue);
            }

        }


    }
}
