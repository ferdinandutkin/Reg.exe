using CW.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для AllTestResultsView.xaml
    /// </summary>
    public partial class AllTestResultsView : ReactiveUserControl<AllTestResultsViewModel>
    {
        public AllTestResultsView()
        {
            InitializeComponent();
            this.WhenActivated(
                d =>
                {
                 
                    this.OneWayBind(ViewModel, vm => vm.Model, v => v.results.ItemsSource).DisposeWith(d);
                    this.BindCommand(ViewModel, vm => vm.StartNewTest, v => v.newTest).DisposeWith(d);
                });
        }
    }
}
