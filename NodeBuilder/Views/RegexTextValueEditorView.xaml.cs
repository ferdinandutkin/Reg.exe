using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using NodeBuilder.ViewModels;
using NodeBuilder.ViewModels.Nodes;
using ReactiveUI;

namespace NodeBuilder.Views
{
    public partial class RegexTextValueEditorView : UserControl, IViewFor<RegexTextValueEditorViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(RegexTextValueEditorViewModel), typeof(RegexTextValueEditorView), new PropertyMetadata(null));

        public RegexTextValueEditorViewModel ViewModel
        {
            get => (RegexTextValueEditorViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (RegexTextValueEditorViewModel)value;
        }
        #endregion

        public RegexTextValueEditorView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, vm => vm.Value.Text, v => v.valueTextBox.Text).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Value.EscapeSpecial, v => v.escapeSpecial.IsChecked).DisposeWith(d);
            });
        }
    }
}
