using CW.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для ReferenceEditingControl.xaml
    /// </summary>
    public partial class ReferenceEditingControl : ReactiveUserControl<ReferenceEditingControlViewModel>
    {
        public ReferenceEditingControl()
        {
            InitializeComponent();
            this.WhenActivated(
                d =>
                {
                    this.OneWayBind(ViewModel, vm => vm.Model, v => v.Grid.ItemsSource).DisposeWith(d);
                }
                );
        }
    }
}
