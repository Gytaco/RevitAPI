/*
  This commend accesses wall information directly from the Revit database
  It then returns the type information of the wall such as the names and Id's and returns that to a string
*/


//This code requires a reference for the stringbuilder class
using System.Text;

public void Ex2_2get_Collection()
		{
			//assigns doc to the active document in use - default
			Document doc = this.ActiveUIDocument.Document;
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
