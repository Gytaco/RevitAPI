'this macro will collect all the views and then loop through them hiding the crop region in each one		
Public Sub HideCropRegion()

	'assigns document to the active document in use
	Dim document As Document = Me.ActiveUIDocument.Document
	
	'get all the views
	Dim viewCollection As ienumerable(Of view) = From v As View In New FilteredElementCollector(document) _
										.OfCategory(BuiltInCategory.OST_Views) _
										.whereelementisnotelementtype _
										.cast(Of view)
		
	'Start a transaction CRITICAL without transactions Revit cannot update
	Using rvtTransaction As New Transaction(document,"Hide Crop Regions")
		rvtTransaction.Start()
		

		'loop through the views				
		For Each v As View In viewCollection
			
			'get the crop region parameter
			Dim p As Parameter = v.Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE)
			
			'if parameter exists, turn it off
			If p IsNot Nothing Then
				p.Set(0)
			End If
				
		Next
		
		'Commit the changes to the Revit File 
		rvtTransaction.Commit
	End Using


End Sub
