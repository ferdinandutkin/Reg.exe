using CW.Data;
using CW.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Windows.Input;

namespace CW.ViewModels
{
    public class LoginViewModel : ReactiveObject
    {

        [Reactive]
        public string Login { get; set; }

        [Reactive]
        public string Errors { get; set; }
        [Reactive]
        public string Password { get; set; }

        public ICommand LoginAsAnonymousCommand => ReactiveCommand.Create(() =>
        {
            var res = ServerInteractionSigleton.Instance.User.LoginAsAnonymous();
            if (res)
            {
                OnLoginAction();
            }
            else Errors = "Что-то с подключением";
        }
        );


        [Reactive]
        public bool IsFailedToLogin
        {
            get; set;
        }


        [Reactive]
        public bool IsFailedToRegister
        {
            get; set;
        }

        void OnLoginAction()
        {
            OnLogIn?.Invoke();
            Password = string.Empty;
            Login = string.Empty;
            Errors = string.Empty;
        }


        static LoginViewModel()
        {

            Splat.Locator.CurrentMutable.Register(() => new LoginControl(), typeof(IViewFor<LoginViewModel>));
        }

        public event Action OnLogIn;

        public ICommand LoginCommand => ReactiveCommand.Create(
            () =>
            {
                var res = ServerInteractionSigleton.Instance.User.Login(Login, Password);
                if (res)
                {
                    OnLoginAction();

                }
                else Errors = "Не удалось войти";
            }
            );


        readonly IObservable<bool> CanRegister;
        public ICommand Register => ReactiveCommand.Create(() =>
        {
            bool res = false;
            try
            {
                res = ServerInteractionSigleton.Instance.User.Register(Login, Password);

                if (res != true)
                {
                    IsAlreadyRegistered = ServerInteractionSigleton.Instance.User.IsRegistered(Login);
                    if (IsAlreadyRegistered)
                    {
                        Errors = "Такой пользователь уже существует";
                    }
                }

            }

            catch (Exception)
            {
                IsBadConnection = true;
            }
            finally
            {
                IsFailedToRegister = !res;
            }

            if (res)
            {
                Errors = "Уcпешная регистрация";
            }

        }, CanRegister);




        [Reactive]
        public bool IsAlreadyRegistered
        {
            get; set;
        }

        [Reactive]
        public bool IsBadConnection
        {
            get; set;
        }


        public LoginViewModel() =>
            //я не знаю что с ним но если он настаивает
            CanRegister = this.WhenAnyValue((vm => vm.Login), vm => vm.Password,
                (login, password) => login is not null && password is not null &&
                                     !string.IsNullOrWhiteSpace(login.Trim()) && !(password.Contains(' ')) &&
                                     !(password.Length > 6));
    }
}
