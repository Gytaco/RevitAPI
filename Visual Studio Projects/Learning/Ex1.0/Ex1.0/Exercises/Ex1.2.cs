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
            int ivalue1;
            int ivalue2;

            //try to convert the text in the textbox to an int if successful bvaluetest equals true
            //and the out value is the int conversion result
            bool bvaluetest = Int32.TryParse(value1.Text, out ivalue1);
            //if statement that checks if the bvaluetext is true or false
            if (bvaluetest == false)
            {
                //If the text cannot be converted displays the following message and exits the code
                MessageBox.Show("Please enter a whole number");
            }

            bool bvaluetest1 = Int32.TryParse(value2.Text, out ivalue2);
            if (bvaluetest1 == false)
            {
                MessageBox.Show("Please enter a whole number");
            }

            //creates a new int and gets the result of the addition of two int values
            int iresult = ivalue1 + ivalue2;

            //This gets the result of the two values and puts the value into the text of the textbox
            tbResult.Text = iresult.ToString();
        }
    }
}
*/