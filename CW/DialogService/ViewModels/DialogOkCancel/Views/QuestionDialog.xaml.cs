using CW.Dialog.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows;

namespace CW.Dialog.Views
{
    /// <summary>
    /// Логика взаимодействия для DialogYesNo.xaml
    /// </summary>
    public partial class QuestionDialog : ReactiveWindow<QuestionDialogViewModel>, IDialogService<QuestionDialogViewModel>
    {

        
        public QuestionDialog()
        {
            InitializeComponent();
         
            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.QuestionCreationControlViewModel, v => v.qqc.ViewModel).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.OkCommand, v => v.okButton).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.CancelCommand, v => v.cancelButton).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.IsNew, v => v.buttons.Visibility,
                    isNew => isNew ? Visibility.Visible : Visibility.Collapsed
                    ).DisposeWith(d);
            });

   
        }

        private void okCancelButton_Click(object sender, RoutedEventArgs e) => this.Close();

        void IDialogService<QuestionDialogViewModel>.ShowDialog() => this.ShowDialog();
    }
}
