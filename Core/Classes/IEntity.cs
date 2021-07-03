using System.ComponentModel;

namespace Core.Classes
{
    public interface IEntity
    {

        int Id { get; set; }
    }

    public interface INotifiableEntity : INotifyPropertyChanged, IEntity
    {

    }
}
