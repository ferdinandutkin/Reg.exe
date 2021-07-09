using NodeBuilder.Models;
using NodeBuilder.Views;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;

namespace NodeBuilder.ViewModels
{
    public class WildcardValueEditorViewModel : ValueEditorViewModel<WildcardModel>
    {

        static WildcardValueEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new WildcardEditorView(), typeof(IViewFor<WildcardValueEditorViewModel>));
        }

        public WildcardValueEditorViewModel()
        {
            Value = new WildcardModel();
        }
    }
}
