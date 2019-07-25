using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace DockablePane
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]

    class RegisterDockablePaneManager : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            return Register(commandData.Application);
        }

        public Result Register(UIApplication application)
        {

            try
            {
                var data = new DockablePaneProviderData();
                var pane = new ViewPane();
                data.FrameworkElement = pane as FrameworkElement;

                var dpid = new DockablePaneId(DockablePaneIdentifierManager.GetPanelIdentifier());
                application.RegisterDockablePane(dpid, "Pane", pane as IDockablePaneProvider);
                return Result.Succeeded;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return Result.Failed;
            }
           
        }
    }
}
