using NodeBuilder.Models;
using NodeBuilder.Views;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;

namespace NodeBuilder.ViewModels
{
    public class RegexTextValueEditorViewModel : ValueEditorViewModel<RegexTextModel>
    {
        static RegexTextValueEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new RegexTextValueEditorView(), typeof(IViewFor<RegexTextValueEditorViewModel>));
        }

        public RegexTextValueEditorViewModel()
        {
            Value = new RegexTextModel();
        }
    }
}
