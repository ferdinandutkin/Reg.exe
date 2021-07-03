using NodeNetwork;
using System.Windows;
using Splat;
using CW.Data;
using Core.Models;

namespace CW
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Locator.CurrentMutable.Register<IObjectManager<InputQuestion>>(
               () => new ObjectManager<InputQuestion>(ServerInteractionSigleton.Instance.QuestionControllerCallsWrapper));

            Locator.CurrentMutable.Register<IObjectManager<TestCase>>(
                () => new ObjectManager<TestCase>(ServerInteractionSigleton.Instance.TestCaseControllerCallsWrapper));



            Locator.CurrentMutable.Register<IObjectManager<TestResult>>(
                () => new ObjectManager<TestResult>(ServerInteractionSigleton.Instance.ResultsControllerCallsWrapper));



            Locator.CurrentMutable.Register<IObjectManager<Position>>(
                () => new ObjectManager<Position>(ServerInteractionSigleton.Instance.PositionsControllerCallsWrapper));


            Locator.CurrentMutable.Register<IObjectManager<ReferenceEntry>>(
                () => new ObjectManager<ReferenceEntry>(ServerInteractionSigleton.Instance.ReferenceEntryControllerCallsWrapper));
               

            NNViewRegistrar.RegisterSplat();
        }
    }
}
