using CW.Dialog.Views;
using CW.ViewModels;
using ReactiveUI.Fody.Helpers;


namespace CW.Dialog.ViewModels
{
    public class QuestionDialogViewModel : DialogOkCancelViewModelBase
    {
        static QuestionDialogViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new QuestionDialog(), typeof(IDialogService<QuestionDialogViewModel>));

        }

        [Reactive]
        public bool IsNew
        {
            get; set;
        }

        [Reactive]
        public QuestionCreationControlViewModel QuestionCreationControlViewModel
        { get; set; }
    }
}
