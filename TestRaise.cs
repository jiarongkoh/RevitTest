using System;
using System.Diagnostics;

using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System.Collections.Generic;

namespace DockablePane
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]

    public class TestRaise : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            UIDocument uiDoc = app.ActiveUIDocument;

            Selection selection = uiDoc.Selection;
            ICollection<ElementId> selectedIds = selection.GetElementIds();

            //Get all elements in active document
            //https://thebuildingcoder.typepad.com/blog/2010/06/filter-for-all-elements.html
            FilteredElementCollector coll = new FilteredElementCollector(uiDoc.Document);

            coll.WherePasses(new LogicalOrFilter(
                                new ElementIsElementTypeFilter(false),
                                new ElementIsElementTypeFilter(true)));

            String info = "Ids of selected elements in the document are: ";

            foreach (Element e in coll)
            {
                info += "\n\t" + e.Id.IntegerValue + e.Name;
            }

            TaskDialog.Show("Revit", info);
        }

        public string GetName()
        {
            return "External event";
        }
    }
}
