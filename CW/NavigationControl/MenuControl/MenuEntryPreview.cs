using Core.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CWRegexTester
{

    public class MenuEntryPreview : ReactiveObject
    {
        static MenuEntryPreview()
        {
            Splat.Locator.CurrentMutable.Register(() => new MenuEntryPreviewView(), typeof(IViewFor<MenuEntryPreview>));
        }

        [Reactive] 
        public UserRoles MinRoleRequirment { get; set; }

        [Reactive]
        public string Title { get; set; }
    }
}
