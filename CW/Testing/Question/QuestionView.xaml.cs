
using CW.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для QuestionView.xaml
    /// </summary>
    public partial class QuestionView : ReactiveUserControl<QuestionViewModel>
    {



        public QuestionView()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.Model.Name, v => v.questionName.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.Model.Text, v => v.text.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.Model.Difficulty, v => v.difficulty.Text).DisposeWith(d);
            });
        }
    }
}
