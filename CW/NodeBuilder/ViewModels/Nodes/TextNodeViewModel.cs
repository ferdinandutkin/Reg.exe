using DynamicData;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using System.Reactive.Linq;

namespace NodeBuilder.ViewModels.Nodes
{






    class TextNodeViewModel : NodeViewModel
    {
        static TextNodeViewModel()
        {

            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<TextNodeViewModel>));
        }

        public RegexTextValueEditorViewModel ValueEditor { get; } = new RegexTextValueEditorViewModel();

        public ValueNodeOutputViewModel<string> Output { get; }


        public ValueNodeInputViewModel<string> Input { get; }

        public TextNodeViewModel()
        {
            Name = "Text";


            Input = new ValueNodeInputViewModel<string>();

            Inputs.Add(Input);




            Output = new ValueNodeOutputViewModel<string>
            {

                Editor = ValueEditor,

                Value = this.WhenAnyValue(vm => vm.Input.Value, vm => vm.ValueEditor.Value.Text, vm => vm.ValueEditor.Value.EscapeSpecial)
              .Select(_ => Input.Value + ValueEditor?.Value?.GetValue())
            };




            Outputs.Add(Output);
        }
    }
}
