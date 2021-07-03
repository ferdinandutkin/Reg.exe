using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reactive.Disposables;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;
using NodeBuilder.Models;
using NodeBuilder.ViewModels;
using NodeBuilder.ViewModels.Nodes;
using ReactiveUI;

namespace NodeBuilder.Views
{
    public static class DescriptionExtension
    {
        public static string Description(this Enum value)
        {
            var attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Any())
                return (attributes.First() as DescriptionAttribute).Description;

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(ti.ToLower(value.ToString().Replace("_", " ")));
        }

    }

    public class EnumDescriptions<T> : IEnumerable<ValueDescription> where T : Enum
    {
        readonly List<ValueDescription> descriptions;

        public EnumDescriptions() =>
             descriptions = Enum.GetValues(typeof(T)).Cast<Enum>().Select((e) => new ValueDescription() { Value = e, Description = e.Description() }).ToList();

        public IEnumerator<ValueDescription> GetEnumerator() => descriptions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => (descriptions as IEnumerable).GetEnumerator();

    }


   
    public partial class QuantifierEditorView : UserControl, IViewFor<QuantifierValueEditorViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(QuantifierValueEditorViewModel), typeof(QuantifierEditorView), new PropertyMetadata(null));

        public QuantifierValueEditorViewModel ViewModel
        {
            get => (QuantifierValueEditorViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as QuantifierValueEditorViewModel;
        }
        #endregion



        private static void SetWidthFromItems(ComboBox comboBox)
        {
            if (comboBox.Template.FindName("PART_Popup", comboBox) is Popup popup
                && popup.Child is FrameworkElement popupContent)
            {
                popupContent.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                 
                var emptySize = SystemParameters.VerticalScrollBarWidth + comboBox.Padding.Left + comboBox.Padding.Right;
                comboBox.Width = emptySize + popupContent.DesiredSize.Width;
            }
        }

        public QuantifierEditorView()
        {
            InitializeComponent();



            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.Value.SearchType, v => v.searchType.ItemsSource,
                  _ => new EnumDescriptions<SearchType>()).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Value.SearchType, v => v.searchType.SelectedValue).DisposeWith(d);


               
                this.OneWayBind(ViewModel, vm => vm.Value.RepetitionsType, v => v.repetitionsType.ItemsSource,
                    _ => new EnumDescriptions<RepetitionsType>()).DisposeWith(d);

                this.Bind(ViewModel, vm => vm.Value.RepetitionsType, v => v.repetitionsType.SelectedValue).DisposeWith(d);


                this.Bind(ViewModel, vm => vm.Value.Range.Start, v => v.rangeStart.Value).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Value.Range.End, v => v.rangeEnd.Value).DisposeWith(d);

                this.Bind(ViewModel, vm => vm.Value.Number, v => v.number.Value).DisposeWith(d);


            });


            SetWidthFromItems(repetitionsType);
            SetWidthFromItems(searchType);
            
        }
 


         
    }
}
