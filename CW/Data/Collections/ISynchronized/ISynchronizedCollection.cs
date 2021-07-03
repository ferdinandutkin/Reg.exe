using Core.Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW.Data 
{
    public interface ISynchronizedCollection<T> : INotifyCollectionChanged, IList<T>
        where T : INotifiableEntity
    {
        bool ISSynchronizationEnabled { get; set; }
    }
}
