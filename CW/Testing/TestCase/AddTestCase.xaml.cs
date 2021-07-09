using Core.Models;
using CW.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для AddTestCase.xaml
    /// </summary>
    public partial class AddTestCase : ReactiveUserControl<AddTestCaseViewModel>
    {

        public AddTestCase()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {

                this.Bind(ViewModel, vm => vm.Model.Text, v => v.TextInput.Text).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.SelectedPosition, v => v.Positions.SelectedValue).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.RegexPattern, v => v.RegexInput.Text).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.ApplyRegexCommand, v => v.ApplyRegex).DisposeWith(d);
            });


        }

        private void AddToSelected_Click(object sender, RoutedEventArgs e) =>
             ViewModel.AddPositionCommand.Execute(new Position(TextInput.SelectionStart, TextInput.SelectionStart + TextInput.SelectionLength - 1));

    }
}
