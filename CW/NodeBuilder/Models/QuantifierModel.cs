using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NodeBuilder.Models
{

    public enum RepetitionsType
    {
        One, Number, Range, ZeroOrOne, ZeroOrMore, OneOrMore
    }

    public enum SearchType
    {
        Greedy, Lazy, Possessive
    }

    public class QuantifierModel : ReactiveObject
    {





        public string GetValue(string input)
        {

            string suffix = string.Empty;
            string prefix = string.Empty;

            suffix += GetSuffix();


            if (SearchType == SearchType.Lazy)
            {
                suffix += "?";
            }
            else if (SearchType == SearchType.Possessive)
            {
                suffix += ")";
                prefix = "(?>" + prefix;
            }

            return string.Concat(prefix, input, suffix);
        }

        private string GetSuffix()
        {
            return RepetitionsType switch
            {
                RepetitionsType.One => "",
                RepetitionsType.ZeroOrMore => "*",
                RepetitionsType.OneOrMore => "+",
                RepetitionsType.ZeroOrOne => "?",
                RepetitionsType.Number => $"{{{Number}}}",
                RepetitionsType.Range => $"{{{Range.Start},{ Range.End}}}",
                _ => "",
            };
        }
        [Reactive]
        public RepetitionsType RepetitionsType
        {
            get;
            set;
        }

        [Reactive]
        public SearchType SearchType
        {
            get;
            set;
        }


        [Reactive]
        public int Number
        {
            get;
            set;
        }


        [Reactive]
        public Range Range
        {
            get;
            set;
        } = new Range();

    }
}
