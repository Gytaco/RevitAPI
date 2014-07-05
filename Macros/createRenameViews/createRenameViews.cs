/*
  This project creates Plan, Cailing and Structural Views based on your ViewTypes in yoru project and will rename them
  to Level - View Type and will convert them to upper case.
*/

		public void createRenameViews()
		{
			//sets the variable to the current application
			Document doc = this.ActiveUIDocument.Document;
			
			//creates a collection of the levels
			var lvls = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels)
				.WhereElementIsNotElementType().ToElements();
			
			//starts a transaction that creates the views
			using (Transaction tx = new Transaction (doc, "GHD Create Views"))
            {
                tx.Start();
				
                //gets the View Types from the project using linq commands
				foreach (ViewFamilyType vpt in (from v in new FilteredElementCollector(doc)
				    .OfClass(typeof(ViewFamilyType)).Cast<ViewFamilyType>() select v))
	            {
                	//looks for the floor plans, ceiling plans and structural plans only
					if (vpt.ViewFamily == ViewFamily.FloorPlan || vpt.ViewFamily == ViewFamily.CeilingPlan
					    || vpt.ViewFamily == ViewFamily.StructuralPlan)
					{
                		//looks at each level
						foreach (Level lvl in lvls)
						{
							//creates a new floor plan
							ViewPlan flplan = ViewPlan.Create(doc, vpt.Id, lvl.Id);
	
	                        //rename the View to the name of the View Type and level to upper case
	                        flplan.Name = vpt.Name.ToUpper() + " " + lvl.Name.ToUpper();
						}
					}
				}
                //commits the transaction
                tx.Commit();
			}
		}
