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
            coll.WherePasses(new LogicalOrFilter(
                                new ElementIsElementTypeFilter(false),
                                new ElementIsElementTypeFilter(true)));

            //Setup info string to append text to display on TaskDialog
            String info = "Ids of selected elements in the document are: ";

            foreach (Element e in coll)
            {
                //Extract just Stairs
                if (e.Name == "Stair")
                {
                    info += "\n\t" + e.Name + " " + e.Id;

                    //Cast as Stairs object
                    Stairs stair = uiDoc.Document.GetElement(e.Id) as Stairs;

                    info += "\n\t" + "Number of steps: " + stair.ActualRisersNumber.ToString();
                    info += "\n\t";
                }
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
