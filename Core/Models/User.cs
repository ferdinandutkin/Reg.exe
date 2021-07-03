using Core.Classes;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace Core.Models
{
    public class User : ReactiveObject, INotifiableEntity
    {

        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public ObservableCollection<TestResult> Results
        {
            get; set;
        } = new();
    }
}
