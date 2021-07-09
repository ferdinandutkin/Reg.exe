using DynamicData;
using NodeBuilder.Views;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;
using System.Linq;
using System.Reactive.Linq;

namespace NodeBuilder.ViewModels.Nodes
{
    public class GroupNodeViewModel : DynamicNodeViewModel
    {
        static GroupNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new DynamicNodeView(), typeof(IViewFor<GroupNodeViewModel>));
        }

        private readonly NameGen Gen = new NameGen();

        public ValueNodeInputViewModel<string> Input1 { get; }
        public ValueNodeInputViewModel<string> Input2 { get; }
        public ValueNodeOutputViewModel<string> Output { get; }


        private void AddNew()
        {

            Inputs.Add(new ValueNodeInputViewModel<string>
            {
                Name = Gen.NextName,
                Editor = new StringValueEditorViewModel(),


            });
        }


        public GroupNodeViewModel()
        {
            Name = "Group";

            AddNewCommand = ReactiveCommand.Create(AddNew);

            Input1 = new ValueNodeInputViewModel<string>
            {
                Name = Gen.NextName,
                Editor = new StringValueEditorViewModel()
            };
            Inputs.Add(Input1);

            Input2 = new ValueNodeInputViewModel<string>
            {
                Name = Gen.NextName,
                Editor = new StringValueEditorViewModel()
            };
            Inputs.Add(Input2);


            var output = Inputs.Connect().
                WhenValueChanged(item => ((ValueNodeInputViewModel<string>)item).Value).
                Select(_ =>
                string.Concat('(',
                string.Concat(Inputs
             .Items
             .OfType<ValueNodeInputViewModel<string>>()
             .Select(item => item.Value)
             .Where(value => value != null && value.ToString().Trim() != string.Empty)),
                ')'));




            Output = new ValueNodeOutputViewModel<string>
            {
                Name = "(AB)",
                Value = output
            };
            Outputs.Add(Output);
        }
    }
}
