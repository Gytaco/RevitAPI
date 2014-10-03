'  This routine is more complex and has a few error handlers around it. It will copy across filters, Wall types and Wall Tags
'  from a open project called Text.rvt to your current open project. 
'  TAKE NOTE: When having files open if your Windows Folder open for hide file extentions is turned on then this will not
'  work and you will need to remove the .rvt from the file name.
    Public Sub copyProj2Proj()
    	
    	'assign application to the current applicaton
    	Dim application As Autodesk.Revit.ApplicationServices.Application = Me.Application
    	
    	'assigns document to the active document in use - default
    	Dim document As Document = Me.ActiveUIDocument.Document
    	
    	'set the source document name
    	Dim sourceDocumentName As String = "Test.rvt"
    	
    	'get the source document
    	Dim sourceDocument As Document = (From d As document In application.Documents.Cast(Of Document)    		
    		Where d.Title = sourceDocumentName
    		Select d).first
    	
    	'create a list to store the list of categories that will be copied
    	Dim categoriesToCopy As New List(Of BuiltInCategory)
    	
    	'add wall and walltags to the category list
    	categoriesToCopy.Add(BuiltInCategory.OST_Walls)
    	categoriesToCopy.Add(BuiltInCategory.OST_WallTags)
    	
    	'setup a collector for the source document
    	Dim collector As New FilteredElementCollector(sourceDocument)
    	
    	'setup a multicategoryfilter base on the category list
    	Dim filter As New ElementMulticategoryFilter(categoriesToCopy)
    	
    	'run the collector and get the wall and tag types to copy
    	Dim wallsToCopy As iEnumerable(Of ElementId) = collector _
    		.WherePasses(filter) _
    		.WhereElementIsElementType _
    		.ToElementIds
    	
    	'get all the parameter filter ids
    	Dim paramFiltersToCopy As List(Of elementid) = New FilteredElementCollector(sourceDocument) _
    		.ofclass(GetType(ParameterFilterElement)) _
    		.ToElementIds.ToList
    	
    	'combine the lists
    	Dim allElementsToCopy As New List(Of ElementId)
    	allElementsToCopy.Addrange(wallsToCopy)
    	allElementsToCopy.AddRange(paramFiltersToCopy)
    
		'check we have at least one thing to copy
		If allElementsToCopy.Count>0 Then
		
			'Start a transaction CRITICAL without transactions Revit cannot update
			Using rvtTransaction As New Transaction(document,"Copy families and filters")
			rvtTransaction.Start()
			
				'set the copypaste options to use destination items when duplicates are found
				Dim copyOptions As New CopyPasteOptions()
				copyoptions.SetDuplicateTypeNamesHandler(New DuplicateTypeNamesHandler())
				
				'copy elements
				ElementTransformUtils.CopyElements(sourceDocument,allElementsToCopy,document,Transform.Identity,copyOptions)
				
				'setup a failure handler to supress warnings when the transaction is commited
				Dim failureOptions As FailureHandlingOptions = rvtTransaction.GetFailureHandlingOptions
				failureoptions.SetFailuresPreprocessor(New FailuresPreprocessor)
				
				'Commit the changes to the Revit File 
				rvtTransaction.Commit(failureOptions)
				
			End Using
		
		End If
  			
End Sub




'This class supresses duplication type warning dialog by forcing copy paste to use destination project types
Public Class DuplicateTypeNamesHandler 
	Implements IDuplicateTypeNamesHandler
	
	Public Function OnDuplicateTypeNamesFound(args As DuplicateTypeNamesHandlerArgs) As DuplicateTypeAction Implements IDuplicateTypeNamesHandler.OnDuplicateTypeNamesFound
		
		'Always use duplicate destination types when asked
		Return DuplicateTypeAction.UseDestinationTypes
		
	End Function

End Class


		
		
'This class silently handles failures caused by finding duplicates and allows revit to continue	
Public Class FailuresPreprocessor
    Implements IFailuresPreprocessor

    Public Function PreprocessFailures(ByVal failuresAccessor As FailuresAccessor) As FailureProcessingResult Implements IFailuresPreprocessor.PreprocessFailures
    	
    	'look through all the failure messages
    	For Each failureMessage As FailureMessageAccessor In failuresAccessor.GetFailureMessages
    		
    		'Delete any "Can't paste duplicate types.  Only non duplicate types will be pasted." messages
    		If failureMessage.GetFailureDefinitionId = BuiltInFailures.CopyPasteFailures.CannotCopyDuplicates Then
                failuresAccessor.DeleteWarning(failureMessage)
            End If
    	Next
    	
    	'all othe messages will have to be dealt with interactively by the user
    	Return FailureProcessingResult.Continue
    	
    End Function

End Class