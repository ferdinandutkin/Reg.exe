using CW.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для MainLoginControl.xaml
    /// </summary>
    public partial class MainLoginControl : ReactiveUserControl<MainLoginViewModel>
    {
        public MainLoginControl()
        {
            InitializeComponent();

            this.WhenActivated(d => this.Bind(ViewModel, vm => vm.CurrentViewModel, v => v.Host.ViewModel).DisposeWith(d));
        }
    }
}
