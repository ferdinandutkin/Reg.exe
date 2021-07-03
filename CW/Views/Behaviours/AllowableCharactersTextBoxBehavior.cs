using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CW.Views.Behaviours
{
    public class AllowableCharactersTextBoxBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty RegularExpressionProperty =
             DependencyProperty.RegisterAttached("RegularExpression", typeof(string), typeof(AllowableCharactersTextBoxBehavior),
             new FrameworkPropertyMetadata(".*"));
       

        public static string GetRegularExpression(FrameworkElement frameworkElement) => frameworkElement.GetValue(RegularExpressionProperty) as string;
        public static void SetRegularExpression(FrameworkElement frameworkElement, string value) => frameworkElement.SetValue(RegularExpressionProperty, value);

        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.RegisterAttached("MaxLength", typeof(int), typeof(AllowableCharactersTextBoxBehavior),
            new FrameworkPropertyMetadata(int.MinValue));



        public static int GetMaxLength(FrameworkElement frameworkElement) => (int)frameworkElement.GetValue(MaxLengthProperty);
        public static void SetMaxLength(FrameworkElement frameworkElement,  int value) => frameworkElement.SetValue(MaxLengthProperty, value);
 
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            DataObject.AddPastingHandler(AssociatedObject, OnPaste);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = Convert.ToString(e.DataObject.GetData(DataFormats.Text));

                if (!IsValid(text, true))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        void OnPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) => e.Handled = !IsValid(e.Text, false);

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            DataObject.RemovePastingHandler(AssociatedObject, OnPaste);
        }

        private bool IsValid(string newText, bool paste) => !ExceedsMaxLength(newText, paste) && Regex.IsMatch(newText, GetRegularExpression(this.AssociatedObject));

        private bool ExceedsMaxLength(string newText, bool paste)
        {
            if (GetMaxLength(this.AssociatedObject) == 0) return false;

            return LengthOfModifiedText(newText, paste) > GetMaxLength(this.AssociatedObject);
        }

        private int LengthOfModifiedText(string newText, bool paste)
        {
            var countOfSelectedChars = this.AssociatedObject.SelectedText.Length;
            var caretIndex = this.AssociatedObject.CaretIndex;
            string text = this.AssociatedObject.Text;

            if (countOfSelectedChars > 0 || paste)
            {
                text = text.Remove(caretIndex, countOfSelectedChars);
                return text.Length + newText.Length;
            }
            else
            {
                var insert = Keyboard.IsKeyToggled(Key.Insert);

                return insert && caretIndex < text.Length ? text.Length : text.Length + newText.Length;
            }
        }
    }
}
