/*
  The following code will update any door that has a ToRoom parameter for the name Number that is active.
  It will then update the comments parameter of each door with the updated value.
*/

    public void doorTagUpdate()
		{
			//Sets the active document
			Document doc = this.ActiveUIDocument.Document;
			
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
