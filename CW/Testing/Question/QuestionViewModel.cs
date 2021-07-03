using Core.Models;
using ReactiveUI.Fody.Helpers;

namespace CW.ViewModels
{
    public  class QuestionViewModel 
    {
        [Reactive]
        public InputQuestion Model
        {
            get; set;
        }


    }
}
