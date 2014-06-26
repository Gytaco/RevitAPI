/*
  This code has a placeholder sheet that allows users to move a single sheet and the views in other drawings
  will update.
  YOU NEED TO USE THIS WITH THE REVIT FILE CALLED THE SAME NAME IN THIS FOLDER!!
*/

		public void Ex08Op_PlaceMoveViewsOnSheets()
		{
			//sets the variable to the active document
			Document doc = this.ActiveUIDocument.Document;
			//creates an empty viewport object
			Viewport vpe = null;
			
			//collection to get the current viewports in our project
			var fcoll = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Viewports)
				.WhereElementIsNotElementType().ToElements();
			
			//goes through the viewports and the gets the drafting view called Placement Setup
			foreach (Element e in fcoll)
			{
				Viewport vp = e as Viewport;
				if(vp != null)
				{
					//gets the Viewport name which is the drafting sheet name
					string svp = vp.get_Parameter(BuiltInParameter.VIEWPORT_SHEET_NAME).AsString();
					
					if( svp == "Placement Setup")
					{
						vpe = vp;
						break;
					}
				}
			}
			//gets the centre location of the viewport cropbox or boundary area (becareful with this!!
			//If views are not the same size when place they will not align
			XYZ plcLoc = vpe.GetBoxCenter();
			
			//collection to get the single legend in the project
			var lcoll = from elem in new FilteredElementCollector(doc)
				.OfClass(typeof(View))
				         let type = elem as View
				         where type.ViewType == ViewType.Legend
				         select type;
			
			//get the elementid of the single legend (Won't work if you have more then 1 legend)
			ElementId lid = lcoll.Select(elid => elid.Id).FirstOrDefault();
			
			//collector to get all the sheets in the project
			var sheetColl = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Sheets)
			.WhereElementIsNotElementType().ToElements();
			
			//starts the transaction
			using (Transaction t = new Transaction(doc, "Place Legends"))
			{
				t.Start();
			       
			//cycles through the sheets in the collection
			foreach (ViewSheet vs in sheetColl)
			{
				//set a placeholder element to null
				Element evp = null;
				//gets a list of all the elementid's of the viewports on a sheet
				ICollection<ElementId> vpcoll = vs.GetAllViewports();
				//checks if viewports exist
				if(vpcoll.Count == 0)
				{
					//if no viewports exists creates a sheet
					Viewport.Create(doc, vs.Id, lid, plcLoc);
				}
				//else if viewports exist runs the next code
				else
				{
					//cycles through the elementid's of the viewports collected
					foreach (ElementId vpid in vpcoll)
					{
						//gets the placeholder from above and puts an element inside it
						evp = doc.GetElement(vpid);
						//sets the element as a Viewport object i able
						Viewport vp = evp as Viewport;
						//checks if the elementid is a valid viewport object otherwise doesn't work
						if (vp != null)
						{
							//gets the name of the legend with a built in category
							string svp = vp.get_Parameter(BuiltInParameter.VIEW_NAME).AsString();
							//checks if the view is already on the sheet
							if(svp == "Placement View")
							{
								//if the names line up moves the legends to the updated position
								vp.SetBoxCenter(plcLoc);
							}
						}	
					}
				}
			}
			t.Commit();
			}
		}
