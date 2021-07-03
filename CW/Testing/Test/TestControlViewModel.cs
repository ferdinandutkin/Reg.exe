using Core.Models;
using CW.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;

namespace CW.ViewModels
{
    public class TestControlViewModel : ReactiveObject
    {

        static TestControlViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new TestControl(), typeof(IViewFor<TestControlViewModel>));
        }
        [Reactive]
        public Test Model
        {
            get; set;
        }

        

         [Reactive]
        private int CurrentAnswerNumber
        {
            get; set;
        }

       
        public Answer CurrentAnswer => currentAnswer.Value;

        private readonly IObservable<bool> CanMoveForward;
        private readonly IObservable<bool> CanMoveBackwards;
        public readonly IObservable<bool> CanFinish;


        private readonly ObservableAsPropertyHelper<Answer> currentAnswer;

        public TestControlViewModel(Test model)
        {

            this.Model = model;
            currentAnswer = this.WhenAnyValue(vm => vm.CurrentAnswerNumber)
                   .Select(num => Model?[num]).ToProperty(this, vm => vm.CurrentAnswer);
                  
           
           CanMoveForward = this.WhenAnyValue(vm =>
            vm.CurrentAnswerNumber, CurrentAnswerNumber =>
   CurrentAnswerNumber < Model.Count - 1);

            CanMoveBackwards = this.WhenAnyValue(vm =>
           vm.CurrentAnswerNumber, CurrentAnswerNumber =>
  CurrentAnswerNumber > 0);

            CanFinish = this.WhenAnyValue(vm =>
           vm.CurrentAnswerNumber, CurrentAnswerNumber =>
  CurrentAnswerNumber == Model.Count - 1);


        }

        
        [Reactive]
        public ICommand Finish
        {
            get; set;
        }

        public ICommand NextAnswer => ReactiveCommand.Create(() => CurrentAnswerNumber++, CanMoveForward);
     

        public ICommand PrevAnswer => ReactiveCommand.Create(() => CurrentAnswerNumber--, CanMoveBackwards);
       
    }
}
