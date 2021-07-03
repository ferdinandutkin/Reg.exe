using Core.Classes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.Models
{


   


    public class TestResult : ReactiveObject, INotifiableEntity
    {

        public int Id
        {
            get; set;
        }

        [Reactive]
        public User User
        {
            get; set;
        }

        [Reactive]
        public DateTime СompletionTime
        {
            get; set;
        }
        [Reactive]
        public ObservableCollection<AnswerResult> Results
        {
            get; set;
        } = new();


        [Reactive]
        public int Score
        {
            get;
            set;
        }
         

        

       
        public AnswerResult this[int index]
        { 
            get => Results[index];
            set => Results[index] = value; 
        }
        public TestResult()
        {

        }
        public TestResult(IEnumerable<Answer> answers, IAnswerVerifier verifier)
        {
            Results = new(answers.Select(answer => new AnswerResult() { Answer = answer, Result = verifier.IsValid(answer) }));
            Score = Results.Where(res => res.Result).Sum(res => res.Answer.Question.Difficulty);
        }
            
      
        public void Insert(int index, AnswerResult item) => Results.Insert(index, item);

        public void RemoveAt(int index) => Results.RemoveAt(index);

        public void Add(AnswerResult item) => Results.Add(item);

        public void Clear() => Results.Clear();

       

       
    }
}
