	'This is a simple routine that allows a user to customise the naming of their DWG exports. It will save people alot
	'of time. Please make sure you rename the ExportDWGSettings to "Adam" or a custom export setting name
	'as well as change the folder location before using this feature.
	
	Public Sub renameExportDWG()
		
		'assigns document to the active document in use
		Dim document As Document = Me.ActiveUIDocument.Document
		
		'sets the name of the DWGexport
		Dim dwgExportName As String = "Adam"
		
		'a string to store the out files folder
		Dim location As String = "c:\"
		
		'get the project number
		Dim projectNumber As String  = document.ProjectInformation.Parameter(BuiltInParameter.PROJECT_NUMBER).AsString
		
		'get the export settings named above
		Dim exportSettings As ExportDWGSettings = (From es As ExportDWGSettings In New FilteredElementCollector(document).ofclass(GetType(ExportDWGSettings)).cast(Of ExportDWGSettings)
													where es.name =dwgExportName
													Select es).first
		
		
		'gets a collection of the sheets in the project
		Dim sheetCollection As IEnumerable(Of ViewSheet) = New FilteredElementCollector(document).ofclass(GetType(ViewSheet)).WhereElementIsNotElementType.cast(Of ViewSheet)
		
				
		'Start a transaction CRITICAL without transactions Revit cannot update
  		Using rvtTransaction As New Transaction(document,"comment updates")
  			rvtTransaction.Start()
  			
  			
  			For Each sheet As ViewSheet In sheetCollection
  				
  				'set of the name of the DWG to exported
  				Dim sheetName As String = projectNumber & "-" & sheet.Name
  				
  				'creates a new sheet id list
  				Dim idList As New List(Of ElementId)
  				
  				'adds the sheet id to the list because the Exporter requires it
  				idList.Add(sheet.Id)
  				
  				'export the sheet as a DWG
  				document.Export(location,sheetName,idList,exportSettings.GetDWGExportOptions)
  				
  			Next
  						

  			'Commit the changes to the Revit File 
  			rvtTransaction.Commit
  			
  		End Using
		
	End Sub