using Core.Classes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.Models
{

    public enum Category 
    {
        CharacterClasses,
        Anchors,
        GroupsLokaround,
        QuantifiersAlternation
    }
    public class ReferenceEntry : ReactiveObject, INotifiableEntity
    {
        [Reactive]
        public Category Category
        {
            get; set;
        }


        public int Id
        {
            get; set;
        }

        [Reactive]
        public string Token
        {
            get; set;
        }

        [Reactive]
        public string Info
        {
            get; set;
        }

    }
}
