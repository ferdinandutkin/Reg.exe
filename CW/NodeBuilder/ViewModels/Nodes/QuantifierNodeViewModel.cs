using DynamicData;
using NodeBuilder.Views;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
 
using System.Windows;

namespace NodeBuilder.ViewModels.Nodes
{

  
    

    
    class QuantifierNodeViewModel : RegexElementNodeViewModel
    {
        static QuantifierNodeViewModel()
        {
            
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<QuantifierNodeViewModel>));
        }

        public QuantifierValueEditorViewModel ValueEditor { get; } = new QuantifierValueEditorViewModel();

        public ValueNodeOutputViewModel<string> Output { get; }


        public ValueNodeInputViewModel<string> Input { get; }

        public QuantifierNodeViewModel()
        {
            Name = "Quantifier";
            

            Input = new ValueNodeInputViewModel<string>();
      
            Inputs.Add(Input);




            Output = new ValueNodeOutputViewModel<string>
            {
           
                Editor = ValueEditor,

                Value = this.WhenAnyValue(vm => vm.Input.Value,
                vm => vm.ValueEditor.Value.SearchType, vm => vm.ValueEditor.Value.RepetitionsType,
                vm => vm.ValueEditor.Value.Range.Start, vm => vm.ValueEditor.Value.Range.End, vm => vm.ValueEditor.Value.Number)
              .Select(_ =>  ValueEditor.Value.GetValue(Input.Value) ?? "")
            };
        



            Outputs.Add(Output);
        }
    }
}
