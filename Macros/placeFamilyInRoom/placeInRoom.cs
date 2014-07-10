/*
  The code will get a family type called "Single" and place it into a Room with the Room Name "Roombed" and the Number 2.
  You can customise each of these options as you require
*/

		public void placeInRoom()
		{
			//sets the variable to the current application
			Document doc = this.ActiveUIDocument.Document;
			
			//gets the family by it's type name
			var bed = from elem in new FilteredElementCollector(doc)
				.OfClass(typeof(FamilySymbol))
				let fam = elem as FamilySymbol
				where fam.Name == "Single"
				select fam;
			
			//selets the family type			
			FamilySymbol bed1 = bed.Single();
			
			//gets the room by its name
			var room2 = from elem in new FilteredElementCollector(doc)
				.OfClass(typeof(SpatialElement))
				let roo = elem as SpatialElement
				where roo.Name == "Roombed 2"
				select roo;
			
			//selects the room element
			Room r1 = room2.Single() as Room;
			
			//obsolete method for getting levels, will be removed or "deprecated"
			//Level rlevel = r1.Level;
			//new way to get levels
			Level rlevel = doc.GetElement(r1.LevelId) as Level;
			//gets the location of the room as a location point
			LocationPoint rlocation = r1.Location as LocationPoint;
			
			//converts the locationpoint into an XYZ point
			XYZ rPoint = new XYZ(rlocation.Point.X, rlocation.Point.Y, rlocation.Point.Z);
			
			
			//starts the transaction to place objects in rooms
			using (Transaction tr = new Transaction(doc,"Create bed in Room"))
			    {
			        tr.Start();
					
			        //uses the place family method
					FamilyInstance place = doc.Create.NewFamilyInstance(rPoint, bed1, rlevel, StructuralType.NonStructural);
			
					tr.Commit();
				}
		}
