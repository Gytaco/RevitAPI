using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace Application
{
    class examplecommands
    {   
        
        //first exercise put into a user interface
        public static void getSelection(Document doc, UIDocument uidoc)
        {
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
			TaskDialog.Show("Example", s + " " + t );
		}
		//creates an IselectionFilter interface
		class WallSelectionFilter : ISelectionFilter
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
        //second exercise put into a user interface
        public static void getCollection(Document doc, UIDocument uidoc)
        {
            //Creates a new stringbuilder Instance
            StringBuilder results = new StringBuilder(string.Empty);

            //Basic collection for walls
            ICollection<Element> elem = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType().ToElements();

            //foreach statement to get access to the data or elements
            foreach (Element e in elem)
            {
                //TaskDialog.Show("List of Instances", Name and e.Id.ToString() );
                results.Append("Wall Name: " + e.Name + "- Id Number: " + e.Id.ToString() + "\n");
            }

            //Dialog to show results
            TaskDialog.Show("List of Instances", results.ToString());
        }
        //third exervise put into a user interface
        public static void updateDoor(Document doc, UIDocument uidoc)
        {
            //creates a Collection of the doors as FamilyInstances
            var doorColl = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Doors)
                .OfClass(typeof(FamilyInstance));

            //Uses linq queries to select the door that have a ToRoom Value
            IEnumerable<FamilyInstance> doors =
                from FamilyInstance f in doorColl
                where f.ToRoom != null
                select f;

            //Start the transaction CRITICAL without transactions Revit cannot update
            using (Transaction t = new Transaction(doc, "Door Data Update"))
            {
                //Starts the transction
                t.Start();
                //goes through each door based on the filter above
                foreach (FamilyInstance e in doors)
                {
                    //gets the ToRoom number as a string			       		
                    string doorToRoom = e.ToRoom.Number.ToString();
                    //updates the comments parameter to the ToRoom value			       		
                    e.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set(doorToRoom);
                }
                //Commits the changes to the Revit File 
                t.Commit();
            }
        }
    }
}
