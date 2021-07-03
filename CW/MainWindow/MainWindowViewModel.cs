using CW.ViewModels;
using CWRegexTester;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW.ViewModels
{
    class MainWindowViewModel : ReactiveObject
    {
        [Reactive]
        public NavigationControlViewModel NavigationControlViewModel { get; set; }

        public MainWindowViewModel()
        {

            NavigationControlViewModel = new NavigationControlViewModel();

            
          


            NavigationControlViewModel.AddEntry(() => new TestingControlViewModel(), new MenuEntryPreview() { Title = "Тестирование", MinRoleRequirment = Core.Models.UserRoles.User });

            NavigationControlViewModel.AddEntry(() => new RegexTestingControlViewModel(), new MenuEntryPreview() { Title = "Тестирование регулярных выражений", MinRoleRequirment = Core.Models.UserRoles.Anonymous });

            NavigationControlViewModel.AddEntry(() => new QuestionsViewModel(), new MenuEntryPreview() { Title = "Редактор вопросов", MinRoleRequirment = Core.Models.UserRoles.Admin });

            NavigationControlViewModel.AddEntry(() => new ReferenceEditingControlViewModel(), new MenuEntryPreview() { Title = "Редактор справочной информации", MinRoleRequirment = Core.Models.UserRoles.Admin });

            NavigationControlViewModel.AddEntry(() => new NodeBuilder.ViewModels.NodeBuilderViewModel(), new() { Title = "Конструктор", MinRoleRequirment = Core.Models.UserRoles.Anonymous });



            var loginEntry = new MenuEntryTemplate(() => new MainLoginViewModel(), new MenuEntryPreview() { Title = "Пользователь" });


            NavigationControlViewModel.AddEntry(loginEntry);
            NavigationControlViewModel.SelectEntry(loginEntry);
        }
    }
}
