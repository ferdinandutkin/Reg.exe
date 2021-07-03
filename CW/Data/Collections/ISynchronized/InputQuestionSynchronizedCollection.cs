using Core.Models;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW.Data 
{
    class InputQuestionSynchronizedCollection : ObservableCollection<InputQuestion>, ISynchronizedCollection<InputQuestion>

    {
        public bool ISSynchronizationEnabled { get; set; } = true;



        IObjectManager<InputQuestion> LoadingManager { get; set; }
        IObjectManager<TestCase> TestCaseManager = Locator.Current.GetService<IObjectManager<TestCase>>();


        IObjectManager<Position> PositionsManager = Locator.Current.GetService<IObjectManager<Position>>();

        public InputQuestionSynchronizedCollection()
        {
            LoadingManager = Locator.Current.GetService<IObjectManager<InputQuestion>>();

            this.CollectionChanged += InputQuestionSynctonizedCollection_CollectionChanged;
        }

        public InputQuestionSynchronizedCollection(IEnumerable<InputQuestion> entities) : this()
        {
            this.CollectionChanged -= InputQuestionSynctonizedCollection_CollectionChanged;
            foreach (InputQuestion entity in entities)
            {

                Add(entity);
                entity.PropertyChanged += Entity_PropertyChanged; ;
                SubscribeToTestcasePropertiesChanged(entity);

            }
            this.CollectionChanged += InputQuestionSynctonizedCollection_CollectionChanged;

        }

        private void Entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ISSynchronizationEnabled)
            {
                var newValue = typeof(InputQuestion).GetProperty(e.PropertyName).GetValue(sender);
                LoadingManager.Update((sender as InputQuestion).Id, e.PropertyName, newValue);

               
            }
        }

        private void InputQuestionSynctonizedCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ISSynchronizationEnabled)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (InputQuestion item in e.NewItems)
                    {

                        item.Id = LoadingManager.Add(item);
                        item.PropertyChanged += Entity_PropertyChanged;
                        SubscribeToTestcasePropertiesChanged(item);
                    }
                }

                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (InputQuestion item in e.OldItems)
                    {
                        item.PropertyChanged -= Entity_PropertyChanged;
                        LoadingManager.Delete(item);
                    }
                }
            }
        }


        void SubscribeToPositions( TestCase testCase)
        {
            if (ISSynchronizationEnabled)
            {
                foreach (var position in testCase.Positions)
                {
                    position.PropertyChanged += Position_PropertyChanged;
                }
                testCase.Positions.CollectionChanged += (sender, args) => Positions_CollectionChanged(testCase, args);
            }
        }

        private void Positions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ISSynchronizationEnabled)
            {
                if (e.Action == NotifyCollectionChangedAction.Move) return;
                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (Position item in e.OldItems)
                    {
                        if (item.Id != 0)
                        {
                            PositionsManager.Delete(item);
                        }

                    }

                    return;

                }

                var testCase = sender as TestCase;


                TestCaseManager.Update(testCase.Id, nameof(TestCase.Positions), testCase.Positions);


          

                testCase.Positions.ClearEventInvocations(nameof(CollectionChanged));
                testCase.Positions =  TestCaseManager.Get(testCase.Id, nameof(TestCase.Positions)) as ObservableCollection<Position>;

                testCase.Positions.CollectionChanged += (sender, args) => Positions_CollectionChanged(testCase, args);
                testCase.PropertyChanged += TestCase_PropertyChanged;

                foreach (var pos in testCase.Positions)
                {
                    pos.PropertyChanged += Position_PropertyChanged;


                }
            }
        }

     
        private void Position_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ISSynchronizationEnabled)
            {
                if (ISSynchronizationEnabled)
                {
                    var newValue = typeof(Position).GetProperty(e.PropertyName).GetValue(sender);
                    PositionsManager.Update((sender as Position).Id, e.PropertyName, newValue);

                }
            }
        }

        private void SubscribeToTestcasePropertiesChanged(InputQuestion item)
        {
            if (ISSynchronizationEnabled)
            {
                foreach (var testCase in item.TestCases)
                {

                    testCase.PropertyChanged += TestCase_PropertyChanged;
                    SubscribeToPositions(testCase);
                }

                item.TestCases.CollectionChanged += (sender, args) => TestCases_CollectionChanged(item, args);
            }
        }

        private void TestCases_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            if (ISSynchronizationEnabled)
            {
                if (e.Action == NotifyCollectionChangedAction.Move) return;
                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (TestCase item in e.OldItems)
                    {
                        if (item.Id != 0)
                        {
                            TestCaseManager.Delete(item);
                        }

                    }

                    return;

                }

                var question = sender as InputQuestion;




                LoadingManager.Update(question.Id, nameof(InputQuestion.TestCases), question.TestCases);


                question.TestCases.ClearEventInvocations(nameof(CollectionChanged));
                question.TestCases = LoadingManager.Get(question.Id, nameof(InputQuestion.TestCases)) as ObservableCollection<TestCase>;

                question.TestCases.CollectionChanged += (s, args) => TestCases_CollectionChanged(question, args);

                foreach (var testCase in question.TestCases)
                {

                    testCase.Positions.ClearEventInvocations(nameof(CollectionChanged));
                    testCase.Positions = TestCaseManager.Get(testCase.Id, (nameof(TestCase.Positions))) as ObservableCollection<Position>;
                    testCase.Positions.CollectionChanged += (sender, args) => Positions_CollectionChanged(testCase, args);
                    testCase.PropertyChanged += TestCase_PropertyChanged;
                    SubscribeToPositions(testCase);
                }


            }
        }





        private void TestCase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ISSynchronizationEnabled)
            {
             
                
                
                var newValue = typeof(TestCase).GetProperty(e.PropertyName).GetValue(sender);
                TestCaseManager.Update((sender as TestCase).Id, e.PropertyName, newValue);
  
            }
        }






        //private void LazyObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (ISSynchronizationEnabled)
        //    {
        //        if (e.Action == NotifyCollectionChangedAction.Add)
        //        {
        //            foreach (T item in e.NewItems)
        //            {

        //                item.Id = LoadingManager.Add(item);
        //                item.PropertyChanged += Item_PropertyChanged;
        //                SubscribeToInnerPropertiesChanged(item);
        //            }
        //        }

        //        else if (e.Action == NotifyCollectionChangedAction.Remove)
        //        {
        //            foreach (T item in e.OldItems)
        //            {
        //                item.PropertyChanged -= Item_PropertyChanged;
        //                LoadingManager.Delete(item);
        //            }
        //        }
        //    }


        ////}

        //private void NestedCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (ISSynchronizationEnabled)
        //    {
        //        var service = Locator.Current.GetService(typeof(IObjectManager<>).MakeGenericType(sender.GetType().GetGenericArguments()[0]))
        //            as IObjectManager;

        //        if (e.Action == NotifyCollectionChangedAction.Add)
        //        {
        //            foreach (INotifiableEntity item in e.NewItems)
        //            {

        //                item.Id = service.Add(item);
        //                item.PropertyChanged += NestedPropertyChanged;
        //                SubscribeToInnerPropertiesChanged(item);
        //            }
        //        }

        //        else if (e.Action == NotifyCollectionChangedAction.Remove)
        //        {
        //            foreach (INotifiableEntity item in e.OldItems)
        //            {
        //                item.PropertyChanged -= NestedPropertyChanged;
        //                service.Delete(item);
        //            }
        //        }
        //    }


        //}


        //private void NestedPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (ISSynchronizationEnabled)
        //    {
        //        var senderType = sender.GetType();
        //        var service = Locator.Current.GetService(typeof(IObjectManager<>).MakeGenericType(senderType)) as IObjectManager;
        //        var newValue = senderType.GetProperty(e.PropertyName).GetValue(sender);
        //        service.Update((sender as IEntity).Id, e.PropertyName, newValue);

        //        SubscribeToInnerPropertiesChanged(newValue);


        //    }
        //}


        //private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (ISSynchronizationEnabled)
        //    {
        //        var newValue = typeof(T).GetProperty(e.PropertyName).GetValue(sender);
        //        LoadingManager.Update((sender as T).Id, e.PropertyName, newValue);

        //        SubscribeToInnerPropertiesChanged(newValue);
        //    }

        //}

    }
}
