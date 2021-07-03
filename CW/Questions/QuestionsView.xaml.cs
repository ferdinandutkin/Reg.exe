using Core.Models;
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
