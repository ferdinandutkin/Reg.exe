using Core.Classes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;

namespace Core.Models
{

    public class InputQuestion : ReactiveObject, INotifiableEntity
    {
        public int Id
        {
            get; set;
        }

        [Reactive]
        public int Difficulty { get; set; }


        [Reactive]
        public string Name { get; set; }

        [Reactive]
        public string Text { get; set; }

        [Reactive]
        public ObservableCollection<TestCase> TestCases { get; set; } = new ObservableCollection<TestCase>();
    }
}
