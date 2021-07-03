using NodeBuilder.Models;
using NodeBuilder.Views;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;

namespace NodeBuilder.ViewModels
{
    public class AnchorValueEditorViewModel : ValueEditorViewModel<AnchorModel>
    {
        static AnchorValueEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new AnchorValueEditorView(), typeof(IViewFor<AnchorValueEditorViewModel>));
        }

        public AnchorValueEditorViewModel()
        {
            Value = new AnchorModel();
        }
    }
}
