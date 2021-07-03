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
                this.Bind(ViewModel, vm => vm.CurrentAnswer, v => v.answer.ViewModel, 
                    answer => new AnswerViewModel() { Model = answer },
                    answerVM => answerVM.Model
                    ).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.PrevAnswer, v => v.prev).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.NextAnswer, v => v.next).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.Finish, v => v.finish).DisposeWith(d);
            }
                );
        }
    }
}
