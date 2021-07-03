using DynamicData;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace NodeBuilder.ViewModels.Nodes
{
    class WildcardNodeViewModel : RegexElementNodeViewModel
    {

        static WildcardNodeViewModel()
        {

            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<WildcardNodeViewModel>));
        }

        public WildcardValueEditorViewModel ValueEditor { get; } = new WildcardValueEditorViewModel();

        public ValueNodeOutputViewModel<string> Output { get; }


        public ValueNodeInputViewModel<string> Input { get; }

        public WildcardNodeViewModel()
        {
            Name = "Wildcard";


            Input = new ValueNodeInputViewModel<string>();

            Inputs.Add(Input);




            Output = new ValueNodeOutputViewModel<string>
            {

                Editor = ValueEditor,

              
            };




            Outputs.Add(Output);
        }
    }
}
