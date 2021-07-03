using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Linq;

namespace CWRegexTester
{
    public class NavigationControlViewModel : ReactiveObject
    {
        [Reactive]
        public MenuControlViewModel MenuControlViewModel
        { get; set; } = new();



        [Reactive]
        public MenuEntryHostViewModel MenuEntryHostViewModel
        { get; set; } = new();


        public void SelectEntry(MenuEntryTemplate entry)
        {
            MenuEntryHostViewModel.SelectedEntry = entry;
        }
        

        void OnMenuItemSelected(MenuEntryPreview menuEntryPreview)
        {
           MenuEntryHostViewModel.SelectedEntry = MenuControlViewModel.MenuEntryTemplates.Items.First(t => t.PreviewInstance == menuEntryPreview) ;
        }

        static NavigationControlViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NavigationControl(), typeof(IViewFor<NavigationControlViewModel>));
        }

        internal void AddEntry(MenuEntryTemplate loginEntry)
        {
            MenuControlViewModel.AddEntry(loginEntry);
        }

        public void AddEntry(Func<object> entryFactory, MenuEntryPreview previewInstance) =>
             MenuControlViewModel.AddEntry(entryFactory, previewInstance);
      
        
        public NavigationControlViewModel()
        {

            MenuControlViewModel.SelectMenuEntry = ReactiveCommand.Create<MenuEntryPreview>(OnMenuItemSelected);


            
           // M

           

        }

       
    }
}
