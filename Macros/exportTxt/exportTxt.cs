/*
  This code exports a delimited tab text file of some parameters from Walls.
*/

//This code requires the following reference
using System.IO;

    public void exportTxt()
		{			
			//assigns doc to the active document in use - default
			Document doc = this.ActiveUIDocument.Document;
			//a string that stores the files location
			string location = @"C:\data.txt";
			//creates a private IEnumerable that accepts string DataTypes only
			IEnumerable<string> Result;
			//creates a string list object
			List<string> list = new List<string>();
			
			//Basic collection for walls
			ICollection<Element> elem = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls)
				.WhereElementIsNotElementType().ToElements();
			
			//string header for the text file
			string header = ("WallID\tName\tComments");
			//adds the header to the top of the list
			list.Add(header);
			
			//foreach statement to get access to the data or elements
			foreach (Element e in elem)
			{
				//gets all the wall instance Id's, names and comments and formats them with tabs 				
				list.Add(e.Id.ToString() + "\t" + e.Name.ToString() + "\t" + e.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).AsString());
			}
			//get the completed list into the Ienumerable
			Result = list;
			//writes all the information to the text file
			File.WriteAllLines(location, Result);
			
			//Dialog to show results
			TaskDialog.Show("List of Instances", "Export Successful");			
		}
