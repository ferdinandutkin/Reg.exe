using ReactiveUI;
using System.Reactive.Disposables;

namespace CWRegexTester
{
    /// <summary>
    /// Логика взаимодействия для NavigationControl.xaml
    /// </summary>
    public partial class NavigationControl : ReactiveUserControl<NavigationControlViewModel>
    {
        public NavigationControl()
        {
            
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, vm => vm.MenuControlViewModel, v => v.menu.ViewModel).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.MenuEntryHostViewModel, v => v.host.ViewModel).DisposeWith(d);
            });
        }
    }
}
