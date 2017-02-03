//Author: Pedro Mendes     MatricNum:40218056
//Description: GUI class used to get user input for extra details in order to create new extras.
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
    /// Interaction logic for addExtra.xaml
    /// </summary>
    public partial class addExtra : Window
    {
        //Get a GUIFacade reference
        GUIFacade facade = GUIFacade.Instance;
        public addExtra()
        {
            InitializeComponent();
        }

        // Change the GUI properties based on the type of extra to be added (Different options are given to the user depending on the type of extra)
        private void cmb_extraTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_addExtra.IsEnabled = true; //Enable the addExtra button when user selects an extra type from the combobox
            if (cmb_extraTypes.SelectedItem.ToString() == "Breakfast" || cmb_extraTypes.SelectedItem.ToString() == "Evening meal")
            {
                //Change visibility setting of labels and text boxes
                lbl_dietaryRequirements.Visibility = System.Windows.Visibility.Visible;
                txt_dietaryRequirements.Visibility = System.Windows.Visibility.Visible;
                lbl_driverName.Visibility = System.Windows.Visibility.Hidden;
                lbl_startDate.Visibility = System.Windows.Visibility.Hidden;
                lbl_endDate.Visibility = System.Windows.Visibility.Hidden;
                txt_driverName.Visibility = System.Windows.Visibility.Hidden;
                txt_startDate.Visibility = System.Windows.Visibility.Hidden;
                txt_endDate.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (cmb_extraTypes.SelectedItem.ToString() == "Car hire")
            {
                //Change visibility setting of labels and text boxes
                lbl_dietaryRequirements.Visibility = System.Windows.Visibility.Hidden;
                txt_dietaryRequirements.Visibility = System.Windows.Visibility.Hidden;
                lbl_driverName.Visibility = System.Windows.Visibility.Visible;
                lbl_startDate.Visibility = System.Windows.Visibility.Visible;
                lbl_endDate.Visibility = System.Windows.Visibility.Visible;
                txt_driverName.Visibility = System.Windows.Visibility.Visible;
                txt_startDate.Visibility = System.Windows.Visibility.Visible;
                txt_endDate.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void btn_addExtra_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = Owner as MainWindow; //Create a reference to the main window (in order to have access to its properties)
            Costumer aCostumer = facade.CurrentCostumer; //Get the currentCostumer object
            Booking aBooking = facade.CurrentBooking; //Get the currentBooking object

            //If Brekfast extra is selected...
            if (cmb_extraTypes.SelectedItem.ToString() == "Breakfast")
            {
                Breakfast aBreakfast = new Breakfast();//Create new Breakfast object
                aBooking.Extras.Add(aBreakfast);//Add Breakfast Object to the booking list of extras
                aBooking.DietaryRequirements = txt_dietaryRequirements.Text;//Set the booking dietary requirements from user input
                mainWin.txt_bookingDietaryRequirements.Text = aBooking.DietaryRequirements;//update the dietary requirement of booking on main window textbox
                txt_dietaryRequirements.Text = "";//clear the textbox
                mainWin.cmb_bookingExtras.IsEnabled = true;//enable comboBox with the extras on main window
                mainWin.cmb_bookingExtras.Items.Add(cmb_extraTypes.SelectedItem.ToString());//Add extra Type (Breakfast) to the extra comboBox in main window
                MessageBox.Show("Breakfast has been successfully added"); //Prompt user that Breakfast extra has been created
                this.Close();
            }
            //If Evening Meal extra is selected...
            else if (cmb_extraTypes.SelectedItem.ToString() == "Evening meal")
            {
                EveningMeal anEveningMeal = new EveningMeal();//Create new Evening Meal object
                aBooking.Extras.Add(anEveningMeal);//Add Evening Meal Object to the booking list of extras
                aBooking.DietaryRequirements = txt_dietaryRequirements.Text;//Set the booking dietary requirements from user input
                mainWin.txt_bookingDietaryRequirements.Text = aBooking.DietaryRequirements;//update the dietary requirement of booking on main window textbox
                txt_dietaryRequirements.Text = "";//clear the textbox
                mainWin.cmb_bookingExtras.IsEnabled = true;//enable comboBox with the extras on main window
                mainWin.cmb_bookingExtras.Items.Add(cmb_extraTypes.SelectedItem.ToString());//Add extra Type (Evening Meal) to the extra comboBox in main window
                MessageBox.Show("Evening Meal has been successfully added"); //Prompt user that Evening Meal extra has been created
                this.Close();
            }
            //If Car Hire extra is selected...
            else if (cmb_extraTypes.SelectedItem.ToString() == "Car hire")
            {
                DateTime arrivalDate; //local variable to hold a DateTime value (Car Hire pick-up date)
                DateTime departureDate; //local variable to hold a DateTime value (Car Hire return Date)
                if (DateTime.TryParse(txt_startDate.Text, out arrivalDate)) //Check if pick-up date input is a valid DateTime value
                {
                    if (DateTime.TryParse(txt_endDate.Text, out departureDate)) //Check if return date input is a valid DateTime value
                    {
                        // Try to assign input values to CarHire object
                        try
                        {
                            CarHire aCarHire = new CarHire(); //Create new Car Hire object
                            aCarHire.DriverName = txt_driverName.Text; // (try to) Set driver name from user input
                            aCarHire.StartDate = arrivalDate; // (try to) Set pick-up date from user input
                            aCarHire.ReturnDate = departureDate; // (try to) Set return date from user input
                            aBooking.Extras.Add(aCarHire); //Add Car Hire Object to the booking list of extras
                            txt_driverName.Text = "";//clear the textbox
                            txt_startDate.Text = "";//clear the textbox
                            txt_endDate.Text = "";//clear the textbox
                            mainWin.cmb_bookingExtras.IsEnabled = true; //enable comboBox with the extras on main window
                            mainWin.cmb_bookingExtras.Items.Add(cmb_extraTypes.SelectedItem.ToString()); //Add extra Type (Car Hire) to the extra comboBox in main window
                            MessageBox.Show("Car hire has been successfully added"); //Prompt user that Car Hire extra has been created
                            this.Close();
                        }
                        // Catch exception messages if the assignemt of CarHire properties fail
                        catch(Exception excep)
                        {
                            MessageBox.Show(excep.Message);//Print message in Message Box
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid return date date. (YYYY-MM-DD)");//Promp user to input a valid DateTime showing the correct format
                        txt_endDate.Text = "";//clear the textbox
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid pick-up date. (YYYY-MM-DD)"); //Promp user to input a valid DateTime showing the correct format
                    txt_startDate.Text = "";//clear the textbox
                }
            }
        }
    }
}
