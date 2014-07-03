	'  The following code will update any door that has a ToRoom parameter for the name Number that is active.
	'  It will then update the comments parameter of each door with the updated value.
	
	Public Sub doorTagUpdate()
		
		 'assigns document to the active document in use
  		Dim document As Document = Me.ActiveUIDocument.Document
  		
  		'creates a Collection of the doors as FamilyInstances
  		Dim doorCollection As IEnumerable(Of FamilyInstance) = New FilteredElementCollector(document) _
				.OfCategory(BuiltInCategory.OST_Doors) _
				.WhereElementIsNotElementType _
				.ToElements().Cast(Of FamilyInstance)()
  		
  		'Use linq to select doors that have a ToRoom Value
  		Dim doorsWithRooms As IEnumerable(Of FamilyInstance) = From fi As FamilyInstance In doorCollection
									Where fi.ToRoom IsNot Nothing
									Select fi
  		
  		'Start a transaction CRITICAL without transactions Revit cannot update
  		Using rvtTransaction As New Transaction(document,"Door data update")
  			rvtTransaction.Start()
  			
  			'loop through each door based on the filter above
  			For Each fi As FamilyInstance In doorsWithRooms
  				
  				'gets the ToRoom number as a string
  				Dim toRoom As string = fi.ToRoom.Number.ToString
  				
  				'updates the comments parameter to the ToRoom value
  				fi.Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set(toRoom)
  				Next
  				
  			'Commit the changes to the Revit File 
  			rvtTransaction.Commit
  		End Using
  		
	End Sub
