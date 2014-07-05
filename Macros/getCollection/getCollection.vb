'  This commend accesses wall information directly from the Revit database
'  It then returns the type information of the wall such as the names and Id's and returns that to a string
   
    Public Sub Ex2_2get_Collection()
    	
  		'assigns doc to the active document in use
  		Dim document As Document = Me.ActiveUIDocument.Document
  		
	    'Creates a new stringbuilder Instance
	    Dim results As New System.Text.StringBuilder(String.Empty)
	    
	    'Basic collection of wall types
	    Dim wallCollection As IList(Of Element) = New FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType().ToElements()
	    	
	    'foreach statement to get access to the data or elements
	    For Each e As Element In wallCollection
	    	results.AppendLine("Wall Name: " & e.Name & "- Id Number: " & e.Id.ToString())
	    Next
	    
	    'Dialog to show results
	    TaskDialog.Show("List of Instances", results.ToString())
	    
    End Sub