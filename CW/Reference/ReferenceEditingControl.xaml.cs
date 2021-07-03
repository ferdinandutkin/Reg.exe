using CW.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для ReferenceEditingControl.xaml
    /// </summary>
    public partial class ReferenceEditingControl : ReactiveUserControl<ReferenceEditingControlViewModel>
    {
        public ReferenceEditingControl()
        {
            InitializeComponent();
            this.WhenActivated(
                d =>
                {
                    this.OneWayBind(ViewModel, vm => vm.Model, v => v.grid.ItemsSource).DisposeWith(d);
                }
                );
        }
    }
}
