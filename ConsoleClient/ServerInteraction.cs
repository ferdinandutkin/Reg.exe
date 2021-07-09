using Core.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;

namespace ConsoleClient
{
    public partial class ServerInteraction
    {
        readonly HttpClient client;


        public void LogOut()
        {
            this.token = new JwtSecurityToken();
            client.DefaultRequestHeaders.Remove("Authorization");
            CurrentUser = new();
        }
     
        public event Action UserChanged;


        [Reactive]
        public CurrentUserModel CurrentUser
        {
            get;
            private set;
            
        } = new();
        private JwtSecurityToken Token
        {
            get => token;
            set
            {

                client.DefaultRequestHeaders.Remove("Authorization");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {value.RawData}");


                int userId = int.Parse(value.Claims.First().Value);  
                CurrentUser = new()
                {
                    Roles = value
                      .Claims
                      .Where(claim => claim.Type == "role") //почему у claimtypes.role другое значение??
                      .Select(claim => Enum.Parse<UserRoles>(claim.Value))
                      .ToArray(),
                    User =
                    UserControllerCallsWrapper.Get(userId),
                };

                UserChanged?.Invoke();

                CurrentUser.User.Results = UserControllerCallsWrapper.GetProperty(userId, "Results")
                    as ObservableCollection<TestResult> ?? new ObservableCollection<TestResult>();




                token = value;
            }
        }


        private JwtSecurityToken token;

        public ServerInteraction(string address)
        {
            client = new HttpClient() { BaseAddress = new Uri(address) };

            QuestionControllerCallsWrapper = new(this, "Question");

            ResultsControllerCallsWrapper = new(this, "Results");

            ReferenceEntryControllerCallsWrapper = new(this, "ReferenceEntry");

            UserControllerCallsWrapper = new(this, "User");

            TestCaseControllerCallsWrapper = new(this, "TestCase");

            PositionsControllerCallsWrapper = new(this, "Positions");


            User = new(this);



        }

        public readonly UserManager User;


        public readonly ApiControllerWithPropertyAccessInteraction<User>
            UserControllerCallsWrapper;


        public readonly ApiControllerWithPropertyAccessInteraction<TestResult>
            ResultsControllerCallsWrapper;

        public readonly ApiControllerWithPropertyAccessInteraction<InputQuestion>
            QuestionControllerCallsWrapper;

        public readonly ApiControllerWithPropertyAccessInteraction<TestCase>
            TestCaseControllerCallsWrapper;

        public readonly ApiControllerWithPropertyAccessInteraction<Position>
           PositionsControllerCallsWrapper;

        public readonly ApiControllerWithPropertyAccessInteraction<ReferenceEntry>
          ReferenceEntryControllerCallsWrapper;


    }
}
