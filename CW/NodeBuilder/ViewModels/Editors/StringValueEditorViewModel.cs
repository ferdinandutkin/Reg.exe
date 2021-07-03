using NodeBuilder.Views;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;

namespace NodeBuilder.ViewModels
{
    public class StringValueEditorViewModel : ValueEditorViewModel<string>
    {
        static StringValueEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new TextValueEditorView(), typeof(IViewFor<StringValueEditorViewModel>));
        }

        public StringValueEditorViewModel()
        {
            Value = "";
        }
    }
}
