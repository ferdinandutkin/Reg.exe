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
                this.BindCommand(ViewModel, vm => vm.OkCommand, v => v.OkButton).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.CancelCommand, v => v.CancelButton).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.AddTestCaseViewModel, v => v.AddTestCase.ViewModel);
            });


        }

        private void OkCancelButton_Click(object sender, RoutedEventArgs e) => Close();

        void IDialogService<AddTestCaseDialogViewModel>.ShowDialog() => ShowDialog();

    }
}
