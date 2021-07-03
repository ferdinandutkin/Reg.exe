using Core.Models;
using CW.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CW.ViewModels
{
    public class TestResultViewModel : ReactiveObject
    {
        static TestResultViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new TestResultView(), typeof(IViewFor<TestResultViewModel>));
        }
        [Reactive]
        public TestResult Model
        {
            get; set;
        }
    }
}
