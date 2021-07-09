using CW.Data;
using CW.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Windows.Input;

namespace CW.ViewModels
{
    public class WelcomeViewModel : ReactiveObject
    {

        [Reactive]
        public string Message { get; set; } = "Добро пожаловать, ";


        [Reactive]
        public string UserName { get; set; } = ServerInteractionSigleton.Instance?.CurrentUser?.User?.Name;

        public event Action OnLogOut;
        void LogOutAction()
        {
            ServerInteractionSigleton.Instance.LogOut();
            OnLogOut?.Invoke();
        }


        public ICommand LogOutCommand => ReactiveCommand.Create(LogOutAction);
        static WelcomeViewModel()
        {

            Splat.Locator.CurrentMutable.Register(() => new Welcome(), typeof(IViewFor<WelcomeViewModel>));
        }


        public WelcomeViewModel()
        {
            ServerInteractionSigleton.Instance.UserChanged += () =>
            {
                UserName = ServerInteractionSigleton.Instance.CurrentUser.User.Name;
            };
        }


    }
}
