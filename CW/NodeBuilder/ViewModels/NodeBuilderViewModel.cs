using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using NodeBuilder.ViewModels.Nodes;
using NodeBuilder.Views;
using NodeNetwork;
using NodeNetwork.Toolkit;
using NodeNetwork.Toolkit.NodeList;
using NodeNetwork.ViewModels;
using ReactiveUI;
using Splat;

namespace NodeBuilder.ViewModels
{
    public class NodeBuilderViewModel : ReactiveObject
    {
        static NodeBuilderViewModel()
        {
            Locator.CurrentMutable.Register(() => new NodeBuilderControl(), typeof(IViewFor<NodeBuilderViewModel>));
        }

        public NodeBuilderViewModel()
        {
            ListViewModel.AddNodeType(() => new TextNodeViewModel());
            ListViewModel.AddNodeType(() => new WildcardNodeViewModel());

            ListViewModel.AddNodeType(() => new GroupNodeViewModel());
            ListViewModel.AddNodeType(() => new AlterNodeViewModel());
            ListViewModel.AddNodeType(() => new QuantifierNodeViewModel());
            ListViewModel.AddNodeType(() => new AnchorNodeViewModel());

            var output2 = new OutputTextNodeViewModel();

            NetworkViewModel.Nodes.Add(output2);

            NetworkViewModel.Validator = network =>
            {
                var containsLoops = GraphAlgorithms.FindLoops(network).Any();
                if (containsLoops)
                    return new NetworkValidationResult(false, false,
                        new ErrorMessageViewModel("Network contains loops!"));


                return new NetworkValidationResult(true, true, null);
            };

            output2.ResultInput.ValueChanged
                .Select(v => NetworkViewModel.LatestValidation?.IsValid ?? true ? v : "Error")
                .BindTo(this, vm => vm.ValueLabel);
        }

        public NodeListViewModel ListViewModel { get; } = new();
        public NetworkViewModel NetworkViewModel { get; } = new();

        #region ValueLabel

        private string _valueLabel;

        public string ValueLabel
        {
            get => _valueLabel;
            set => this.RaiseAndSetIfChanged(ref _valueLabel, value);
        }

        #endregion
    }
}