using System;
using System.Diagnostics;

using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System.Collections.Generic;
using Autodesk.Revit.DB.Architecture;

using System.Collections.ObjectModel;
using System.Windows;

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
            // TaskDialog.Show("Revit", info);
            Window window = new Window()
            {
                Title = "Results",
                Content = new MellowWindow(app, getStairViewMapping(app))
            };

            window.Show();
        }

        // returns mapping of stair ID to list of views that contain it
        private ObservableCollection<StairViewMapping> getStairViewMapping(UIApplication app)
        {
            // Get current active document
            UIDocument uiDoc = app.ActiveUIDocument;

            ObservableCollection<StairViewMapping> stairViewMapping = new ObservableCollection<StairViewMapping>();

            // Collector used to iterate through all views in document
            FilteredElementCollector viewCollector = new FilteredElementCollector(uiDoc.Document);
            viewCollector.OfClass(typeof(View));

            // Collector used to iterate through all stairs in document
            FilteredElementCollector stairCollector = new FilteredElementCollector(uiDoc.Document);
            stairCollector.OfClass(typeof(Stairs));

            // First iterate through all stairs
            foreach (Stairs stair in stairCollector)
            {
                Debug.WriteLine("---------------");
                Debug.WriteLine("Stair ID: " + stair.Id);

                ObservableCollection<ViewDetails> containerViews = new ObservableCollection<ViewDetails>();

                // Iterate through all views to check if that view contains stair
                foreach (View view in viewCollector)
                {
                    // Get all element ids in that view
                    try
                    {
                        FilteredElementCollector elementsInView = new FilteredElementCollector(uiDoc.Document, view.Id);

                        // If contains our stair ID, add to stair view mapping
                        if (elementsInView.ToElementIds().Contains(stair.Id))
                        {
                            containerViews.Add(new ViewDetails { viewId = view.Id.ToString(), stairId = stair.Id.ToString() });
                            Debug.WriteLine("---");
                            Debug.WriteLine("View ID: " + view.Id);
                            Debug.WriteLine("View name: " + view.Name);
                            Debug.WriteLine("---");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                }

                // add to mapping before going to next stair
                stairViewMapping.Add(new StairViewMapping() { stairId = stair.Id.ToString(), viewIds = containerViews });
            }

            return stairViewMapping;
        }

        public string GetName()
        {
            return "External event";
        }
    }
}
