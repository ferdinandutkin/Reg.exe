using Core.Models;
using CW.Data;
using CW.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;

namespace CW.ViewModels
{
    public class TestingControlViewModel : ReactiveObject
    {
        static TestingControlViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new TestingControl(), typeof(IViewFor<TestingControlViewModel>));
        }

        [Reactive]
        public AllTestResultsViewModel AllTestResultsViewModel
        {
            get; set;
        } = new()
        {

            Model = UnitOfWorkSingleton.Instance.TestResulsRepository.GetAllWithPropertiesIncluded()
        };

        void OnTestFinish()
        {
            CurrentControlViewModel = TestResultViewModel;


            var res = new TestResult(TestControlViewModel.Model.Answers, new RegexAnswerVerifier());

            res.СompletionTime = DateTime.Now;

            TestResultViewModel.Model = res;




            ServerInteractionSigleton.Instance.CurrentUser.User.Results.Add(res);


            UnitOfWorkSingleton.Instance.UsersRepository.Update(ServerInteractionSigleton.Instance.CurrentUser.User);

        }


        [Reactive]
        public TestControlViewModel TestControlViewModel
        {
            get; set;
        }

        [Reactive]
        public TestResultViewModel TestResultViewModel
        {
            get; set;
        } = new();

        [Reactive]
        public ReactiveObject CurrentControlViewModel
        {
            get; set;
        }

        public TestingControlViewModel()
        {

            CurrentControlViewModel = AllTestResultsViewModel;
            AllTestResultsViewModel.StartNewTest =
                 ReactiveCommand.Create(() =>
                 {
                     TestControlViewModel = new TestControlViewModel(new Test(UnitOfWorkSingleton.Instance.QuestionsRepository.GetAllWithPropertiesIncluded()));

                     TestControlViewModel.Finish = ReactiveCommand.Create(OnTestFinish, TestControlViewModel.CanFinish);
                     CurrentControlViewModel = TestControlViewModel;
                 }


             );

        }
    }
}
