using Core.Models;
using CW.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CW.ViewModels
{
    public class AnswerResultViewModel : ReactiveObject
    {
       static AnswerResultViewModel()
       {
            Splat.Locator.CurrentMutable.Register(() => new AnswerResultView(), typeof(IViewFor<AnswerResultViewModel>));
       }
       [Reactive]
       public AnswerResult Model
       {
            get; set;
       }
    }
}
