'	This project creates Plan, Ceiling and Structural Views based on your ViewTypes in yoru project and will rename them
    '  	to "ViewType Level" in uppercase.
    Public Sub CreateRenameViews()

        'assigns document to the active document in use - default
        Dim document As Document = Me.ActiveUIDocument.Document

        'get all the levels in the project
        Dim levelCollection As IEnumerable(Of Level) = New FilteredElementCollector(document) _
            .OfCategory(BuiltInCategory.OST_Levels) _
            .WhereElementIsNotElementType() _
            .ToElements() _
            .Cast(Of Level)()

        'get all the viewtypes in the project
        Dim viewTypecollection As IEnumerable(Of ViewFamilyType) = New FilteredElementCollector(document) _
            .OfClass(GetType(ViewFamilyType)) _
            .WhereElementIsElementType _
            .Cast(Of ViewFamilyType)()

        'Start a transaction CRITICAL without transactions Revit cannot update
        Using rvtTransaction As New Transaction(document, "Create views")
            rvtTransaction.Start()

            'loop through all the view types found
            For Each v As ViewFamilyType In viewTypecollection

                'look for floor, ceiling and structural plans
                If v.ViewFamily = ViewFamily.FloorPlan Or v.ViewFamily = ViewFamily.CeilingPlan Or v.ViewFamily = ViewFamily.StructuralPlan Then

                    For Each l As Level In levelCollection

                        'create a plan
                        Dim plan As ViewPlan = ViewPlan.Create(document, v.Id, l.Id)

                        'rename the plan
                        plan.Name = Microsoft.VisualBasic.UCase(v.Name & " " & l.Name)
                    Next

                End If
            Next

            'Commit the changes to the Revit File 
            rvtTransaction.Commit()

        End Using


    End Sub