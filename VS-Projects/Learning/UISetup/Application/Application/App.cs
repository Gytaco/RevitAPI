#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using System.Windows.Media.Imaging;
#endregion

namespace Application
{
    class App : IExternalApplication
    {
        //sets the address for the dll file
        public string assemblyloca = @"C:\Users\Gytaco\AppData\Roaming\Autodesk\Revit\Addins\2014\Application.dll";

        //Built in startup command for Revit
        public Result OnStartup(UIControlledApplication a)
        {           
            //the name of the new tab to be created
            string tabName = "RTC 2014";
            //method to create the tab
            a.CreateRibbonTab(tabName);
            //method to create a new panel
            RibbonPanel panel1 = a.CreateRibbonPanel(tabName, "Programming");

            //Buttons go in this area
            PullDown(panel1);
            updateDoor(panel1);
            //return succeeded
            return Result.Succeeded;
        }

        public void PullDown(RibbonPanel panel)
        {
            //creates a Push button for the get selection command
            PushButtonData bgetselect = new PushButtonData("getselec", "Get Selection", assemblyloca, "Application.getSelection");
            //creates a Push button for the get Collection command
            PushButtonData bgetCollect = new PushButtonData("getcoll", "Get Collection", assemblyloca, "Application.getCollection");
            //creates a pull down menu
            PulldownButtonData pdb1 = new PulldownButtonData("WallsSelection","Wall Selector");
            //assigns the pulldown menu to our panel
            PulldownButton pdb = panel.AddItem(pdb1) as PulldownButton;
            //adds the buttons above to the pulldown menu
            pdb.AddPushButton(bgetselect);
            pdb.AddPushButton(bgetCollect);

        }

        public void updateDoor(RibbonPanel panel)
        {
            //This where we setup the command it's using in this case the HelloWorld.CS
            PushButtonData pushButtondataHello = new PushButtonData("updateDoor", "Update Door Data", assemblyloca, "Application.updateDoor");
            // This is how we add the button to our Panel
            PushButton pushButtonHello = panel.AddItem(pushButtondataHello) as PushButton;
            //This is how we add an Icon
            //Make sure you reference WindowsBase and PresentationCore, and import System.Windows.Media.Imaging namespace. 
            pushButtonHello.LargeImage = new BitmapImage(new Uri(@"C:\Users\Gytaco\AppData\Roaming\Autodesk\Revit\Addins\2014\Images\image.png"));
            //Add a tooltip
            pushButtonHello.ToolTip = "This tool updates the doors to contain ToRoom data in the comments parameter";

            PushButtonData importTextbutton = new PushButtonData("importData", "Import Comments for Walls", assemblyloca, "Application.importData");
            PushButton importText = panel.AddItem(importTextbutton) as PushButton;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
