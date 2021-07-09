using Core.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows.Input;

namespace CW.ViewModels
{
    public class AddTestCaseViewModel : TestCaseViewModel
    {
        [Reactive]
        public Position SelectedPosition
        {
            get; set;
        } = new();


        [Reactive]
        public string RegexPattern
        {
            get; set;
        } = string.Empty;


        public ICommand ApplyRegexCommand =>
             ReactiveCommand.Create(() => Model.Positions = new RegexTester() { Input = Model.Text, Pattern = RegexPattern }.Matches);



        public ICommand AddPositionCommand =>
            ReactiveCommand.Create<Position>(pos => Model.Positions.Add(pos));


    }
}
