using CW.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginControl.xaml
    /// </summary>
    public partial class LoginControl : ReactiveUserControl<LoginViewModel>
    {
        public LoginControl()
        {
            InitializeComponent();

            this.WhenActivated(
                d =>
                {
                    //бинд пароля унесен в xaml потому что я хз уже




                    this.Bind(ViewModel, vm => vm.Login, v => v.LoginTextBox.Text).DisposeWith(d);
                    this.BindCommand(ViewModel, vm => vm.LoginCommand, v => v.LoginButton).DisposeWith(d);
                    this.BindCommand(ViewModel, vm => vm.LoginAsAnonymousCommand, v => v.AnonymousButton).DisposeWith(d);
                    this.BindCommand(ViewModel, vm => vm.Register, v => v.RegisterButton).DisposeWith(d);

                    this.Bind(ViewModel, vm => vm.Errors, v => v.Errors.Text).DisposeWith(d);
                });
        }
    }
}
