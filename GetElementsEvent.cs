using System;
using System.Diagnostics;

using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System.Collections.Generic;
using Autodesk.Revit.DB.Architecture;

namespace DockablePane
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]

    public class GetElementsEvent : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            //Get current active document
            UIDocument uiDoc = app.ActiveUIDocument;

            //Get all elements in active document
            //https://thebuildingcoder.typepad.com/blog/2010/06/filter-for-all-elements.html
            FilteredElementCollector coll = new FilteredElementCollector(uiDoc.Document);
            coll.OfClass(typeof(Stairs));

            //Setup info string to append text to display on TaskDialog
            String info = "Ids of selected elements in the document are: ";

            foreach (Stairs s in coll)
            {
                info += "\n\t" + s.Name + " " + s.Id;

                    //Cast as Stairs object
                Stairs stair = uiDoc.Document.GetElement(s.Id) as Stairs;

                    info += "\n\t" + "Number of steps: " + stair.ActualRisersNumber.ToString();
                    info += "\n\t";
                }

            //Display on TaskDialog
            TaskDialog.Show("Revit", info);
        }

        public string GetName()
        {
            return "External event";
        }
    }
}
