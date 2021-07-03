using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NodeBuilder.Models
{

    public enum WildcardType
    {
        Everything,
        WordCharacters,
        Letters,
        Digits,
        Whitespace,
        Custom
    }
    public class WildcardModel : ReactiveObject
    {

        [Reactive]
        public bool MatchNewlines
        {
            get; 
            set;
        }

        [Reactive]
        public WildcardType WildcardType
        {
            get;
            set;
        }


        [Reactive]
        public bool Invert
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
        public bool AllowWhitespace { get; set; }

        [Reactive]
        public bool  AllowUppercase { get; set; }

        [Reactive]
        public bool  AllowLowercase { get; set; }

        [Reactive]
        public bool AllowDigits { get; set; }

        [Reactive]
        public bool AllowUnderscore { get; set; }


        public string GetValue() =>
             (WildcardType, Invert) switch
             {
                 (WildcardType.Everything, _) => MatchNewlines ? @"[\s\S]" : ".",
                 (WildcardType.WordCharacters, false) => "\\w",
                 (WildcardType.WordCharacters, true) => "\\W",
                 (WildcardType.Letters, false) => "[a-zA-Z]",
                 (WildcardType.Letters, true) => "[^a-zA-Z]",
                 (WildcardType.Digits, false) => @"\d",
                 (WildcardType.Digits, true) => @"\D",
                 (WildcardType.Whitespace, false) => @"\s",
                 (WildcardType.Whitespace, true) => @"\S",
                 (WildcardType.Custom, _) => GetContentsCustom(Invert),
                 _ => ".",
             };
 


        private string GetContentsCustom(bool invert)
        {
            var inputs = (
                i: invert,
                w: AllowWhitespace,
                L: AllowUppercase,
                l: AllowLowercase,
                d: AllowDigits,
                u: AllowUnderscore
                );

            return inputs switch
            {
               
                (true, false, false, false, false, false) => "[]",
                (true, false, false, false, true, false) => @"\D",
                (true, true, false, false, false, false) => @"\S",
                (true, false, true, true, true, true) => @"\W",

               
                (false, true, true, true, true, true) => ".",
                (false, false, false, false, false, true) => "_",
                (false, false, false, false, true, false) => @"\d",
                (false, true, false, false, false, false) => @"\s",
                (false, false, true, true, true, true) => @"\w",

              
                _ => GetClassContents(invert: invert, w: inputs.w, L: inputs.L, l: inputs.l, d: inputs.d, u: inputs.u),
            };



        }

        private static string GetClassContents(bool invert, bool w, bool L, bool l, bool d, bool u)
        {
            string result = invert ? "[^" : "[";
            result += w ? @"\s" : "";

            if (L && l && d && u)
            {
                result += @"\w";
            }
            else
            {
                result +=
                    (L ? "A-Z" : "") +
                    (l ? "a-z" : "") +
                    (d ? @"\d" : "") +
                    (u ? "_" : "");
            }
            return result + "]";
        }

    }
}
