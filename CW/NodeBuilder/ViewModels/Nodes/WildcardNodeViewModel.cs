using DynamicData;
using NodeBuilder.Models;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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





            var output = Observable.CombineLatest(
                            this.WhenAnyValue(vm => vm.Input),
                            Observable.FromEventPattern(ValueEditor.Value, nameof(INotifyPropertyChanged.PropertyChanged)),
                           this.WhenAnyValue(vm => vm.ValueEditor.Value),
                           (a, b, c) => c
                        ).Select(_ => ValueEditor?.Value?.GetValue());


            Output = new ValueNodeOutputViewModel<string>
            {

                Editor = ValueEditor,
                Value = output







            };




            Outputs.Add(Output);
        }
    }
}
