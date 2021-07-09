using CW.ViewModels;
using ReactiveUI;

namespace CW.Views
{
    /// <summary>
    /// Логика взаимодействия для QuestionsView.xaml
    /// </summary>
    public partial class QuestionsView : ReactiveUserControl<QuestionsViewModel>
    {
        public QuestionsView()
        {
            InitializeComponent();

            this.WhenActivated(
                d =>
                {

                }

                );




        }
    }
}
