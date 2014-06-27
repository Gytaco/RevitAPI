#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace Application
{
    [Transaction(TransactionMode.Manual)]
    public class getSelection : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            examplecommands.getSelection(doc, uidoc);

            return Result.Succeeded;
        }
    }
    [Transaction(TransactionMode.Manual)]
    public class getCollection : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            examplecommands.getCollection(doc, uidoc);

            return Result.Succeeded;
        }
    }
    [Transaction(TransactionMode.Manual)]
    public class updateDoor : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            examplecommands.updateDoor(doc, uidoc);

            return Result.Succeeded;
        }
    }
    [Transaction(TransactionMode.Manual)]
    public class importData : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            UserControl1 form = new UserControl1(doc);
            form.ShowDialog();

            return Result.Succeeded;
        }
    }
}
