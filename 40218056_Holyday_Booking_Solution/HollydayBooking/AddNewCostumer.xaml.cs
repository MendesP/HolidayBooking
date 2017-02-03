//Author: Pedro Mendes     MatricNum:40218056
//Description: GUI class used to get user input for custumer details in order to create new customers.
//Date last modified: 2016-12-07
//

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
    /// Interaction logic for AddNewCostumer.xaml
    /// </summary>
    public partial class AddNewCostumer : Window
    {
        GUIFacade facade = GUIFacade.Instance; //Get an instance of the GUIFacade class
        public AddNewCostumer()
        {
            InitializeComponent();
        }

        //Method validates user input for custumer name and address fields in order to create new customers
        private void btn_addNewCostumerDone_Click(object sender, RoutedEventArgs e)
        {
            //Check if name field is not empty
            if (txt_addNewCostumerName.Text == "")
            {
                MessageBox.Show("Please enter a valid customer name!");
            }
            //Check if address field is not empty
            else if (txt_addNewCostumerAddress.Text == "")
            {
                MessageBox.Show("Please enter a valid customer address!");
            }
            else
            {
                //Create new customer object
                Costumer aCostumer = new Costumer();
                // try to assign values to the new customer object
                try
                {
                    aCostumer.Name = txt_addNewCostumerName.Text; // (Try to) assign costumer name from user input
                    aCostumer.Address = txt_addNewCostumerAddress.Text; // (Try to) assign costumer address from user input
                    facade.AddCostumer(aCostumer); //use the facade class to add new customer to customer list
                    facade.SaveToCSV(); //saves it to CSV file
                    facade.SetCurrentCostumer(aCostumer.ReferenceNumber); //Sets the current custumer in the facade class to the customer just created
                    MessageBox.Show("New Costumer has been added. Costumer reference number: " + aCostumer.ReferenceNumber);
                    this.Close();
                }
                // If assignement of name or address fails, catch and show exception message
                catch (Exception excep)
                {
                    MessageBox.Show(excep.Message);
                }
            }
        }
    }
}
