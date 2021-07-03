using Core.Classes;
using Core.Models;
using CW.Data;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive.Linq;

namespace CWRegexTester
{

    public  class ReferenceControlViewModel : ReactiveObject
    {


        [Reactive]
        public string Title
        {
            get; set;
        }


        [Reactive]
        public string EmptyLabel
        { 
            get; set;
        }
        public ISourceList<ReferenceEntry> Entries { get; } = new SourceList<ReferenceEntry>();

        public IObservableList<ReferenceEntry> VisibleEntries { get; }

        [Reactive]
        public string SearchQuery
        {
            get;
            set;
        }


        public ReferenceControlViewModel()
        {
            Title = "Reference";
            EmptyLabel = "No info found.";

            var entries =  (UnitOfWorkSingleton.Instance.RefenceRepository as IRepository<ReferenceEntry>).GetAll();

            
            Entries.AddRange(entries);

            var onQueryChanged = this.WhenAnyValue(vm => vm.SearchQuery)
                .Throttle(TimeSpan.FromMilliseconds(200), RxApp.MainThreadScheduler)
                .Publish();
            onQueryChanged.Connect();
            VisibleEntries = Entries.Connect()
                .AutoRefreshOnObservable(_ => onQueryChanged)
                 

                .Filter(n => (n.Token ?? "").ToUpper().Contains(SearchQuery?.ToUpper() ?? "")
                || (n.Info ?? "").ToUpper().Contains(SearchQuery?.ToUpper() ?? ""))
                .AsObservableList();
        }

        
        public void AddEntry(ReferenceEntry entry)  
        {
            Entries.Add(entry);
        }
    }
}
