using ReactiveUI;
using System.Windows;

namespace CW.Dialog.ViewModels
{
    public abstract class DialogViewModelBase : ReactiveObject
    {
        public DialogResult UserDialogResult
        {
            get;
            private set;
        }

        public void CloseDialogWithResult(Window dialog, DialogResult result)
        {

            UserDialogResult = result;
            if (dialog != null)
                dialog.DialogResult = true;
        }
    }
}
