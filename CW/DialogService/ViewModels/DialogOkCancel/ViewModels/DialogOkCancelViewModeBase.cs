using ReactiveUI;
using System.Windows;
using System.Windows.Input;


namespace CW.Dialog.ViewModels
{
    public abstract class DialogOkCancelViewModelBase : DialogViewModelBase
    {

        public ICommand OkCommand => ReactiveCommand.Create<object>(ob => OnOkClicked(ob));


        public ICommand CancelCommand => ReactiveCommand.Create<object>(ob => OnCancelClicked(ob));



        private void OnOkClicked(object parameter)
        {
            CloseDialogWithResult(parameter as Window, DialogResult.Ok);
        }

        private void OnCancelClicked(object parameter)
        {
            CloseDialogWithResult(parameter as Window, DialogResult.Cancel);
        }
    }
}
