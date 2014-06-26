/*
  The code will get all the rooms in the project and make an individual floor to each of the internal boundaries.
  It will then offset the floor by the floor type thickness. It is just set to the current floor for now.
*/

		public void interiorFloorByRoom()
		{
			//SETUP public ILIST in the CLASS!!!!
			
			
			//sets the variable to the current appliction
			Application app = this.Application;
			//sets the variable to the active project
			Document doc = this.ActiveUIDocument.Document;
			
			//finds all the rooms in the project
			ICollection<Element> eRoom = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Rooms)
				.WhereElementIsNotElementType().ToElements();
			
			//cycles through each room in the project
			foreach (Element e in eRoom)
			{
				//gets the element as a room object
				Room r = e as Room;
				//creates a new array of curves to store the room outline
	            CurveArray floor = new CurveArray();
				//sets the rules for the spatial element boundaries (centre, external/internal etc..)
				SpatialElementBoundaryOptions opt = new SpatialElementBoundaryOptions();
	 			//sets the option to respect the wall finish of the room
	  			opt.SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.Finish;
				//gets the boundary segements of the room and applies the options
	  			IList<IList<AliasBoundarySegment>> loops = r.GetBoundarySegments(opt);
				//loops through the boundary for the segments
				foreach (IList<AliasBoundarySegment> a in loops)
				{
					//cycles through boundary to get the individual curves
					foreach (AliasBoundarySegment item in a)
					{
						//get the curve
						Curve floorCurve = item.Curve;
						//adds the curve to the curve array
						floor.Append(floorCurve);
					}               
	            }
				//clears the change floor height for each Room so it starts new again
	            changefloorheight.Clear();
				//this starts the an event tracker to watch when our program creates objects and looks
				//for the Floor object to be created so we can get the id number of the new item
				app.DocumentChanged += new EventHandler<DocumentChangedEventArgs>(OnDocumentChanged);
				//starts the transaction
				Transaction tran = new Transaction(doc, "Generate Floor");
	           	tran.Start();
				//creates the floor object
				doc.Create.NewFloor(floor, false);
				//commits the transaction
				tran.Commit();
				//turns off the event tracker for the object creation
				app.DocumentChanged -= new EventHandler<DocumentChangedEventArgs>(OnDocumentChanged );
				//cycles through all the objects id's that were created while the event was open
	            foreach (ElementId id in changefloorheight)
	            {
	            	//tries to select the element id as a floor object
	                Floor fltest = doc.GetElement(id) as Floor;
					//If the object is not null then it is a floor otherwise it will skip this and check
					//the next id
	                if (fltest != null)
	                {
	                	//gets the parameters of the floor thickness
	                    Parameter p = fltest.get_Parameter(BuiltInParameter.FLOOR_ATTR_THICKNESS_PARAM);
						//gets the parameter of the floor above height level
	                    Parameter offset = fltest.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM);
						//starts another transaction to update the floor parameter
	                    Transaction tran2 = new Transaction(doc, "Generate Floor");
	                    tran2.Start();
						//sets the offset of the floor to the desired height
	                    offset.Set(p.AsDouble());
	                    //commits the transaction
	                    tran2.Commit();
	                }
	            }
        	}
		}
		//this method turns on and off the even track that tracks changes to the revit database
        void OnDocumentChanged(object sender, DocumentChangedEventArgs e)
	        {
        		//finds the element id of the room based on looking for the floor only
	            changefloorheight.AddRange(e.GetAddedElementIds());
	        }
