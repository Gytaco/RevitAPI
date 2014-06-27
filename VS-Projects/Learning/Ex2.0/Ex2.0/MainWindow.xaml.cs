//This is all our references for accessing particular commands
//This is how we create regions to make our code easier to read
#region Namespaces
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
#endregion

//The namespace is the location where our code is located
namespace Ex2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// // a partial class is a special type of class that relies on other classes for it's functionality
    /// </summary>
    public partial class MainWindow : Window
    {
        public string filename;
        //This is the constructor that is the instructions used to create the class
        public MainWindow()
        {           
            //This is the command that creates the MainWindow as a window in Windows OS
            InitializeComponent();
        }
        //This is an method that tells the computer what to do when we click a button.
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {          
            int i1 = checker(value1.Text);
            int i2 = checker(value2.Text);
         
            int result;
            
            if (rbplus.IsChecked == true)
            {
                result = i1 + i2;
            }
            else
            {
                result = i1 - i2;
            }

            tbResult.Text = result.ToString();     
        }
        //This is a method that returns the calculating ints after checking they can be ints
        int checker(string text)
        {
            //this assigns the field ivalue to an int
            int ivalue;
            //checks to see if the text can be converted to an int and creates a bool as well as converts the text to an int
            bool bvaluetest = Int32.TryParse(text, out ivalue);
            //If statement to check if the conversion was successful
            if (bvaluetest != true)
            {
                //If not successful a warning message displays and no assignment of data is performed
                MessageBox.Show("Please enter a whole Number");
            }
            //returns the int value to the checker Method to be used as one of the calculated values
            return ivalue;
        }
        #region String Manipulators
        private void bToUpper_Click(object sender, RoutedEventArgs e)
        {
            //string result;
            //result = tbstring.Text.ToUpper();
            //tbstringresult1.Text = result;

            //sets the text to upper case and stores it in the textbox
            tbstringresult1.Text = tbstring.Text.ToUpper();
        }

        private void bToLower_Click(object sender, RoutedEventArgs e)
        {
            //sets the text to lower case and stores it in the textblock
            tbstringresult1.Text = tbstring.Text.ToLower();
        }

        private void bRSpaces_Click(object sender, RoutedEventArgs e)
        {
            //removes all the spaces from the text string and stores it in the textblock
            tbstringresult1.Text = tbstring.Text.Replace(" ", string.Empty);
        }
        #endregion
        //Concatenates (joins) the strings together to make a single string
        private void bConcatenate_Click(object sender, RoutedEventArgs e)
        {
            //tbstringresult2.Text = (tbconc1.Text + tbconc2.Text + tbconc3.Text + tbconc4.Text);
            tbstringresult2.Text = (tbconc1.Text + tbSeperator.Text + tbconc2.Text + tbSeperator.Text + tbconc3.Text + tbSeperator.Text + tbconc4.Text);
        }
        //creates a dialogbox to allow users to select the correct file
        private void bopenfile_Click(object sender, RoutedEventArgs e)
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
            tbFileLocation.Text = filename;
           
            
        }
    }
}
