using DynamicData;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace CWRegexTester
{
    /// <summary>
    /// Логика взаимодействия для ReferenceControl.xaml
    /// </summary>
    public partial class ReferenceControl : ReactiveUserControl<ReferenceControlViewModel>
    {

        #region Show/Hide properties
        public static readonly DependencyProperty ShowSearchProperty =
            DependencyProperty.Register(nameof(ShowSearch), typeof(bool), typeof(ReferenceControl), new PropertyMetadata(true));

        public static readonly DependencyProperty ShowTitleProperty =
            DependencyProperty.Register(nameof(ShowTitle), typeof(bool), typeof(ReferenceControl), new PropertyMetadata(true));

        public bool ShowSearch
        {
            get => (bool)GetValue(ShowSearchProperty);
            set => SetValue(ShowSearchProperty, value);
        }


        public bool ShowTitle
        {
            get => (bool)GetValue(ShowTitleProperty);
            set => SetValue(ShowTitleProperty, value);
        }
        #endregion

        #region Colors
        public static readonly DependencyProperty ListEntryBackgroundBrushProperty =
            DependencyProperty.Register(nameof(ListEntryBackgroundBrush), typeof(Brush), typeof(ReferenceControl), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush ListEntryBackgroundBrush
        {
            get => (Brush)GetValue(ListEntryBackgroundBrushProperty);
            set => SetValue(ListEntryBackgroundBrushProperty, value);
        }

        public static readonly DependencyProperty ListEntryBackgroundMouseOverBrushProperty =
            DependencyProperty.Register(nameof(ListEntryBackgroundMouseOverBrush), typeof(Brush), typeof(ReferenceControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0xf7, 0xf7, 0xf7))));

        public Brush ListEntryBackgroundMouseOverBrush
        {
            get => (Brush)GetValue(ListEntryBackgroundMouseOverBrushProperty);
            set => SetValue(ListEntryBackgroundMouseOverBrushProperty, value);
        }

        public static readonly DependencyProperty ListEntryHandleBrushProperty =
            DependencyProperty.Register(nameof(ListEntryHandleBrush), typeof(Brush), typeof(ReferenceControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0x99, 0x99, 0x99))));

        public Brush ListEntryHandleBrush
        {
            get => (Brush)GetValue(ListEntryHandleBrushProperty);
            set => SetValue(ListEntryHandleBrushProperty, value);
        }
        #endregion

        public CollectionViewSource CVS { get; } = new CollectionViewSource();

        public ReferenceControl()
        {
            InitializeComponent();



            this.WhenActivated(d =>
            {

                this.Bind(ViewModel, vm => vm.SearchQuery, v => v.SearchBox.Text).DisposeWith(d);

                this.WhenAnyValue(v => v.ViewModel.VisibleEntries).Switch().Bind(out var bindableList).Subscribe().DisposeWith(d);
                CVS.Source = bindableList;
                ElementsList.ItemsSource = CVS.View;

                this.WhenAnyObservable(v => v.ViewModel.VisibleEntries.CountChanged)
                    .Select(count => count == 0)
                    .BindTo(this, v => v.EmptyMessage.Visibility).DisposeWith(d);

                this.OneWayBind(ViewModel, vm => vm.Title, v => v.TitleLabel.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.EmptyLabel, v => v.EmptyMessage.Text).DisposeWith(d);

                this.WhenAnyValue(v => v.SearchBox.IsFocused, v => v.SearchBox.Text)
                      .Select(t => !t.Item1 && string.IsNullOrWhiteSpace(t.Item2))
                      .BindTo(this, v => v.EmptySearchBoxMessage.Visibility)
                      .DisposeWith(d);

                this.WhenAnyValue(v => v.ShowSearch)
                    .BindTo(this, v => v.SearchBoxGrid.Visibility).DisposeWith(d);

                this.WhenAnyValue(v => v.ShowTitle)
                    .BindTo(this, v => v.TitleLabel.Visibility).DisposeWith(d);
            });
        }


    }
}

