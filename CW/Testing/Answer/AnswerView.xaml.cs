using CW.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для AnswerView.xaml
    /// </summary>
    public partial class AnswerView : ReactiveUserControl<AnswerViewModel>
    {
        

        
        public AnswerView()
        {
            InitializeComponent();

            this.WhenActivated(
                d =>
                {
                    this.Bind(ViewModel, vm => vm.Model.Question, v => v.question.ViewModel,
                        qm => new QuestionViewModel() { Model = qm },
                        qvm => qvm.Model).DisposeWith(d);

                    

                    this.Bind(ViewModel, vm => vm.Model.Input, v => v.answer.Text).DisposeWith(d);
                }
                );

        }
    }
}
