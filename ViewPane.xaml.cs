using System;
using System.Windows;
using System.Windows.Controls;

using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;

namespace DockablePane
{
    /// <summary>
    /// Interaction logic for ViewPane.xaml
    /// </summary>
    public partial class ViewPane : Page, IDisposable, IDockablePaneProvider
    {
        TestRaise testREvent;
        ExternalEvent TestRaiseEvent;

        public ViewPane()
        {
            InitializeComponent();

            //To call RevitAPI, need to invoke throught ExternalEvent
            testREvent = new TestRaise();
            TestRaiseEvent = ExternalEvent.Create(testREvent);
        }

        public void Dispose()
        {
            this.Dispose();
        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this as FrameworkElement;
            //data.VisibleByDefault = false;

            data.InitialState = new DockablePaneState
            {
                DockPosition = DockPosition.Right
            };
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            TestRaiseEvent.Raise();
        }
    }
}
