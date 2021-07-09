using Core.Classes;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CW.Data
{
    public interface ISynchronizedCollection<T> : INotifyCollectionChanged, IList<T>
        where T : INotifiableEntity
    {
        bool ISSynchronizationEnabled { get; set; }
    }
}
