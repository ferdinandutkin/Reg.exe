using NodeNetwork.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;

namespace NodeBuilder.ViewModels.Nodes
{
    public class DynamicNodeViewModel : RegexElementNodeViewModel
    {
        protected class NameGen
        {

            string CurrentName { get; set; } = "A";

            public string NextName
            {
                get
                {
                    var ret = CurrentName;
                    GenerateNewName();
                    return ret;

                }
            }

            void GenerateNewName()
            {
                if (CurrentName.Last() == 'Z')
                {
                    CurrentName += 'A';
                }

                else
                {
                    CurrentName = CurrentName[0..^1] + (char)(CurrentName.Last() + 1);

                }

            }

        }

        public ReactiveCommand<Unit, Unit> AddNewCommand { get; set; }


    

        public DynamicNodeViewModel()
        {
           
        }
        
    }
}
