using CW.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для TestingControl.xaml
    /// </summary>
    public partial class TestingControl : ReactiveUserControl<TestingControlViewModel>
    {
        public TestingControl()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.CurrentControlViewModel, v => v.CurrentVmHost.ViewModel).DisposeWith(d);
            });
        }
    }
}
