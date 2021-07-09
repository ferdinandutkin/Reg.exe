using Core.Models;
using CW.Data;
using DynamicData;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace CWRegexTester
{

    public class RequirmentsToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ServerInteractionSigleton.Instance.CurrentUser.IsInRoleOrHigher((UserRoles)value) ? Visibility.Visible : Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class MenuControl : ReactiveUserControl<MenuControlViewModel>
    {


        #region Show/Hide properties
        public static readonly DependencyProperty ShowSearchProperty =
            DependencyProperty.Register(nameof(ShowSearch), typeof(bool), typeof(MenuControl), new PropertyMetadata(true));

        public static readonly DependencyProperty ShowTitleProperty =
            DependencyProperty.Register(nameof(ShowTitle), typeof(bool), typeof(MenuControl), new PropertyMetadata(true));

        public bool ShowSearch
        {
            get { return (bool)GetValue(ShowSearchProperty); }
            set { SetValue(ShowSearchProperty, value); }
        }

        public bool ShowTitle
        {
            get { return (bool)GetValue(ShowTitleProperty); }
            set { SetValue(ShowTitleProperty, value); }
        }
        #endregion

        #region Colors
        public static readonly DependencyProperty ListEntryBackgroundBrushProperty =
            DependencyProperty.Register(nameof(ListEntryBackgroundBrush), typeof(Brush), typeof(MenuControl), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush ListEntryBackgroundBrush
        {
            get { return (Brush)GetValue(ListEntryBackgroundBrushProperty); }
            set { SetValue(ListEntryBackgroundBrushProperty, value); }
        }

        public static readonly DependencyProperty ListEntryBackgroundMouseOverBrushProperty =
            DependencyProperty.Register(nameof(ListEntryBackgroundMouseOverBrush), typeof(Brush), typeof(MenuControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0xf7, 0xf7, 0xf7))));

        public Brush ListEntryBackgroundMouseOverBrush
        {
            get { return (Brush)GetValue(ListEntryBackgroundMouseOverBrushProperty); }
            set { SetValue(ListEntryBackgroundMouseOverBrushProperty, value); }
        }

        public static readonly DependencyProperty ListEntryHandleBrushProperty =
            DependencyProperty.Register(nameof(ListEntryHandleBrush), typeof(Brush), typeof(MenuControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0x99, 0x99, 0x99))));

        public Brush ListEntryHandleBrush
        {
            get { return (Brush)GetValue(ListEntryHandleBrushProperty); }
            set { SetValue(ListEntryHandleBrushProperty, value); }
        }
        #endregion

        public CollectionViewSource CVS { get; } = new CollectionViewSource();

        public MenuControl()
        {
            InitializeComponent();
            if (DesignerProperties.GetIsInDesignMode(this)) { return; }




            this.WhenActivated(d =>
            {






                this.WhenAnyValue(v => v.ViewModel.VisibleEntries).Switch().Bind(out var bindableList).Subscribe().DisposeWith(d);
                CVS.Source = bindableList;
                ElementsList.ItemsSource = CVS.View;



                //felt cute might delete later (я слышал мввм умер)
                ServerInteractionSigleton.Instance.UserChanged += () => CVS.View.Refresh();





                this.OneWayBind(ViewModel, vm => vm.Title, v => v.TitleLabel.Text).DisposeWith(d);




                this.WhenAnyValue(v => v.ShowTitle)
                    .BindTo(this, v => v.TitleLabel.Visibility).DisposeWith(d);
            });
        }

        private void OnNodeMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)

            {

                if ((sender as FrameworkElement).DataContext is MenuEntryPreview preview)
                {
                    var template = ViewModel.MenuEntryTemplates.Items.First(t => t.PreviewInstance == preview);

                    DragDrop.DoDragDrop(this, new DataObject("entryTemplate", template), DragDropEffects.Copy);
                }


            }
        }

        private void ViewModelViewHost_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show((sender as ViewModelViewHost).ViewModel.ToString());
        }
    }
}
