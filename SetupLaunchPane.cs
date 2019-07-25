using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;

namespace DockablePane
{
    public class SetupLaunchPane : IExternalApplication
    {
        Result IExternalApplication.OnShutdown(UIControlledApplication application)
        {
            throw new NotImplementedException();
        }

        Result IExternalApplication.OnStartup(UIControlledApplication application)
        {
           

            //Create a ribbon panel at the top
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("Dockable Pane");

            //Get url to the DockablePane.dll
            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            //Create a push button and add to ribbon panel
            PushButtonData buttonData = new PushButtonData("launchPane", "Launch Pane", assemblyPath, "DockablePane.LaunchPane");
            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;

            //Setup button image
            Uri buttonImageUri = new Uri(@"C:\Users\Jia Rong\Desktop\VSProjects\baseline_flight_takeoff_black_24dp.png");
            BitmapImage buttonImage = new BitmapImage(buttonImageUri);
            pushButton.LargeImage = buttonImage;

            //Register dockable pane
            application.ControlledApplication.ApplicationInitialized += RegLaunchPane;

            return Result.Succeeded;
        }

        private void RegLaunchPane(object sender, Autodesk.Revit.DB.Events.ApplicationInitializedEventArgs e)
        {
            var registerPaneCommand = new RegisterDockablePaneManager();
            registerPaneCommand.Register(new UIApplication(sender as Autodesk.Revit.ApplicationServices.Application));
        }
    }
}
