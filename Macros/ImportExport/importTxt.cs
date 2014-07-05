/*
  This will import the modified text file from the exportTxt.cs file in this folder and update any comment values
  that might have been changed.
*/

//this file requires the following reference
using System.IO;

		public void Ex5_1txt_Import()
		{
			//assigns doc to the active document in use - default
			Document doc = this.ActiveUIDocument.Document;
			
			//string that stores the tiles location
			string location = @"C:\data.txt";
			//array string with no size
			string[] aData;
			
			//command that reads all the lines in our text file from the location
			aData = File.ReadAllLines(location);
			//gets the length of our string array
			int len = (aData.Length - 1);
			
			//transaction to update our parameters
			using (Transaction t = new Transaction(doc, "comment updates"))
			{
			      	//start the transaction
			      	t.Start();
					
			      	//a for statement that is similar to foreach but is structured for arrays
					for (int i = 1; i <= len; i++)
					{
						//get the char location of the first tab in a line of text
						int iNo = aData[i].IndexOf('\t');
						//remove the rest of the string based on the position of the character
						string sId = aData[i].Remove(iNo);
						
						//removes the Id data from the string line
						string sComment1 = aData[i].Remove(0, iNo + 1);
						//get the char location of the second(now first) tab in the line of text
						int iNo2 = sComment1.IndexOf('\t');
						//removes the name data from the line of text leaving only comments
						string sComment2 = sComment1.Remove(0, iNo2 + 1);
						
						//convert the string id into an integer
						int iid = Int32.Parse(sId);
						//create a new elementid from the converted integer
						ElementId obj = new ElementId(iid);
						//get the element from the element id
						Element e = doc.GetElement(obj);
						
						//update the comments parameter with the information in the text
						e.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set(sComment2);
					}
					//commit the transaction
					t.Commit();
			}
			
		}
