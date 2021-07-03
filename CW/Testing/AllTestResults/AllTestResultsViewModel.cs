using Core.Models;
using CW.Data;
using CW.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CW.ViewModels
{
    public class AllTestResultsViewModel : ReactiveObject
    {
        [Reactive]
        public ISynchronizedCollection<TestResult> Model { get; set; }
        static AllTestResultsViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new AllTestResultsView(), typeof(IViewFor<AllTestResultsViewModel>));
        }
        [Reactive]
        public ICommand StartNewTest
        {
            get; set;
        }
    }
}
