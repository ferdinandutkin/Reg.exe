using Core.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CW.ViewModels
{
    public class AnswerViewModel : ReactiveObject
    {

        [Reactive]
        public Answer Model
        {
            get; set;
        }

    }
}
