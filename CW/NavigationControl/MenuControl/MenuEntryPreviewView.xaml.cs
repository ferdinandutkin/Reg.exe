using ReactiveUI;
using System.Reactive.Disposables;

namespace CWRegexTester
{
    /// <summary>
    /// Логика взаимодействия для MenuEntryPreviewView.xaml
    /// </summary>
    public partial class MenuEntryPreviewView : ReactiveUserControl<MenuEntryPreview>
    {

        public MenuEntryPreviewView()
        {
            InitializeComponent();



            this.WhenActivated(
                d =>
                {
                    this.OneWayBind(ViewModel, vm => vm.Title, v => v.Title.Text).DisposeWith(d);
                }
                );



        }





    }
}
