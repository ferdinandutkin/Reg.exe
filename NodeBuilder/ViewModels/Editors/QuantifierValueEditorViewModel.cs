using NodeBuilder.Models;
using NodeBuilder.Views;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;

namespace NodeBuilder.ViewModels
{
    public class QuantifierValueEditorViewModel : ValueEditorViewModel<QuantifierModel>
    {
        static QuantifierValueEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new QuantifierEditorView(), typeof(IViewFor<QuantifierValueEditorViewModel>));
        }

        public QuantifierValueEditorViewModel()
        {
            Value = new QuantifierModel();
        }
    }
}
