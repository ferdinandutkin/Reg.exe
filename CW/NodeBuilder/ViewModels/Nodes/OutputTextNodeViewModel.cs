using DynamicData;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.Views;
using ReactiveUI;

namespace NodeBuilder.ViewModels.Nodes
{
    public class OutputTextNodeViewModel : RegexElementNodeViewModel
    {
        static OutputTextNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<OutputTextNodeViewModel>));
        }

        public ValueNodeInputViewModel<string> ResultInput { get; }

        //public ValueNodeOutputViewModel<string> Output { get; }

        public OutputTextNodeViewModel()
        {
            Name = "Output";

            CanBeRemovedByUser = false;

            ResultInput = new ValueNodeInputViewModel<string>
            {
                Name = "Value",

            };
            Inputs.Add(ResultInput);





        }
    }
}
