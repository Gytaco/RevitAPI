/*
  This code will delete views, filters and view templates from your Project
*/

		public void deleteElements()
		{
			//sets the variable to the current application
			Document doc = this.ActiveUIDocument.Document;
			
			//uses linq to select the views and templates to delete
            var ViewDelExport = from elem in new FilteredElementCollector(doc)
            .OfClass(typeof(View))
                            let type = elem as View
                            where type.ViewType == ViewType.AreaPlan ||
                            type.ViewType == ViewType.CeilingPlan ||
                            type.ViewType == ViewType.ColumnSchedule ||
                            type.ViewType == ViewType.CostReport ||
                            type.ViewType == ViewType.Detail ||
                            type.ViewType == ViewType.DraftingView ||
                            type.ViewType == ViewType.Legend ||
                            type.ViewType == ViewType.LoadsReport ||
                            type.ViewType == ViewType.PanelSchedule ||
                            type.ViewType == ViewType.PresureLossReport ||
                            type.ViewType == ViewType.Rendering ||
                            type.ViewType == ViewType.Report ||
                            type.ViewType == ViewType.Section ||
                            type.ViewType == ViewType.ThreeD ||
                            type.ViewType == ViewType.Walkthrough ||
                            type.ViewType == ViewType.Elevation ||
                            type.ViewType == ViewType.Undefined ||
                            type.ViewType == ViewType.DrawingSheet ||
            				type.IsTemplate
                            select type;

			//gets the element id's of the views and adds them to a list
            var deleteViews = ViewDelExport.Select(view => view.Id).ToList();	
			
            //gets all the element id's of the filters in a project
            var deletefilters = new FilteredElementCollector(doc).OfClass(typeof(ParameterFilterElement))
            	.WhereElementIsNotElementType().ToElementIds();
            
            //create a new transaction
            Transaction tx = new Transaction(doc);
            //starts the new transaction
            tx.Start("Delete All View Templates");
            //delete all the views
            doc.Delete(deleteViews);
            //delete the filters in the project
            doc.Delete(deletefilters);
            //commits the transaction
            tx.Commit();
		}
