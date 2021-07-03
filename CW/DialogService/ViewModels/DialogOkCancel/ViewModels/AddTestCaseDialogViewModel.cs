using CW.Dialog.Views;
using CW.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;


namespace CW.Dialog.ViewModels
{
    public class AddTestCaseDialogViewModel : DialogOkCancelViewModelBase
    {

        [Reactive]
        public bool IsNew {get; set;}

        static AddTestCaseDialogViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new AddTestCaseDialog(), typeof(IDialogService <AddTestCaseDialogViewModel>));
           
        }

        [Reactive]
        public AddTestCaseViewModel AddTestCaseViewModel
        { get; set; }
    }
}
