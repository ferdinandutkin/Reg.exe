using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using NodeBuilder.Models;
using NodeBuilder.ViewModels;
using NodeBuilder.ViewModels.Nodes;
using ReactiveUI;

namespace NodeBuilder.Views
{
    public partial class AnchorValueEditorView : UserControl, IViewFor<AnchorValueEditorViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(AnchorValueEditorViewModel), typeof(AnchorValueEditorView), new PropertyMetadata(null));

        public AnchorValueEditorViewModel ViewModel
        {
            get => (AnchorValueEditorViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (AnchorValueEditorViewModel)value;
        }
        #endregion

        public AnchorValueEditorView()
        {
            InitializeComponent();


            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.Value.AnchorType, v => v.anchorType.ItemsSource,
                    _ => new EnumDescriptions<AnchorType>()).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Value.AnchorType, v => v.anchorType.SelectedValue).DisposeWith(d);

            });

            anchorType.UpdateLayout();

        }
    }
}
