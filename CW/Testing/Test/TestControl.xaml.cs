using CW.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для TestControl.xaml
    /// </summary>
    public partial class TestControl : ReactiveUserControl<TestControlViewModel>
    {
        public TestControl()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, vm => vm.CurrentAnswer, v => v.Answer.ViewModel,
                    answer => new AnswerViewModel() { Model = answer },
                    answerVM => answerVM.Model
                    ).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.PrevAnswer, v => v.Prev).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.NextAnswer, v => v.Next).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.Finish, v => v.Finish).DisposeWith(d);
            }
                );
        }
    }
}
