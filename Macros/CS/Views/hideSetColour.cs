/*
  This code will hide and set the colours of any elements in yoru active view. There are additional search functionalities
  I have left in as tags to query and access objects.
*/

		public void hideSetColour()
		{
			//sets the variable to the current application
			Application app = this.Application;
			//sets the variable to the current document
			Document doc = this.ActiveUIDocument.Document;
			//gets the active view
			ElementId aview = this.ActiveUIDocument.ActiveView.Id;
			
			#region setup a colour override
			/*
			//create a new colour
			Color color = new Color(150, 200, 200);
			
			//gets the element of the fill pattern solid fill
			var fpcoll = from elem in new FilteredElementCollector(doc)
				.OfClass(typeof(FillPatternElement))
					let type = elem as FillPatternElement
					where type.Name == "Solid fill"
					select type;
			//gets the element id from the element
			var fpid = fpcoll.Select(fp => fp.Id).FirstOrDefault();
			
  			//set the colour of the projection surface colour and the fill pattern
  			var col = new OverrideGraphicSettings();
  			col.SetProjectionFillColor(color);
  			col.SetProjectionFillPatternId(fpid);
  			*/
  			#endregion
			
  			#region Linq to get Views by Name
  			/*
			//LINQ query that uses a filtered element collector to get the view by name
			var viewget = from elem in new FilteredElementCollector(doc)
			.OfClass(typeof(ViewPlan))
					let type = elem as ViewPlan
					where type.Name == "Level 1"
					select type;
			
			//gets the element id of the view
			ElementId eid = viewget.Select(view => view.Id).FirstOrDefault();
			*/
			#endregion
			
			//basic option to use a filter
			ICollection<ElementId> fiid = new FilteredElementCollector(doc, aview).OfCategory(BuiltInCategory.OST_Walls)
				.WhereElementIsNotElementType().ToElementIds();
			
			#region Linq to get elements by multiple categories
			/*
			//how to use multiple categories in a filtered collection
			ICollection<ElementId> fiid = new FilteredElementCollector(doc, eid)
					//applies a logical filter which allows user to add multiple categories
					.WherePasses(new LogicalOrFilter(new List<ElementFilter>
				                                  {
				                                  	new ElementCategoryFilter(BuiltInCategory.OST_Walls),
				                                  	new ElementCategoryFilter(BuiltInCategory.OST_Floors)
				                                  }))
					.WhereElementIsNotElementType().ToElementIds();
			*/
			#endregion			
			
			#region Linq to get elements by parameter values
			/*
			//Using LINQ lets the user select objects via a parameter value
			var elemget = from elem in new FilteredElementCollector(doc, eid)
				.OfClass(typeof(Wall)).WhereElementIsNotElementType()
					let type = elem as Wall
					where type.Name == "Generic - 300mm"
					select type;
					
			
			ICollection<ElementId> fiid = elemget.Select(elid => elid.Id).ToList();
			*/
			#endregion
			
			//starts the transaction
			using (Transaction t = new Transaction(doc, "Hide Elements"))
			{
				t.Start();
				
				//uses the GetElement method to get the View
				View flrplan = doc.GetElement(aview) as View;
				
				//methods to hide the elements in the view based on their element id's
				flrplan.HideElements(fiid);
				
				#region set colour by element override
				/*
				var leftid = new FilteredElementCollector(doc, aview).WhereElementIsNotElementType().ToElementIds();
				
				//apply the override by element to the remaining visible elements
				foreach (ElementId feid in leftid)
				{
				flrplan.SetElementOverrides(feid, col);
				}
				*/
				#endregion
				
				t.Commit();
			}
		}
