using CW.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

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

                this.BindCommand(ViewModel, vm => vm.LogOutCommand, v => v.LogOut).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.Message, v => v.WelcomeMessage.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.UserName, v => v.UserName.Text).DisposeWith(d);

            }
                );
        }
    }
}
