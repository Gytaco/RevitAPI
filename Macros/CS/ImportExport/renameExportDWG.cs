/*
  This is a simple routine that allows a user to customise the naming of their DWG exports. It will save people alot
  of time. Please make sure you rename the ExportDWGSettings to "Adam" or a custom export setting name
  as well as change the folder location before using this feature.
*/

		public void renameExportDWG()
		{
			//sets the variable to the current application
			Document doc = this.ActiveUIDocument.Document;
			//sets the name of the DWGexport
			string dwgexportname = "Adam";
			//sets the folder location for our files to save to
			string folder = @"C:\";
			//gets the project Number as a string
			string projinfo = new FilteredElementCollector(doc).OfClass(typeof(ProjectInfo))
				.ToElements().FirstOrDefault().get_Parameter(BuiltInParameter.PROJECT_NUMBER).AsString();
			
			//gets the Export DWG Settings named above as dwgexportname
			ExportDWGSettings edwg = new FilteredElementCollector(doc).OfClass(typeof(ExportDWGSettings))
				.Where(dwg => dwg.Name == dwgexportname).First() as ExportDWGSettings;
			
			//gets a collection of the sheets in the project
			ICollection<Element> sheet = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Sheets)
				.WhereElementIsNotElementType().ToElements();
			
			//starts the transaction to export the sheets
			Transaction tran = new Transaction(doc, "DWG Export");
            tran.Start();
			
            //gets each sheet as a element
			foreach (Element e in sheet)
			{
				//looks at the ViewSheet properties rather then element properties of each sheet
				ViewSheet s = e as ViewSheet;
				//sets of the name of the DWG to exported
				string shtName = projinfo + "-" + s.SheetNumber + "_" + s.Name;
				//creates a new sheet id list
				IList<ElementId> shtId = new List<ElementId>();
				//adds the single sheet id to the list because the Exporter requires it
				shtId.Add(e.Id);
				//gets the Export options from the export settings
				DWGExportOptions dwgEO = edwg.GetDWGExportOptions();
				
				//This method runs the DWG creation tool
				doc.Export(folder, shtName, shtId, dwgEO);
			}
			tran.Commit();
		}
