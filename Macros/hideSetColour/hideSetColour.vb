    '   This code will hide and set the colours of any elements in yoru active view. There are additional search functionalities
    '   I have left in as tags to query and access objects.
    Public Sub HideSetColour()

        'assign application to the current applicaton
        Dim application As Autodesk.Revit.ApplicationServices.Application = Me.Application

        'assigns document to the active document in use - default
        Dim document As Document = Me.ActiveUIDocument.Document

        'get the active view
        Dim activeView As View = document.ActiveView

        'create a color 
        Dim color As New Color(150, 200, 200)

        'get the solid fill pattern
        Dim fillPattern As FillPatternElement = (From fp As FillPatternElement In New FilteredElementCollector(document) _
                                                    .ofclass(GetType(FillPatternElement)) _
                                                    .cast(Of FillPatternElement)() _
                                                    Where fp.Name = "Solid fill"
                                                    Select fp).FirstOrDefault

        'setup the overide
        Dim overrideSettings As New OverrideGraphicSettings()
        overrideSettings.SetProjectionFillColor(color)
        overrideSettings.SetProjectionFillPatternId(fillPattern.id)

        'find the view called "level 1" we will hide elements in this view later
        Dim viewToHideElementsIn As view = (From v As view In New FilteredElementCollector(document) _
                                                .ofclass(GetType(viewPlan)) _
                                                .cast(Of view)()
                                                Where v.name = "Level 1"
                                                Select v).FirstOrDefault




        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'collection 1: All the walls in the project
        Dim collectionI As iEnumerable(Of ElementId) = New FilteredElementCollector(document) _
                                                        .OfCategory(BuiltInCategory.OST_Walls) _
                                                        .WhereElementIsNotElementType() _
                                                        .ToElementIds




        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'collection 2: Elements from multiple categories using an or filter
        'create a list of category filters
        Dim categoryFilterList As New list(Of elementFilter)
        categoryFilterList.add(New ElementCategoryFilter(BuiltInCategory.OST_Walls))
        categoryFilterList.add(New ElementCategoryFilter(BuiltInCategory.OST_Floors))

        'setup an OR filter
        Dim orFilter As New LogicalOrFilter(categoryFilterList)

        'get collection
        Dim collectionII As iEnumerable(Of ElementId) = New FilteredElementCollector(document) _
                                                                    .WherePasses(orFilter) _
                                                                    .ToElementIds




        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'collection 3: Elements based upon parameter values. In this case walls with type named "Generic - 300mm"
        Dim collectionIII As iEnumerable(Of ElementId) = (From w As wall In New FilteredElementCollector(document) _
                                                            .OfCategory(BuiltInCategory.OST_Walls) _
                                                            .WhereElementIsNotElementType() _
                                                            .Cast(Of Wall)()
                                                            Where w.name = "Generic - 300mm)"
                                                            Select w.id)


        'Start a transaction CRITICAL without transactions Revit cannot update
        Using rvtTransaction As New Transaction(document, "Create views")
            rvtTransaction.Start()

            'hide the walls (collectionI) in the current view
            activeView.HideElements(collectionI.ToList)

            'get all the remaining elements in the active view
            Dim remainingElements As iEnumerable(Of elementId) = New FilteredElementCollector(document, activeView.id) _
                .WhereElementIsNotElementType() _
                .toElementIds

            'override the colour and projection pattern of the remaining elements
            For Each id As ElementId In remainingElements
                activeView.setElementOverrides(id, overrideSettings)
            Next

            'Commit the changes to the Revit File 
            rvtTransaction.Commit()

        End Using


    End Sub