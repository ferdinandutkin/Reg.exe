using Core.Models;
using CW.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;

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

                this.Bind(ViewModel, vm => vm.Model.Text, v => v.textInput.Text).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.SelectedPosition, v => v.positions.SelectedValue).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.RegexPattern, v => v.regexInput.Text).DisposeWith(d);
                
                this.BindCommand(ViewModel, vm => vm.ApplyRegexCommand, v => v.applyRegex).DisposeWith(d);
            });
          

        }

        private void AddToSelected_Click(object sender, RoutedEventArgs e) => 
             ViewModel.AddPositionCommand.Execute(new Position(textInput.SelectionStart, textInput.SelectionStart + textInput.SelectionLength - 1));
      
    }
}
