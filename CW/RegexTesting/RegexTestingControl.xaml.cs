using CW.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для RegexTestingView.xaml
    /// </summary>
    public partial class RegexTestingControl : ReactiveUserControl<RegexTestingControlViewModel>
    {
        public RegexTestingControl()
        {
            InitializeComponent();



            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.CurrentControlViewModel, v => v.CurrentVmHost.ViewModel).DisposeWith(d);







            });
        }
    }
}
