using CW.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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


                   
                        
                    this.Bind(ViewModel, vm => vm.Login, v => v.loginTextBox.Text).DisposeWith(d);
                    this.BindCommand(ViewModel, vm => vm.LoginCommand, v => v.loginButton).DisposeWith(d);
                    this.BindCommand(ViewModel, vm => vm.LoginAsAnonymousCommand, v => v.anonymousButton).DisposeWith(d);
                    this.BindCommand(ViewModel, vm => vm.Register, v => v.registerButton).DisposeWith(d);

                    this.Bind(ViewModel, vm => vm.Errors, v => v.errors.Text).DisposeWith(d);
                });
        }
    }
}
