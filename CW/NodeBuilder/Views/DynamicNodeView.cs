using NodeBuilder.ViewModels.Nodes;
using NodeNetwork.Views;
using ReactiveUI;
using System.Windows;
using System.Windows.Controls;

namespace NodeBuilder.Views
{

    [TemplatePart(Name = nameof(AddNewButton), Type = typeof(Button))]
    public class DynamicNodeView : NodeView, IViewFor<DynamicNodeViewModel>
    {
        private Button AddNewButton { get; set; }


        DynamicNodeViewModel IViewFor<DynamicNodeViewModel>.ViewModel
        {
            get => ViewModel as DynamicNodeViewModel;
            set => ViewModel = value;
        }

        public DynamicNodeView()
        {
            this.WhenActivated(
      d =>
      {
          d(this.BindCommand(
              (this as IViewFor<DynamicNodeViewModel>).ViewModel,
              vm => vm.AddNewCommand,
              v => v.AddNewButton));
      });
        }




        public override void OnApplyTemplate()
        {
            AddNewButton = GetTemplateChild(nameof(AddNewButton)) as Button;

            base.OnApplyTemplate();
        }
    }
}
