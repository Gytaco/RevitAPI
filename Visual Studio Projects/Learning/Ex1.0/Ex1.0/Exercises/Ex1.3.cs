/*
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
namespace Ex1._0
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
            //asigns space DataType int to these to variable names
            //this runs our checker method that checks if the value can be converted
            //to an int and returns a result, otherwise we get a warning message
            int ivalue1 = checker(value1.Text);
            int ivalue2 = checker(value2.Text);

            //creates a new int and gets the result of the addition of two int values
            int iresult = ivalue1 + ivalue2;

            //This gets the result of the two values and puts the value into the text of the textbox
            tbResult.Text = iresult.ToString();
        }
        //method called checker that takes a string argument and if correct returns the converted int
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
    }
}
*/