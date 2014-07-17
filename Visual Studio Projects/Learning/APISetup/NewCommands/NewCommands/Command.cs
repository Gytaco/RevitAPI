#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace NewCommands
{
    [Transaction(TransactionMode.Manual)]
    public class GetSelection : IExternalCommand
    {
        public UIDocument uidoc;
        public Document doc;

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            doc = uidoc.Document;

            //creates a new instance of the wallselectionFilter
            ISelectionFilter wallfil = new WallSelectionFilter();

            //gets the object selected Elementid, applies the selection filter and the string mesasge
            ElementId obj = uidoc.Selection.PickObject(ObjectType.Element, wallfil, "Select a Wall please").ElementId;

            //gets the element from the ElementID
            Element e = doc.GetElement(obj);
            //get the family type Elementid
            ElementId idType = e.GetTypeId();
            //Get the Element Type ID
            Element pType = doc.GetElement(idType);

            //gets the instance length of the object			
            string s = e.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsValueString();

            //gets the type width of the object
            string t = pType.get_Parameter(BuiltInParameter.WALL_ATTR_WIDTH_PARAM).AsValueString();

            //Shows the values
            TaskDialog.Show("Example", s + " " + t);

            return Result.Succeeded;
        }
        //creates an IselectionFilter interface
        public class WallSelectionFilter : ISelectionFilter
        {
            //Sets up a boolean based on whether an object can be selection
            public bool AllowElement(Element element)
            {
                //if statement for the wall object
                if (element.Category.Name == "Walls")
                {
                    return true;
                }
                return false;
            }
            //sets up whether a reference can be selected
            public bool AllowReference(Reference refer, XYZ point)
            {
                return false;
            }
        }
    }
}

