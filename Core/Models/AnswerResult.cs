using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.Models
{
    public class AnswerResult : ReactiveObject
    {
        [Reactive]
        public Answer Answer
        {
            get; set;
        }

        [Reactive]
        public bool Result
        {
            get; set;
        }

    }


}
