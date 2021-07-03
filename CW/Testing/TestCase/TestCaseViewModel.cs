using Core.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CW
{
    public class TestCaseViewModel : ReactiveObject
    {

        [Reactive]
        public TestCase Model
        {
            get; set;
        }

    }

 
}
