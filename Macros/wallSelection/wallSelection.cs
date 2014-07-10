/*
  This code enables users to select walls using a ISelectionFiler class, it then interrogates the selected wall
  and returns some instance and Type built in Parameters as a dialog box in Revit.
*/

		public void Ex1_4get_Selection()
		{
			//assigns doc to the active document in use - default
			Document doc = this.ActiveUIDocument.Document;
			//assings uidoc to the active User Interface in use-default
			UIDocument uidoc = this.ActiveUIDocument;
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
			string t = pType.get_Parameter("Width").AsValueString();
			
			//Shows the values
			TaskDialog.Show("Example", s + " " + t );
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
