using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.Models
{
    public class Answer : ReactiveObject
    {
        [Reactive]
        public InputQuestion Question

        {
            get; set;
        }

        [Reactive]
        public string Input
        {
            get; set;
        } = string.Empty;
    }
}
