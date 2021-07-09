using CW.Data;
using CW.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CW.ViewModels
{
    public class MainLoginViewModel : ReactiveObject
    {
        readonly LoginViewModel LoginViewModel = new();

        readonly WelcomeViewModel WelcomeViewModel = new();

        [Reactive]
        public ReactiveObject CurrentViewModel
        {
            get; set;
        }

        public MainLoginViewModel()
        {
            LoginViewModel.OnLogIn += () =>
            {
                CurrentViewModel = WelcomeViewModel;
            };

            WelcomeViewModel.OnLogOut += () =>
            {
                CurrentViewModel = LoginViewModel;
            };

            if (ServerInteractionSigleton.Instance.CurrentUser.Roles.Length == 0)
            {
                CurrentViewModel = LoginViewModel;
            }
            else CurrentViewModel = WelcomeViewModel;


        }
        static MainLoginViewModel()
        {

            Splat.Locator.CurrentMutable.Register(() => new MainLoginControl(), typeof(IViewFor<MainLoginViewModel>));
        }

    }
}
