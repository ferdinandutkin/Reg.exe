using Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;


namespace CW.Views.Behaviours
{


    public static class PositionsBehaviuor
    {


        public static bool IsBetween<T>(this T item, T start, T end) where T : IComparable => item.CompareTo(start) >= 0 && item.CompareTo(end) <= 0;

        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
           "Text",
           typeof(string),
           typeof(PositionsBehaviuor),
           new FrameworkPropertyMetadata("", OnUpdate));
        public static string GetText(FrameworkElement frameworkElement) => frameworkElement.GetValue(TextProperty) as string;
        public static void SetText(FrameworkElement frameworkElement, string value) => frameworkElement.SetValue(TextProperty, value);


        public static readonly DependencyProperty SelectedPositionsProperty = DependencyProperty.RegisterAttached(
           "SelectedPositions",
           typeof(ObservableCollection<Position>),
           typeof(PositionsBehaviuor),
           new FrameworkPropertyMetadata(null, OnSelectedPositionsChanged));

        private static void OnSelectedPositionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OnUpdate(d, e);
            if (e.OldValue is ObservableCollection<Position> observable)
            {
                observable.CollectionChanged -= (s, e) => CollectionChangedHandler(d, e);
            }
            if (e.NewValue is ObservableCollection<Position> newObservable)
            {
                newObservable.CollectionChanged += (s, e) => CollectionChangedHandler(d, e);
            }
        }

        private static void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                (e.NewItems[0] as Position).PropertyChanged += (s, a) => OnUpdate(sender as DependencyObject, new());
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                (e.OldItems[0] as Position).PropertyChanged -= (s, a) => OnUpdate(sender as DependencyObject, new());
            }
        }

        public static ObservableCollection<Position> GetSelectedPositions(FrameworkElement frameworkElement) => frameworkElement.GetValue(SelectedPositionsProperty) as ObservableCollection<Position>;

        public static void SetSelectedPositions(FrameworkElement frameworkElement, ObservableCollection<Position> value) => frameworkElement.SetValue(SelectedPositionsProperty, value);



        public static readonly DependencyProperty SelectedPositionsBrushProperty = DependencyProperty.RegisterAttached(
      "SelectedPositionsBrush",
      typeof(Brush),
      typeof(PositionsBehaviuor),
      new FrameworkPropertyMetadata(Brushes.White, OnUpdate));


        public static Brush GetSelectedPositionsBrush(FrameworkElement frameworkElement) => frameworkElement.GetValue(SelectedPositionsBrushProperty) as Brush;
        public static void SetSelectedPositionsBrush(FrameworkElement frameworkElement, Brush value) => frameworkElement.SetValue(SelectedPositionsBrushProperty, value);



        public static readonly DependencyProperty DefaultBrushProperty = DependencyProperty.RegisterAttached(
    "DefaultBrush",
    typeof(Brush),
    typeof(PositionsBehaviuor),

    new FrameworkPropertyMetadata(Brushes.Transparent, OnUpdate));


        public static Brush GetDefaultBrush(FrameworkElement frameworkElement) => frameworkElement.GetValue(DefaultBrushProperty) as Brush;
        public static void SetDefaultBrush(FrameworkElement frameworkElement, Brush value) => frameworkElement.SetValue(DefaultBrushProperty, value);


        public static readonly DependencyProperty HighlightedPositionProperty = DependencyProperty.RegisterAttached(
"HighlightedPosition",
typeof(Position),
typeof(PositionsBehaviuor),
new FrameworkPropertyMetadata(null, OnUpdate));


        public static Position GetHighlightedPosition(FrameworkElement frameworkElement) => frameworkElement.GetValue(HighlightedPositionProperty) as Position;
        public static void SetHighlightedPosition(FrameworkElement frameworkElement, Position value) => frameworkElement.SetValue(HighlightedPositionProperty, value);






        public static readonly DependencyProperty HighlightedPositionBrushProperty = DependencyProperty.RegisterAttached(
   "HighlightedPositionBrush",
   typeof(Brush),
   typeof(PositionsBehaviuor),
   new FrameworkPropertyMetadata(Brushes.White, OnUpdate));


        public static Brush GetHighlightedPostionBrush(FrameworkElement frameworkElement) => frameworkElement.GetValue(HighlightedPositionBrushProperty) as Brush;
        public static void SetHighlightedPositionBrush(FrameworkElement frameworkElement, Brush value) => frameworkElement.SetValue(HighlightedPositionBrushProperty, value);




   
        static void ProcessPositions(TextBlock tb, string text)
        {


            var notselectedBrush = GetDefaultBrush(tb);
            var selectedBrush = GetSelectedPositionsBrush(tb);

            var highlightedBrush = GetHighlightedPostionBrush(tb);


            var highlighted = GetHighlightedPosition(tb) ?? new Position(-1, -1);


            var selected = GetSelectedPositions(tb) ?? Enumerable.Empty<Position>();


            var current = new Run() { Background = notselectedBrush };

            foreach (var (c, i) in text.Select((c, i) => (c, i)))
            {

                if (i.IsBetween(highlighted.Start, highlighted.End))
                {
                    if (current.Background != highlightedBrush)
                    {
                        tb.Inlines.Add(current);
                        current = new Run() { Background = highlightedBrush };
                    }

                    current.Text += c;

                }

                else if (selected.Any(term => i.IsBetween(term.Start, term.End)))
                {
                    if (current.Background != selectedBrush)
                    {
                        tb.Inlines.Add(current);
                        current = new Run() { Background = selectedBrush };
                    }

                    current.Text += c;


                }
                else
                {
                    if (current.Background != notselectedBrush)
                    {
                        tb.Inlines.Add(current);
                        current = new Run() { Background = notselectedBrush };

                    }
                    current.Text += c;

                }

            }
            tb.Inlines.Add(current);
        }




        private static void OnUpdate(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBlock tb)
            {
                tb.Text = string.Empty;

                var text = GetText(tb);
                if (!string.IsNullOrEmpty(text))
                {
                    ProcessPositions(tb, text);
                }

            }

        }


    }

}
