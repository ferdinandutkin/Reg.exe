using Core.Models;
using CW.Data;
using CW.Dialog;
using CW.Dialog.ViewModels;
using CW.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows.Input;

namespace CW.ViewModels
{
    public class QuestionsViewModel : ReactiveObject
    {

        static QuestionsViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new QuestionsView(), typeof(IViewFor<QuestionsViewModel>));
        }


        [Reactive]
        public ISynchronizedCollection<InputQuestion> Model { get; set; }





        public ICommand OpenQuestionWindow =>
            ReactiveCommand.Create<object>(OnOpenDialog);


        private void OnOpenDialog(object param)
        {
            bool isNew = false;
            InputQuestion toEdit;
            if (param is InputQuestion inputQuestion)
            {
                toEdit = inputQuestion;

            }
            else
            {
                isNew = true;
                toEdit = new InputQuestion();

            }





            var vm = new QuestionDialogViewModel()
            {
                IsNew = isNew,
                QuestionCreationControlViewModel = new QuestionCreationControlViewModel
                {

                    Model = toEdit
                }
            };
            var result = DialogService.OpenDialog(vm);

            if (result == DialogResult.Ok && isNew)
            {
                Model.Add(toEdit);
            }
        }
        public QuestionsViewModel()
        {
            //оно жаловалось на неоднозначный вызов пока я методом тыка не подобрал аргументы типа ну ладно
            ///   CanOpenDialog = this.WhenAnyValue<QuestionsViewModel, bool, InputQuestion>(vm => vm.SelectedValue, sv => sv is not null);




            Model = UnitOfWorkSingleton.Instance.QuestionsRepository.GetAllWithPropertiesIncluded();



        }
    }
}
