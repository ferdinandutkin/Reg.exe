using DynamicData;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.Views;
using ReactiveUI;
using System.Reactive.Linq;

namespace NodeBuilder.ViewModels.Nodes
{





    class AnchorNodeViewModel : RegexElementNodeViewModel
    {
        static AnchorNodeViewModel()
        {

            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<AnchorNodeViewModel>));
        }

        public AnchorValueEditorViewModel ValueEditor { get; } = new AnchorValueEditorViewModel();

        public ValueNodeOutputViewModel<string> Output { get; }


        public ValueNodeInputViewModel<string> Input { get; }

        public AnchorNodeViewModel()
        {
            Name = "Anchor";


            Input = new ValueNodeInputViewModel<string>();

            Inputs.Add(Input);




            Output = new ValueNodeOutputViewModel<string>
            {

                Editor = ValueEditor,
                Value = this.WhenAnyValue(vm => vm.Input.Value, vm => vm.ValueEditor.Value.AnchorType)
                .Select(_ => Input.Value + ValueEditor.Value.GetValue())


            };

            Outputs.Add(Output);
        }
    }
}
