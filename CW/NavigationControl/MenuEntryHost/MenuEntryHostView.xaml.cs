using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CWRegexTester
{
    /// <summary>
    /// Логика взаимодействия для MenuEntryHostview.xaml
    /// </summary>
    public partial class MenuEntryHostView : ReactiveUserControl<MenuEntryHostViewModel>
    {


        public static readonly DependencyProperty EntryBackgroundProperty = DependencyProperty.Register(nameof(EntryBackground),
           typeof(Brush), typeof(MenuEntryHostView));

        public Brush EntryBackground
        {
            get => (Brush)GetValue(EntryBackgroundProperty);
            set => SetValue(EntryBackgroundProperty, value);
        }

        public MenuEntryHostView()
        {
            InitializeComponent();

            SetupDragAndDrop();
        }








        private void SetupDragAndDrop()
        {
            this.WhenActivated(d =>
            {
                bool isDragging = false;

                this.OneWayBind(ViewModel, vm => vm.PendingEntry.Template.PreviewInstance, v => v.PendingEntry.ViewModel).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.PendingEntry, v => v.PendingEntry.Visibility,
                    entry => entry is null ? Visibility.Collapsed : Visibility.Visible)
                    .DisposeWith(d);


                //я не знаю это все работает настолько рандомно что однажды я даже почитаю документацию (шутка)
                this.OneWayBind(ViewModel, vm => vm.PendingEntry, v => v.EntryHost.IsHitTestVisible,
                  entry => entry?.Template?.PreviewInstance is null)
                  .DisposeWith(d);




                this.WhenAnyValue(v => v.ViewModel.PendingEntry.Position).Subscribe(pos =>
                {
                    Canvas.SetLeft(PendingEntry, pos.X);
                    Canvas.SetTop(PendingEntry, pos.Y);
                }).DisposeWith(d);




                this.WhenAnyValue(v => v.ViewModel.SelectedEntry).Subscribe(async entry =>
                {
                    if (entry is not null)
                    {
                        await Task.Delay(300);

                        ViewModel.PendingEntry = new MenuEntryHostViewModel.DraggingTemplate()
                        {
                            Template = entry,


                        };


                        isDragging = false;

                        EntryHost.ViewModel = entry.EntryFactory();

                        ViewModel.PendingEntry = null;





                    }
                }
                );


                this.Events().PreviewDragEnter.Subscribe(e =>
                {
                    if (isDragging)
                        return;

                    if (e.Data.GetData("entryTemplate") is MenuEntryTemplate template)
                    {

                        e.Handled = true;
                        ViewModel.PendingEntry = new MenuEntryHostViewModel.DraggingTemplate()
                        {
                            Template = template,
                            Position = e.GetPosition(ContentContainer)
                        };

                        isDragging = true;
                        e.Effects = DragDropEffects.Copy;

                    }


                    else
                    {
                        e.Handled = false;

                        e.Effects = DragDropEffects.None;
                    }

                }).DisposeWith(d);



                this.Events().PreviewDragOver.Subscribe(e =>
                {


                    if (e.Data.GetData("entryTemplate") is MenuEntryTemplate template)
                    {
                        if (isDragging && ViewModel?.PendingEntry is not null)
                        {
                            ViewModel.PendingEntry.Position = e.GetPosition(ContentContainer);


                        }
                        else
                        {
                            ViewModel.PendingEntry = new MenuEntryHostViewModel.DraggingTemplate
                            {
                                Template = template,
                                Position = e.GetPosition(ContentContainer)
                            };
                            e.Handled = false;

                        }

                        isDragging = true;
                        e.Effects = DragDropEffects.Copy;

                    }
                    else
                    {
                        e.Effects = DragDropEffects.None;
                    }

                }).DisposeWith(d);


                this.Events().PreviewDrop.Subscribe(async e =>
                {



                    if (e.Data.GetData("entryTemplate") is MenuEntryTemplate template)
                    {
                        e.Handled = false;
                        isDragging = false;
                        await Task.Delay(300);
                        EntryHost.ViewModel = template.EntryFactory();

                        ViewModel.PendingEntry = null;


                    }


                }).DisposeWith(d);






                //wpf безбожно спамит dragleave/enter что вызывает мигание 
                //если сразу скоро после leave сработает enter то драг отменять не будем
                //https://stackoverflow.com/questions/5447301/wpf-drag-drop-when-does-dragleave-fire

                this.Events().PreviewDragLeave.Subscribe(async _ =>
                {


                    if (!isDragging) return;

                    isDragging = false;

                    await Task.Delay(200);

                    if (!isDragging)
                    {

                        //я не могу уже drop начал прилетать только при каком-то крайне специфичном поведении
                        if (ContentContainer.IsMouseOver && ViewModel?.PendingEntry?.Template?.EntryFactory is not null)
                        {
                            EntryHost.ViewModel = ViewModel.PendingEntry.Template.EntryFactory();

                        }

                        ViewModel.PendingEntry = null;

                    }

                }
                ).DisposeWith(d);
            });
        }


    }
}

