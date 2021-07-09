using System.Windows;
using System.Windows.Controls;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {

        public string Password
        {
            get => GetValue(PasswordProperty) as string;
            set => SetValue(PasswordProperty, value);
        }


        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(nameof(Password), typeof(string), typeof(BindablePasswordBox));




        public BindablePasswordBox()
        {
            InitializeComponent();


        }

        private void sealed_PasswordChanged(object sender, RoutedEventArgs e)
            => Password = (sender as PasswordBox).Password;

    }
}
