using CW.ViewModels;
using ReactiveUI;
using System.Linq;
using System.Reactive.Disposables;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для TestResultView.xaml
    /// </summary>
    public partial class TestResultView : ReactiveUserControl<TestResultViewModel>
    {
        public TestResultView()
        {
            InitializeComponent();
            this.WhenActivated(
                d =>
                {
                    this.OneWayBind(ViewModel, vm => vm.Model, v => v.Results.ItemsSource,
                        results => results.Results.Select(results => new AnswerResultViewModel() { Model = results })).DisposeWith(d);


                    this.OneWayBind(ViewModel, vm => vm.Model.Score, v => v.Score.Text).DisposeWith(d);

                });
        }
    }
}

