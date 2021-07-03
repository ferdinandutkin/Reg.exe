using CW.Views;
using ReactiveUI;

using ReactiveUI.Fody.Helpers;
using Core.Models;

namespace CW.ViewModels
{
    public class RegexTestingControlViewModel : ReactiveObject
    {

        static RegexTestingControlViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new RegexTestingControl(), typeof(IViewFor<RegexTestingControlViewModel>));
        }

        [Reactive]
        public ReactiveObject CurrentControlViewModel
        {
            get; set;
        }



        public RegexTestingControlViewModel()
        {
            CurrentControlViewModel = new RegexTesterViewModel()
            {
                Model = new RegexTester()
            };
        }
    }
}
