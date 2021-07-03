using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using NodeBuilder.ViewModels.Nodes;
using NodeBuilder.Views;
using NodeNetwork;
using NodeNetwork.Toolkit;
using NodeNetwork.Toolkit.NodeList;
using NodeNetwork.ViewModels;
using ReactiveUI;

namespace NodeBuilder.ViewModels
{
    public class NodeBuilderViewModel : ReactiveObject
    {
        static NodeBuilderViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeBuilderControl(), typeof(IViewFor<NodeBuilderViewModel>));
        }

        public NodeListViewModel ListViewModel { get; } = new NodeListViewModel();
        public NetworkViewModel NetworkViewModel { get; } = new NetworkViewModel();

        #region ValueLabel
        private string _valueLabel;
        public string ValueLabel
        {
            get => _valueLabel;
            set => this.RaiseAndSetIfChanged(ref _valueLabel, value);
        } 
        #endregion

        public NodeBuilderViewModel()
        {
          
            ListViewModel.AddNodeType(() => new TextNodeViewModel());
            ListViewModel.AddNodeType(() => new WildcardNodeViewModel());
 
            ListViewModel.AddNodeType(() => new GroupNodeViewModel());
            ListViewModel.AddNodeType(() => new AlterNodeViewModel());
            ListViewModel.AddNodeType(() => new QuantifierNodeViewModel());
            ListViewModel.AddNodeType(() => new AnchorNodeViewModel());
            
            OutputTextNodeViewModel output2 = new OutputTextNodeViewModel();
           
            NetworkViewModel.Nodes.Add(output2);

            NetworkViewModel.Validator = network =>
            {
                bool containsLoops = GraphAlgorithms.FindLoops(network).Any();
                if (containsLoops)
                {
                    return new NetworkValidationResult(false, false, new ErrorMessageViewModel("Network contains loops!"));
                }

             
                return new NetworkValidationResult(true, true, null);
            };
           
            output2.ResultInput.ValueChanged.
                Select(v => (NetworkViewModel.LatestValidation?.IsValid ?? true) ? v : "Error")
                   .BindTo(this, vm => vm.ValueLabel);





        }
    }
}
