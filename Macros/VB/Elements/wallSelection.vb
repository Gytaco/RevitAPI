'  This code enables users to select walls using a ISelectionFiler class, it then interrogates the selected wall
'  and returns some instance and Type built in Parameters as a dialog box in Revit.

    Public Sub Ex1_4get_Selection()
			'assigns doc to the active document in use - default
			Dim document As Document = Me.ActiveUIDocument.Document
    				
			'assings uidoc to the active User Interface in use-default
			Dim uidoc = Me.ActiveUIDocument
			
			'creates a new instance of the wallselectionFilter
			Dim wallFilter As ISelectionFilter  = New WallSelectionFilter()
			
			'gets the object selected Elementid, applies the selection filter and the string mesasge
			Dim ElementId As Elementid = uidoc.Selection.PickObject(ObjectType.Element,wallFilter,"Select a Wall Please").ElementId
			
			'gets the element from the ElementID
			Dim e As Element = document.GetElement(ElementId)

			'get the family type Elementid
			Dim typeId As ElementId = e.GetTypeId
			
			'Get the family
			dim familyType As Element = document.GetElement(typeId)

			'gets the length of the wall (this is in decimal feet, revits internal units)
			Dim length As Double = e.Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble
			
			'gets the type width of the object (this is in decimal feet, revits internal units)
			dim width as double = familyType.Parameter("Width").AsDouble
			
			'Shows the values
			TaskDialog.Show("Example", length & " " & width )
						
    End Sub


		

		'creates an IselectionFilter interface
		Public Class WallSelectionFilter 
			Implements ISelectionFilter

			'Sets up a boolean based on whether an object can be selection
			Public Function AllowElement(element As Element) As Boolean Implements ISelectionFilter.allowElement	
				Return element.Category.Name = "Walls"
			End Function
			
			'sets up whether a reference can be selected
			Public Function AllowReference(reference As Reference, point As XYZ)As Boolean  Implements ISelectionFilter.allowReference
				Return false
			End Function
			
		End Class