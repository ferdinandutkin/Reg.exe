using CW.Data;
using ReactiveUI;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;

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
                    this.OneWayBind(ViewModel, vm => vm.Title, v => v.title.Text).DisposeWith(d);
                }
                );



        }





    }
}
