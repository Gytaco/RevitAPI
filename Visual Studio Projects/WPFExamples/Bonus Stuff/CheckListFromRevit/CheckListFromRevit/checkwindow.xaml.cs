using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;

namespace CheckListFromRevit
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class checkwindow : Window
    {
        //creates a private document to access all areas of the class
        private Document doc;

        //adds an argument to the constructor to access the revit application
        public checkwindow(ExternalCommandData Data)
        {
            //initailises the dialog
            InitializeComponent();
            //gets the active document
            doc = Data.Application.ActiveUIDocument.Document;
        }
        //this is the method for clicking the CreateList button
        private void bClick(object sender, RoutedEventArgs e)
        {
            //gets a collection wall types
            ICollection<Element> wallT = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls)
                .WhereElementIsElementType().ToElements();
            //cycles through the wall types
            foreach (Element ew in wallT)
            {
                //adds the walltype names one by one to the list
                ObjectList.Items.Add(ew.Name);
            }
            
        }
        //method for clicking the ClearList Button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Deletes the objects in the list
            ObjectList.Items.Clear();
        }
    }
}
