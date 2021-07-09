
using DynamicData;
using NodeBuilder.Views;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;
using System.Linq;
using System.Reactive.Linq;

namespace NodeBuilder.ViewModels.Nodes
{



    public class AlterNodeViewModel : DynamicNodeViewModel
    {
        static AlterNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new DynamicNodeView(), typeof(IViewFor<AlterNodeViewModel>));
        }

        public ValueNodeInputViewModel<string> Input1 { get; }
        public ValueNodeInputViewModel<string> Input2 { get; }
        public ValueNodeOutputViewModel<string> Output { get; }

        readonly NameGen Gen = new NameGen();





        private void AddNew()
        {

            Inputs.Add(new ValueNodeInputViewModel<string>
            {
                Name = Gen.NextName,
                Editor = new StringValueEditorViewModel(),


            });
        }



        public AlterNodeViewModel()
        {






            Name = "Alter";
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
                Select(_ => string.Join('|', Inputs
             .Items
             .OfType<ValueNodeInputViewModel<string>>()
             .Select(item => item.Value)
             .Where(value => value != null && value.ToString().Trim() != string.Empty)));







            Output = new ValueNodeOutputViewModel<string>
            {
                Name = "A|B",
                Value = output


            };
            Outputs.Add(Output);




        }
    }
}
