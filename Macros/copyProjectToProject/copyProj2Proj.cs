/*
  This routine is more complex and has a few error handlers around it. It will copy across filters, Wall types and Wall Tags
  from a open project called Text.rvt to your current open project. 
  TAKE NOTE: When having files open if your Windows Folder open for hide file extentions is turned on then this will not
  work and you will need to remove te .rvt from the file name.
*/

		public void copyProj2Proj()
		{
			//sets the variable to the current application
			Application app = this.Application;
			//sets the variable to active document
			Document doc = this.ActiveUIDocument.Document;
			//set the variable to the name of the Revit File - Be aware depending on your file name settings it can cause issues
			string docname = "Test.rvt";
			//selects the open document called test
			Document srcDoc = app.Documents.Cast<Document>().Where(d => d.Title == docname).First();
			//selects the alternate opened document
			//Document srcDoc = app.Documents.Cast<Document>().Where(d => d.Title != doc.Title).FirstOrDefault();
			//creates a new list that will contain element id's
			ICollection<ElementId> Ids;
			//creates a list that will contain builtincategories
			List<BuiltInCategory> TemplateCategories = new List<BuiltInCategory>();
			
			//A list of categories from the document to be copied over
			TemplateCategories.Add(BuiltInCategory.OST_Walls);
			TemplateCategories.Add(BuiltInCategory.OST_WallTags);
			
			//creates a default element collector on the active document			
			FilteredElementCollector Coll = new FilteredElementCollector(srcDoc);
			//a multicategory filter to allow users to pass the categories
            ElementMulticategoryFilter mColl = new ElementMulticategoryFilter(TemplateCategories);
            //checks the collection where there are template categories filters them
            Coll.WherePasses(mColl);
            //adds all the element id's from each of the categories to the ElementId list
            Ids = Coll.WhereElementIsElementType().ToElementIds().ToList();
			
            //gets a collection of element id's
            ICollection<ElementId> filterIds = new FilteredElementCollector(srcDoc).OfClass(typeof(ParameterFilterElement)).ToElementIds();
            //adds each element id from the collection to the Element Id's list
            foreach (ElementId e in filterIds)
	            {
	                Ids.Add(e);
	            }
	            
            //starts the transaction that will copy the data from the source project into the current project    
            using (Transaction t = new Transaction(doc, "Copy Families and Filters"))
           	{
                t.Start();
                	
                //sets the copy paste options to rely on destination items when the warning should appear
                CopyPasteOptions cpOpts = new CopyPasteOptions();
                cpOpts.SetDuplicateTypeNamesHandler(new HideAndAcceptDuplicateTypeNamesHandler());

                //checks if the count is more then 0 if not, cancels transaction
                if (Ids.Count == 0)
	                {
	                    return;
	                }
				//applies the project copy
                ElementTransformUtils.CopyElements(srcDoc, Ids, doc, Transform.Identity, cpOpts);
				//options for copy failure
                FailureHandlingOptions failureOptions = t.GetFailureHandlingOptions();
                failureOptions.SetFailuresPreprocessor(new HidePasteDuplicateTypesPreprocessor());

                t.Commit(failureOptions);
           }
		}
            //this class supresses and sets the duplication warning issue a user receives
            class HideAndAcceptDuplicateTypeNamesHandler : IDuplicateTypeNamesHandler
        	{

            public DuplicateTypeAction OnDuplicateTypeNamesFound(DuplicateTypeNamesHandlerArgs args)
            {
                // Always use duplicate destination types when asked
                return DuplicateTypeAction.UseDestinationTypes;
            }
			}
            //this class handles when copies fail and allow the software to continue
            class HidePasteDuplicateTypesPreprocessor : IFailuresPreprocessor
        	{
            public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
            {
                foreach (FailureMessageAccessor failure in failuresAccessor.GetFailureMessages())
                {
                    // Delete any "Can't paste duplicate types.  Only non duplicate types will be pasted." warnings
                    //if (failure.GetFailureDefinitionId() == BuiltInFailures.CopyPasteFailures.CannotCopyDuplicates)
                    if (failure.GetFailureDefinitionId() == BuiltInFailures.CopyPasteFailures.CannotCopyDuplicates)
                    {
                        failuresAccessor.DeleteWarning(failure);
                        //failuresAccessor.GetType();
                    }
                    //List<ElementId> delete = new List<ElementId>();
                    //failuresAccessor.DeleteElements(delete);
                }

                // Handle any other errors interactively
                return FailureProcessingResult.Continue;
            }
		}
