using Core.Models;
using CW.Dialog;
using CW.Dialog.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows.Input;

namespace CW.ViewModels
{
    public class QuestionCreationControlViewModel 
    {
        [Reactive]
        public InputQuestion Model
        {
            get; set;
        }


        public ICommand OpenTestcaseWindow =>
            ReactiveCommand.Create<object>(OnOpenDialog);


        private void OnOpenDialog(object param)
        {
            bool isNew = false;
            TestCase toEdit;
            if (param is TestCase testCase)
            {
                toEdit = testCase;
            }
            else
            {
                isNew = true;
                toEdit = new TestCase();

            }




 
            var vm = new AddTestCaseDialogViewModel
            {
                AddTestCaseViewModel = new()
                {
                    Model = toEdit

                }
            };

            var result = DialogService.OpenDialog(vm);

            if (result == DialogResult.Ok && isNew)
            {
                Model.TestCases.Add(toEdit);
            }
        }

     
        
    }
}
