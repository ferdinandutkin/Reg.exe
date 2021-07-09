using Core.Models;

using CW.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CW.ViewModels
{
    public class RegexTesterViewModel : ReactiveObject
    {

        static RegexTesterViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new RegexTesterView(), typeof(IViewFor<RegexTesterViewModel>));
        }


        [Reactive]
        public RegexTester Model
        {
            get; set;
        }




        public RegexTesterViewModel()
        {



        }


    }
}
