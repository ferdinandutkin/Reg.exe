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
    


   
    public partial class WildcardEditorView : UserControl, IViewFor<WildcardValueEditorViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(WildcardValueEditorViewModel), typeof(WildcardEditorView), new PropertyMetadata(null));

        public WildcardValueEditorViewModel ViewModel
        {
            get => (WildcardValueEditorViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as WildcardValueEditorViewModel;
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

        public WildcardEditorView()
        {
            InitializeComponent();


            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.Value.WildcardType, v => v.wildcardType.ItemsSource,
                  _ => new EnumDescriptions<WildcardType>()).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Value.WildcardType, v => v.wildcardType.SelectedValue).DisposeWith(d);

                this.Bind(ViewModel, vm => vm.Value.Invert, v => v.invert.IsChecked).DisposeWith(d);

                this.Bind(ViewModel, vm => vm.Value.MatchNewlines, v => v.matchNewlines.IsChecked).DisposeWith(d);

                this.Bind(ViewModel, vm => vm.Value.AllowWhitespace, v => v.whitespace.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Value.AllowUppercase, v => v.uppercaseLetters.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Value.AllowLowercase, v => v.lowercaseLetters.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Value.AllowDigits, v => v.digits.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Value.AllowUnderscore, v => v.underscore.IsChecked).DisposeWith(d);
            });
            
            SetWidthFromItems(wildcardType);
                
            }




    }
}
