using Core.Classes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;

namespace Core.Models
{

    public class TestCase : ReactiveObject, INotifiableEntity
    {
        public int Id
        {
            get; set;
        }

        [Reactive]
        public string Text
        {
            get; set;
        } = string.Empty;

      
        [Reactive]
        public ObservableCollection<Position> Positions { get; set; } = new();



    }

}
