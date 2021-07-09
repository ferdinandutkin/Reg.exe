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
                this.OneWayBind(ViewModel, vm => vm.QuestionCreationControlViewModel, v => v.Qqc.ViewModel).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.OkCommand, v => v.OkButton).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.CancelCommand, v => v.CancelButton).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.IsNew, v => v.Buttons.Visibility,
                    isNew => isNew ? Visibility.Visible : Visibility.Collapsed
                    ).DisposeWith(d);
            });


        }

        private void okCancelButton_Click(object sender, RoutedEventArgs e) => Close();

        void IDialogService<QuestionDialogViewModel>.ShowDialog() => ShowDialog();
    }
}
