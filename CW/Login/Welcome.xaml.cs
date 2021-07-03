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
    /// Логика взаимодействия для Welcome.xaml
    /// </summary>
    public partial class Welcome : ReactiveUserControl<WelcomeViewModel>
    {
        public Welcome()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {

                this.BindCommand(ViewModel, vm => vm.LogOutCommand, v => v.logOut).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.Message, v => v.welcomeMessage.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.UserName, v => v.userName.Text).DisposeWith(d);

            }
                );
        }
    }
}
