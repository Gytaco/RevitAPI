'This will import the modified text file from the exportTxt.vb file in this folder and update any comment values
	'that might have been changed.
	Public Sub importTxt()
		
		'assigns document to the active document in use
		Dim document As Document = Me.ActiveUIDocument.Document
		
		'a string to store the input files location
		Dim location As String = "c:\data.txt"
		
		'read data from txt file into an array. Each item in the array represents one line in the txt file.
		Dim data As String() = System.IO.File.ReadAllLines(location)
		
				
		'Start a transaction CRITICAL without transactions Revit cannot update
  		Using rvtTransaction As New Transaction(document,"comment updates")
  			rvtTransaction.Start()
  			
  			
  			'A for statement that is similar to foreach but is structured for arrays. ie it will loop through the data items. 
  			'Start at item 1 as item 0 is the header row.
  			For i As Integer = 1 To data.Count-1
  				
  				'get the location of the first TAB in the line of text
  				Dim iNo As Integer = data(i).indexof(Microsoft.VisualBasic.vbTab)
  				
  				'remove all the charcters after the first TAB, and convert to integer
  				Dim id As integer =cint(data(i).remove(iNo))
  				
  				'remove all the charcters before the first tab
  				Dim comment1 As String = data(i).remove(0, iNo + 1)
  				
  				'get the location of the second TAB (now first)
  				Dim iNo2 As Integer = comment1.indexof(Microsoft.VisualBasic.vbTab)
  				
  				'remove all the charcters before the TAB leaving only the comment data
  				Dim comment As String = comment1.remove(0, iNo2 + 1)
  				  				
  				'create an elment id from the id integer
  				Dim elementId As elementid = New ElementId(id)
  				
  				'get the element using the element id
  				Dim element As Element = document.GetElement(ElementId)
  				
  				'update the comments parameter with the information in the text
  				Element.Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set(comment)
  				 				
  			Next
  			

  			'Commit the changes to the Revit File 
  			rvtTransaction.Commit
  			
  		End Using
  		
	End Sub