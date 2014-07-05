	'This code exports a delimited tab text file of some parameters from Walls.
	Public Sub exportTxt()
		
		'assigns document to the active document in use
		Dim document As Document = Me.ActiveUIDocument.Document
		
		'a string to store the output files location
		Dim location As String = "c:\data.txt"
		
		'Create a stingbuilder to add the data to
		Dim stringBuilder As New System.Text.StringBuilder()
		
		'add header information
		stringBuilder.AppendLine("WallID" & Microsoft.VisualBasic.vbTab & "Name" & Microsoft.VisualBasic.vbTab & "Comments")

		'Basic collection of wall types
	    Dim wallCollection As IList(Of Element) = New FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType().ToElements()
	    
	    'loop through walls and add data
		For Each e As Element In wallCollection
			stringBuilder.AppendLine(e.Id.ToString  & Microsoft.VisualBasic.vbTab &e.Name & Microsoft.VisualBasic.vbTab & e.Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).AsString)
		Next
		
		'write the string builder to the txt file
		System.IO.File.WriteAllText(location,stringBuilder.tostring)
		
		'dialog to show result
		TaskDialog.Show("List of Instances", "Export Successful")
		
	End Sub