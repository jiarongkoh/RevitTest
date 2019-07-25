using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace DockablePane
{
    public class ModelItems : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            return GetElements();
        }

        public Result GetElements()
        {

            TaskDialog.Show("Revit", "GetAllElements");

            return Result.Succeeded;
        }

    }
}
