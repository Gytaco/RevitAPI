import clr
import sys
clr.AddReference('ProtoGeometry')
from Autodesk.DesignScript.Geometry import *

# Import DocumentManager and TransactionManager
clr.AddReference('RevitServices')
import RevitServices
from RevitServices.Persistence import DocumentManager
from RevitServices.Transactions import TransactionManager

# Import RevitAPI
clr.AddReference('RevitAPI')
clr.AddReference('RevitAPIUI')
import Autodesk
from Autodesk.Revit.DB import *
from Autodesk.Revit.UI import *

#doc = DocumentManager.Instance.CurrentDBDocument
#uiapp = DocumentManager.Instance.CurrentUIApplication
#app = uiapp.Application

#pyt_path = r'C:\Program Files (x86)\IronPython 2.7\Lib'
#sys.path.append(pyt_path)

#from Autodesk.DesignScript.Geometry import *

#from System import Array
#from System.Collections.Generic import *
#import math

#The inputs to this node will be stored as a list in the IN variable.
elements = IN
#List Example = List[ElementId]()

#TransactionManager.Instance.EnsureInTransaction(doc)	
	
#TransactionManager.Instance.TransactionTaskDone()

#Assign your output to the OUT variable
OUT = 0
