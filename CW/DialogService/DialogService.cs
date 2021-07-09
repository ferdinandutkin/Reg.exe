using CW.Dialog.ViewModels;
using System.Windows;

namespace CW.Dialog
{

    interface IDialogService<T> where T : DialogViewModelBase
    {
        public T ViewModel
        {
            get;
            set;
        }

        Window Owner
        {
            get; set;
        }

        void ShowDialog();


    }
    public enum DialogResult
    {
        Undefined,
        Ok,
        Cancel
    };
    public class DialogService
    {

        public static DialogResult OpenDialog<T>(T dialogModel, Window owner = null) where T : DialogViewModelBase
        {

            var instance = Splat.Locator.Current.GetService(typeof(IDialogService<T>)) as IDialogService<T>;

            
            if (owner is not null)
            {
                instance.Owner = owner;
            }


            instance.ViewModel = dialogModel;

            instance.ShowDialog();



            return instance.ViewModel.UserDialogResult;

        }
    }
}
