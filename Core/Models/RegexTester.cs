using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;

namespace Core.Models
{
  
    public class RegexTester : ReactiveObject 
    {


        [Reactive]
        public string Input
        {
            get; set;
        } = string.Empty;

        private readonly ObservableAsPropertyHelper<ObservableCollection<Position>> matches;
        public ObservableCollection<Position> Matches => matches.Value;


        private static bool IsValidRegex(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern)) return false;

            try
            {
                Regex.Match("", pattern);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        private string pattern = string.Empty;
        public string Pattern
        {
            get => pattern;
            set
            {
                if (IsValidRegex(value))
                {
                    pattern = value;
                    this.RaisePropertyChanged();
                }

            }
            
        }
        
      
        public RegexTester()
        {
            matches = this.WhenAnyValue(m => m.Input, m => m.Pattern)
                             .Select(_ =>                              
                             new ObservableCollection<Position>(Regex.Matches(Input, Pattern)
                             .Select(match => new Position(match.Index, match.Index + match.Length - 1))))
                            
                             .ToProperty(this, m => m.Matches);

           

        }



    }
}
