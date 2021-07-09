using CW.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для QuestionCreationControl.xaml
    /// </summary>
    public partial class QuestionCreationControl : ReactiveUserControl<QuestionCreationControlViewModel>
    {





        public QuestionCreationControl()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                //  this.BindCommand(ViewModel, vm => vm.OpenDialogCommand, v => v.addTestCase).DisposeWith(d);


                this.Bind(ViewModel, vm => vm.Model.Text, v => v.QuestionText.Text).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Model.Difficulty, v => v.Difficulty.Text).DisposeWith(d);



                //     this.Bind(ViewModel, vm => vm.InputQuestionViewModel.SelectedPosition, v => v.)



            });



        }


    }
}
