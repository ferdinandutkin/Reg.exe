using Core.Classes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.Models
{
    public class Position : ReactiveObject, INotifiableEntity
    {
        public int Id
        {
            get; set;
        }


        public Position() { }
        public Position(int start, int end) => (Start, End) = (start, end);

        [Reactive]
        public int Start
        {
            get; set;
        }



        [Reactive]
        public int End
        {
            get; set;
        }
        public void Deconstruct(out int start, out int end) => (start, end) = (Start, End);

        public override int GetHashCode() => base.GetHashCode();

        public override bool Equals(object obj) => obj is Position position && (position.Start, position.End) == (Start, End);


    }

}
