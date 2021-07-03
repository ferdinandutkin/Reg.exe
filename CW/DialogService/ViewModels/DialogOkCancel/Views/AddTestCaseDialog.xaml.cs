using CW.Dialog.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows;

namespace CW.Dialog.Views
{
    /// <summary>
    /// Логика взаимодействия для DialogYesNo.xaml
    /// </summary>
    public partial class AddTestCaseDialog : ReactiveWindow<AddTestCaseDialogViewModel>, IDialogService<AddTestCaseDialogViewModel>
    {

        
        public AddTestCaseDialog()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.BindCommand(ViewModel, vm => vm.OkCommand, v => v.okButton).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.CancelCommand, v => v.cancelButton).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.AddTestCaseViewModel, v => v.addTestCase.ViewModel);
            });

   
        }

        private void OkCancelButton_Click(object sender, RoutedEventArgs e) => this.Close();

        void IDialogService<AddTestCaseDialogViewModel>.ShowDialog() => this.ShowDialog();
        
    }
}
