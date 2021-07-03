using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NodeBuilder.Models
{


    public class Range : ReactiveObject
    {
        [Reactive]
        public int Start
        { 
            get; 
            set; 
        }

        [Reactive]
        public int End
        {
            get;
            set;
        }
    }

   

    public class RegexTextModel : ReactiveObject
    {

        [Reactive]
        public string Text
        {
            get;
            set;
        }

        [Reactive]
        public bool EscapeSpecial
        {
            get;
            set;
        }


        public string GetValue() => EscapeSpecial ? Regex.Escape(Text) : Text;


    }
}
