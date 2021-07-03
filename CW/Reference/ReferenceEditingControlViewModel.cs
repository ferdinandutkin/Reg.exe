using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Classes;
using Core.Models;
using CW.Data;
using CW.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CW.ViewModels
{
    public class ReferenceEditingControlViewModel : ReactiveObject
    {
        static ReferenceEditingControlViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new ReferenceEditingControl(), typeof(IViewFor<ReferenceEditingControlViewModel>));
        }

        public ReferenceEditingControlViewModel()
        {
            Model = UnitOfWorkSingleton.Instance.RefenceRepository.GetAllWithPropertiesIncluded();
        }



        [Reactive]
        public ISynchronizedCollection<ReferenceEntry> Model
        {
            get; set;
        }
    }
}
