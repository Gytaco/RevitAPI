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
using System.IO;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Application
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : Window
    {
        public string filename;
        public static Document doc;

        public UserControl1(Document doco)
        {
            doc = doco;
            InitializeComponent();
        }

        private void bopen_Clik(object sender, RoutedEventArgs e)
        {
            
            //Creates a select file dialog box in windows
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //Displays only that type of format file
            dlg.DefaultExt = ".txt";
            //Displays the string in the options for file selection
            dlg.Filter = "Text documents (.txt)|*.txt";

            //opens the Dialog box
            Nullable<bool> result = dlg.ShowDialog();

            //gets the location and filename as a text string
            filename = dlg.FileName;
            File_Location.Text = filename;

            
            }

        

        private void getResults_Click(object sender, RoutedEventArgs e)
        {
            //array string with no size
            string[] aData;
            //command that reads all the lines in our text file from the location
            aData = File.ReadAllLines(filename);
            //gets the length of our string array
            int len = (aData.Length - 1);

            //transaction to update our parameters
            using (Transaction t = new Transaction(doc, "comment updates"))
            {
                //start the transaction
                t.Start();

                //a for statement that is similar to foreach but is structured for arrays
                for (int i = 1; i <= len; i++)
                {
                    //get the char location of the first tab in a line of text
                    int iNo = aData[i].IndexOf('\t');
                    //remove the rest of the string based on the position of the character
                    string sId = aData[i].Remove(iNo);

                    //removes the Id data from the string line
                    string sComment1 = aData[i].Remove(0, iNo + 1);
                    //get the char location of the second(now first) tab in the line of text
                    int iNo2 = sComment1.IndexOf('\t');
                    //removes the name data from the line of text leaving only comments
                    string sComment2 = sComment1.Remove(0, iNo2 + 1);

                    //convert the string id into an integer
                    int iid = Int32.Parse(sId);
                    //create a new elementid from the converted integer
                    ElementId obj = new ElementId(iid);
                    //get the element from the element id
                    Element ge = doc.GetElement(obj);

                    //update the comments parameter with the information in the text
                    ge.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set(sComment2);
                }
                //commit the transaction
                t.Commit();
                }
            TaskDialog.Show("Import Success!!", "Your Results have imported successfully");
        }
    }
}
