using CW.ViewModels;
using ReactiveUI;
using System.Linq;
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
                    this.reference.ViewModel = new CWRegexTester.ReferenceControlViewModel();

                    this.Bind(ViewModel, vm => vm.Model.Input, v => v.inputBox.Text).DisposeWith(d);
                    this.Bind(ViewModel, vm => vm.Model.Pattern, v => v.patternBox.Text).DisposeWith(d);

 


                    this.WhenAnyValue(v => v.inputBox.IsFocused, v => v.inputBox.Text)
                    .Select(t => !t.Item1 && string.IsNullOrWhiteSpace(t.Item2))
                    .BindTo(this, v => v.emptyInputBoxMessage.Visibility)
                    .DisposeWith(d);


                    this.WhenAnyValue(v => v.patternBox.IsFocused, v => v.patternBox.Text)
                   .Select(t => !t.Item1 && string.IsNullOrWhiteSpace(t.Item2))
                   .BindTo(this, v => v.emptyPatternBoxMessage.Visibility)
                   .DisposeWith(d);
                });
        }
    }
}
