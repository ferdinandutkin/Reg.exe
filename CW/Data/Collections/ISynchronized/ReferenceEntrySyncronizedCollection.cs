using Core.Models;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW.Data 
{
    class ReferenceEntrySyncronizedCollection : ObservableCollection<ReferenceEntry>, ISynchronizedCollection<ReferenceEntry>

    {
        public bool ISSynchronizationEnabled { get; set; } = true;



        IObjectManager<ReferenceEntry> LoadingManager { get; set; }
        

        public ReferenceEntrySyncronizedCollection()
        {
            LoadingManager = Locator.Current.GetService<IObjectManager<ReferenceEntry>>();

            this.CollectionChanged += ReferenceEntrySynctonizedCollection_CollectionChanged;
        }

        public ReferenceEntrySyncronizedCollection(IEnumerable<ReferenceEntry> entities) : this()
        {
            this.CollectionChanged -= ReferenceEntrySynctonizedCollection_CollectionChanged;
            foreach (ReferenceEntry entity in entities)
            {

                Add(entity);
                entity.PropertyChanged += Entity_PropertyChanged; 
                

            }
            this.CollectionChanged += ReferenceEntrySynctonizedCollection_CollectionChanged;

        }

        private void Entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ISSynchronizationEnabled)
            {
                var newValue = typeof(ReferenceEntry).GetProperty(e.PropertyName).GetValue(sender);
                LoadingManager.Update((sender as ReferenceEntry).Id, e.PropertyName, newValue);

               
            }
        }

        private void ReferenceEntrySynctonizedCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ISSynchronizationEnabled)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (ReferenceEntry item in e.NewItems)
                    {

                        item.Id = LoadingManager.Add(item);
                        item.PropertyChanged += Entity_PropertyChanged;
                       
                    }
                }

                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (ReferenceEntry item in e.OldItems)
                    {
                        item.PropertyChanged -= Entity_PropertyChanged;
                        LoadingManager.Delete(item);
                    }
                }
            }
        }


      

    }
}
