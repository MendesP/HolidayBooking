//Author: Pedro Mendes     MatricNum:40218056
//Description: GUI class used to amend custumer details of existing Customers
//Date last modified: 2016-12-07


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
using System.Windows.Shapes;

namespace HollydayBooking
{
    /// <summary>
    /// Interaction logic for AmendCostumer.xaml
    /// </summary>
    public partial class AmendCostumer : Window
    {
        GUIFacade facade = GUIFacade.Instance; //Create a reference to the GUIfacade class 
        public AmendCostumer()
        {
            InitializeComponent();
        }

        // Updates the details of an existing costumer
        private void btn_amendCostumerUpdate_Click(object sender, RoutedEventArgs e)
        {
            Costumer aCostumer = facade.CurrentCostumer; //Get Current costumer from the facade
            // Try to re-assing the values of the customer details from the user input
            try
            {
                aCostumer.Name = txt_amendCostumerName.Text; // (Try to) re-assign costumer name from user input
                aCostumer.Address = txt_amendCostumerAddress.Text; // (Try to) re-assign costumer address from user input
                facade.SaveToCSV(); // Save Customer Details (amended) to CSV file
                MessageBox.Show("Costumer details were successfully updated!"); //Insforms the user the customer detaisl were successfully updated
                MainWindow mainWin = Owner as MainWindow; // Get a reference of main window to access its properties
                mainWin.txt_costumerReferenceNumber.Text = ""; //clear textbox in main window
                mainWin.lbl_costumerNameOutput.Content = ""; // clear textbox in main window
                mainWin.lbl_costumerAddressOutput.Content = ""; //clear textbox in main window
                this.Close(); //close this window
            }
            // If re-assignement of name or address fails, catch and show exception message
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }
    }
}
