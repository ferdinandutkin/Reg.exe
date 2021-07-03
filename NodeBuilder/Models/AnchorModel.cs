using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NodeBuilder.Models
{


    public enum AnchorType
    {
        StartLine,
        EndLine,
        WordBoundary,
        NotWordBoundary,
    }
    public class AnchorModel : ReactiveObject
    {
 
        [Reactive]
        public AnchorType AnchorType
        {
            get;
            set;
        }

        public string GetValue() =>
            AnchorType switch
            {
                AnchorType.StartLine => "^",
                AnchorType.EndLine => "$",
                AnchorType.WordBoundary => @"\b",
                AnchorType.NotWordBoundary => @"\B",
                _ => "",
            };
      

       
    }
}
