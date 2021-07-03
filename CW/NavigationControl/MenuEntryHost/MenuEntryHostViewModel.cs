using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows;

namespace CWRegexTester
{
    public class MenuEntryHostViewModel : ReactiveObject
    {
        static MenuEntryHostViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new MenuEntryHostView(), typeof(IViewFor<MenuEntryHostView>));
  
        }


        
        public class DraggingTemplate : ReactiveObject
        {

            [Reactive]
            public MenuEntryTemplate Template
            {
                get;
                set;
            }

            [Reactive]
            public Point Position
            {
                get;
                set;
            }
        }


        [Reactive]
        public DraggingTemplate PendingEntry
        {
            get;
            set;
        } = new();


        //выбранное даблкликом в меню (без негатива)
        [Reactive]
        public MenuEntryTemplate SelectedEntry
        {
            get;
            set;
        }



        public MenuEntryHostViewModel()
        {


        }
    }
    
}

