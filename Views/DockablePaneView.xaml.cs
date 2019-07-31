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
    public partial class DockablePaneView : Page, IDisposable, IDockablePaneProvider
    {
        GetElementsEvent getElementsEvent;
        ExternalEvent GetElementsExternalEvent;

        public DockablePaneView()
        {
            InitializeComponent();

            //To call RevitAPI, need to invoke throught ExternalEvent
            getElementsEvent = new GetElementsEvent();
            GetElementsExternalEvent = ExternalEvent.Create(getElementsEvent);
        }

        /// <summary>
        /// IDisposable interface method
        /// </summary>
        public void Dispose()
        {
            this.Dispose();
        }

        /// <summary>
        /// IDockablePaneProvider interface method
        /// </summary>
        /// <param name="data"></param>
        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this as FrameworkElement;

            //Set visibility to false so that dockablePane is not shown on launch
            data.VisibleByDefault = false;

            data.InitialState = new DockablePaneState
            {
                DockPosition = DockPosition.Right
            };
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            //Perform GetElements Event function
            GetElementsExternalEvent.Raise();
        }
    }
}
