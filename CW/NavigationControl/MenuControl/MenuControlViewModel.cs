using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Windows.Input;

namespace CWRegexTester
{

    public class MenuControlViewModel : ReactiveObject
    {



        [Reactive]
        public string Title
        {
            get;
            set;
        }


        [Reactive]
        public ICommand SelectMenuEntry
        { get; set; }


        public ISourceList<MenuEntryTemplate> MenuEntryTemplates { get; } = new SourceList<MenuEntryTemplate>();


        public IObservableList<MenuEntryPreview> VisibleEntries { get; }




        public MenuControlViewModel()
        {
            Title = "reg.exe";


            VisibleEntries = MenuEntryTemplates.Connect()

                .Transform(t => t.PreviewInstance)

                .AsObservableList();
        }


        public void AddEntry(MenuEntryTemplate template)
        {
            MenuEntryTemplates.Add(template);
        }
        public void AddEntry(Func<object> entryFactory, MenuEntryPreview previewInstance)
        {
            MenuEntryTemplates.Add(new MenuEntryTemplate(entryFactory, previewInstance));
        }
    }
}
