using CW.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для RegexTester.xaml
    /// </summary>
    public partial class RegexTesterView : ReactiveUserControl<RegexTesterViewModel>
    {
        public RegexTesterView()
        {
            InitializeComponent();

            this.WhenActivated(
                d =>

                {
                    Reference.ViewModel = new CWRegexTester.ReferenceControlViewModel();

                    this.Bind(ViewModel, vm => vm.Model.Input, v => v.InputBox.Text).DisposeWith(d);
                    this.Bind(ViewModel, vm => vm.Model.Pattern, v => v.PatternBox.Text).DisposeWith(d);




                    this.WhenAnyValue(v => v.InputBox.IsFocused, v => v.InputBox.Text)
                    .Select(t => !t.Item1 && string.IsNullOrWhiteSpace(t.Item2))
                    .BindTo(this, v => v.EmptyInputBoxMessage.Visibility)
                    .DisposeWith(d);


                    this.WhenAnyValue(v => v.PatternBox.IsFocused, v => v.PatternBox.Text)
                   .Select(t => !t.Item1 && string.IsNullOrWhiteSpace(t.Item2))
                   .BindTo(this, v => v.EmptyPatternBoxMessage.Visibility)
                   .DisposeWith(d);
                });
        }
    }
}
