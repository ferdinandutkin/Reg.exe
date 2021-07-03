using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using NodeBuilder.ViewModels;
using ReactiveUI;

namespace NodeBuilder.Views
{
    public partial class NodeBuilderControl : UserControl, IViewFor<NodeBuilderViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(NodeBuilderViewModel), typeof(NodeBuilderControl), new PropertyMetadata(null));

        public NodeBuilderViewModel ViewModel
        {
            get => (NodeBuilderViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (NodeBuilderViewModel)value;
        }
        #endregion

        public NodeBuilderControl()
        {
            InitializeComponent();

            this.ViewModel = new NodeBuilderViewModel();

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.ListViewModel, v => v.nodeList.ViewModel).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.NetworkViewModel, v => v.viewHost.ViewModel).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.ValueLabel, v => v.valueLabel.Content).DisposeWith(d);
            });
        }
    }
}