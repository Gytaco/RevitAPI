#this script takes a list of elements and returns the types only
import clr
clr.AddReference('RevitAPI')
clr.AddReference('RevitAPIUI')
from Autodesk.Revit.DB import *
from System.Collections.Generic import List
import Autodesk

doc = IN0
fams = IN1

elementlist = list()
idlist = list()
totalist = list()
#gets the list of elements and creates of list of ids
for item in fams:
	idlist.append(item.Id)

totalist = List[ElementId](idlist)
	
collector = FilteredElementCollector(doc,totalist)
collector.WhereElementIsElementType()
elementlist.append(collector.ToElements())

OUT = elementlist
