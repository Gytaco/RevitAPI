	'This code will delete views, filters and view templates from your Project
	Public Sub deleteElements()
		
		'assigns document to the active document in use
  		Dim document As Document = Me.ActiveUIDocument.Document
  		

 ''get all the views and view template ids. Internally revit see view templates as view.

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'' original code. Doesn't actually get all views
'   		Dim viewidCollection As IEnumerable(Of ElementId) = From v As View In New FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_Views).cast(Of view)
'  							Where v.ViewType = ViewType.AreaPlan Or _
'  									v.ViewType = ViewType.CeilingPlan Or _
'									v.ViewType = ViewType.ColumnSchedule Or _
'									v.ViewType = ViewType.CostReport Or _
'									v.ViewType = ViewType.Detail Or _
'									v.ViewType = ViewType.DraftingView Or _
'									v.ViewType = ViewType.Legend Or _
'									v.ViewType = ViewType.LoadsReport Or _
'									v.ViewType = ViewType.PanelSchedule Or _
'									v.ViewType = ViewType.PresureLossReport Or _
'									v.ViewType = ViewType.Rendering Or _
'									v.ViewType = ViewType.Report Or _
'									v.ViewType = ViewType.Section Or _
'									v.ViewType = ViewType.ThreeD Or _
'									v.ViewType = ViewType.Walkthrough Or _
'									v.ViewType = ViewType.Elevation Or _
'									v.ViewType = ViewType.Undefined Or _
'									v.ViewType = ViewType.DrawingSheet Or _
'									v.IsTemplate
'  							Select v.Id
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


		'updated code gets element ids of all views and view templates except project browser, system browser and active view as these cannot be deleted
		Dim viewCollection As list(Of ElementId) = (From v As View In New FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_Views).cast(Of view)
					  							Where v.ViewType <> ViewType.ProjectBrowser And _
						  								v.ViewType <> ViewType.SystemBrowser And _
						  								v.ViewType <> ViewType.Internal And _
						  								v.Id <> document.ActiveView.id
					  							Select v.id).tolist
																		
  		
  		'get all the view filter ids
  		Dim filteridCollection As list(Of ElementId) = (New FilteredElementCollector(document) _
														  			.ofclass(GetType(ParameterFilterElement)) _
														  			.WhereElementIsNotElementType _
													  				.ToElementids()).tolist
					
		'Start a transaction CRITICAL without transactions Revit cannot update
  		Using rvtTransaction As New Transaction(document,"Delete all views")
  			rvtTransaction.Start()
  			
  			'delete the views
  			document.Delete(viewCollection)		
  			  			
  			'delete the filters
  			document.Delete(filteridCollection)
	
  			'Commit the changes to the Revit File 
  			rvtTransaction.Commit
  			
  		End Using
  		 		
  		
	End Sub