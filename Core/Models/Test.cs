using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.Models
{
    public class Test : ReactiveObject, IList<Answer>
    {
      
        [Reactive]
        public ObservableCollection<Answer> Answers
        {
            get; set;
        }

        public int Count => ((ICollection<Answer>)Answers).Count;

        public bool IsReadOnly => ((ICollection<Answer>)Answers).IsReadOnly;

        public Answer this[int index] { get => ((IList<Answer>)Answers)[index]; set => ((IList<Answer>)Answers)[index] = value; }

        

        public Test(IEnumerable<InputQuestion> questions)
        {
            Answers = new ObservableCollection<Answer>(questions.Select(q => new Answer() { Question = q }));
        }

        public IEnumerator<Answer> GetEnumerator() => Answers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => (Answers as IEnumerable).GetEnumerator();

        public int IndexOf(Answer item) => Answers.IndexOf(item);

        public void Insert(int index, Answer item) => Answers.Insert(index, item);

        public void RemoveAt(int index) => Answers.RemoveAt(index);

        public void Add(Answer item) => Answers.Add(item);

        public void Clear() => Answers.Clear();

        public bool Contains(Answer item) => Answers.Contains(item);

        public void CopyTo(Answer[] array, int arrayIndex) => ((ICollection<Answer>)Answers).CopyTo(array, arrayIndex);

        public bool Remove(Answer item) => Answers.Remove(item);
    }
}
