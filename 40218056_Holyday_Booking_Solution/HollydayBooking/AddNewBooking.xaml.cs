//Author: Pedro Mendes     MatricNum:40218056
//Description: GUI class used to get user input for a Booking details in order to create a new Booking. Also allows the user 
//  to add guests and extras to the same booking on its creation
//Date last modified: 2016-12-05


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
    /// Interaction logic for AddNewBooking.xaml
    /// </summary>
    public partial class AddNewBooking : Window
    {
        GUIFacade facade = GUIFacade.Instance; //Get a reference of the GUIFacade class to access its methods
        private List<Guest> tempGuests = new List<Guest>(); // Create a global list of guests to hold guests until the list is added to booking
        private List<Extra> tempExtras = new List<Extra>(); // Create a global list of extras to hold guests until the list is added to booking
        private string dietaryRequirements = ""; //Holds Booking dietary requirements
        

        public AddNewBooking()
        {
            InitializeComponent();
            //populate the extraTypes comboBox on Window inicialization
            cmb_selectExtraType.Items.Add("Breakfast");
            cmb_selectExtraType.Items.Add("Evening meal");
            cmb_selectExtraType.Items.Add("Car hire");
        }
        

        //Method called when add guest button is clicked. Gets user input to create a new guest and add it to the global list of guest (tempGuests)
        private void btn_addNewBookingGuests_Click(object sender, RoutedEventArgs e)
        {
            Guest aGuest = new Guest();//Create new guest instance
            // Try to assign input details to user ( succeds if the inputted values are in a correct format)
            try
            {
                aGuest.Name = txt_name.Text; // (Try) Assign guest name from user input
                aGuest.PassportNumber = txt_passportNumber.Text; // (Try) Assign guest address from user input
                int age;
                //Check if age is convertible to int (if it is a valid integer)
                if (Int32.TryParse(txt_age.Text, out age))
                {
                    aGuest.Age = age; //(Try) Assign guest age from user input
                    //If there is less than 4 guests in the list of guests than add guest to the list
                    if (tempGuests.Count < 4)
                    {
                        tempGuests.Add(aGuest); //Add guest to list of guests
                        MessageBox.Show("New guest has been added to booking."); //promt the user that the creation og guest succeded
                        cmb_guests.IsEnabled = true; //Enable guest comboBox
                        cmb_guests.Items.Add("Guest " + tempGuests.Count); //Add newly created guest to the comboBox of guests (So user can than check how many guests there is for the booking)
                        cmb_selectExtraType.IsEnabled = true; //Enable the comboBox extra type (that will allow the user to create new Extras) - At least a guest must be created before extras can be added
                        txt_name.Text = "";//clear the textbox
                        txt_passportNumber.Text = "";//clear the textbox
                        txt_age.Text = "";//clear the textbox
                    }
                    // otherwise guest is not added to the list and user is informed that creation of guest failed due to the fact that the list id full
                    else
                    {
                        MessageBox.Show("Sorry! A booking may have a maximum of 4 guests."); //Inform the user that creation of guest failed due to the list being full
                        txt_name.Text = "";//clear the textbox
                        txt_passportNumber.Text = "";//clear the textbox
                        txt_age.Text = "";//clear the textbox
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid age."); //Message shown when age input is not an integer
                    txt_age.Text = "";//clear the textbox
                }
            }
            // Catch exception messages when assignement of Guest Properties from user input fail
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);//Show exception message in a MessageBox
            }
        }


        //Method is called when the Selected item of the extra type comboBox changes
        private void cmb_selectExtraType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_extras.IsEnabled = true;//Enable add extra button when extra type is selected
            //Change visibility properties of GUI object according to the type of extra selected (Breakfast or Evening Meal), givin the user different input option
            if (cmb_selectExtraType.SelectedItem.ToString() == "Breakfast" || cmb_selectExtraType.SelectedItem.ToString() == "Evening meal")
            {
                lbl_dietaryRequirements.Visibility = System.Windows.Visibility.Visible;
                txt_dietaryRequirements.Visibility = System.Windows.Visibility.Visible;
                lbl_driverName.Visibility = System.Windows.Visibility.Hidden;
                lbl_startDate.Visibility = System.Windows.Visibility.Hidden;
                lbl_endDate.Visibility = System.Windows.Visibility.Hidden;
                txt_driverName.Visibility = System.Windows.Visibility.Hidden;
                txt_startDate.Visibility = System.Windows.Visibility.Hidden;
                txt_endDate.Visibility = System.Windows.Visibility.Hidden;
            }
            //Change visibility properties of GUI object according to the type of extra selected (CarHire), givin the user different input option
            else if (cmb_selectExtraType.SelectedItem.ToString() == "Car hire")
            {
                lbl_dietaryRequirements.Visibility = System.Windows.Visibility.Hidden;
                txt_dietaryRequirements.Visibility = System.Windows.Visibility.Hidden;
                lbl_driverName.Visibility = System.Windows.Visibility.Visible;
                lbl_startDate.Visibility = System.Windows.Visibility.Visible;
                lbl_endDate.Visibility = System.Windows.Visibility.Visible;
                txt_driverName.Visibility = System.Windows.Visibility.Visible;
                txt_startDate.Visibility = System.Windows.Visibility.Visible;
                txt_endDate.Visibility = System.Windows.Visibility.Visible;
            }
            else 
            {
                btn_extras.IsEnabled = false;//Disable add_extra button
            }
        }

        //Method called when add extra button is clicked. Gets user input to create a new extra and add it to the global list of extras (tempExtras)
        private void btn_extras_Click(object sender, RoutedEventArgs e)
        {
            //If Brekfast extra is selected...
            if (cmb_selectExtraType.SelectedItem.ToString() == "Breakfast")
            {
                Breakfast aBreakfast = new Breakfast();//Create new Breakfast object
                tempExtras.Add(aBreakfast);//Add Breakfast Object to the booking list of extras
                dietaryRequirements = txt_dietaryRequirements.Text;//Set the booking dietary requirements from user input
                txt_dietaryRequirements.Text = "";//clears the textbox
                cmb_extras.IsEnabled = true; //Enable the extras combobox
                cmb_extras.Items.Add(cmb_selectExtraType.SelectedItem.ToString()); //Add the new extra (type Breakfast) to the extras comboBox
                MessageBox.Show("Breakfast has been successfully added");//Informs the user that Breakfast extra was added successfuly
            }
            //If Evening Meal extra is selected...
            else if (cmb_selectExtraType.SelectedItem.ToString() == "Evening meal")
            {
                EveningMeal anEveningMeal = new EveningMeal();//Create new EveningMeal object
                tempExtras.Add(anEveningMeal);//Add Evening Meal Object to the booking list of extras
                dietaryRequirements = txt_dietaryRequirements.Text;//Set the booking dietary requirements from user input
                txt_dietaryRequirements.Text = "";//clears the textbox
                cmb_extras.IsEnabled = true;//Enable the extras combobox
                cmb_extras.Items.Add(cmb_selectExtraType.SelectedItem.ToString());//Add the new extra (type EveningMeal) to the extras comboBox
                MessageBox.Show("Evening meal has been successfully added");//Informs the user that EveningMeal extra was added successfuly
            }
            else if (cmb_selectExtraType.SelectedItem.ToString() == "Car hire")
            {
                DateTime arrivalDate;//local variable to hold a DateTime value (Car Hire pick-up date)
                DateTime departureDate;//local variable to hold a DateTime value (Car Hire return date)
                if (DateTime.TryParse(txt_startDate.Text, out arrivalDate)) //Check if pick-up date input by the user is a valid DateTime format
                {
                    if(DateTime.TryParse(txt_endDate.Text, out departureDate)) //Check if return date input by the user is a valid DateTime format
                    {
                        //Try to assign CarHire properties from user input and create a CarHire extra
                        try
                        {
                            CarHire aCarHire = new CarHire(); //Create new CarHire object
                            aCarHire.DriverName = txt_driverName.Text; // (try to) Set driver name from user input
                            aCarHire.StartDate = arrivalDate; // (try to) Set pick-up date name from user input
                            aCarHire.ReturnDate = departureDate; // (try to) Set return date from user input
                            tempExtras.Add(aCarHire); //Add Car Hire Object to the booking list of extras
                            txt_driverName.Text = ""; //clear the textbox
                            txt_startDate.Text = ""; //clear the textbox
                            txt_endDate.Text = ""; //clear the textbox
                            cmb_extras.IsEnabled = true; //enable comboBox with the extras 
                            cmb_extras.Items.Add(cmb_selectExtraType.SelectedItem.ToString()); //Add the new extra (type CarHire) to the extras comboBox
                            MessageBox.Show("Car hire has been successfully added");//Informs the user that CarHire extra was added successfuly
                        }
                        // Catch exception messages if the assignemt of CarHire properties fail
                        catch (Exception excep)
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
                    MessageBox.Show("Please enter a valid pick-up date. (YYYY-MM-DD)");//Promp user to input a valid DateTime showing the correct format
                    txt_startDate.Text = "";//clear the textbox
                }
            }
        }


        private void btn_addNewBookingDone_Click(object sender, RoutedEventArgs e)
        {
            DateTime arrivalDate; //local variable to hold a DateTime value (Booking arrivalDate date)
            DateTime departureDate; //local variable to hold a DateTime value (Booking departureDate date)
            if (DateTime.TryParse(txt_arrivalDate.Text, out arrivalDate)) //Check if Booking arrivalDate input by the user is a valid DateTime format
            {
                if (DateTime.TryParse(txt_departureDate.Text, out departureDate)) //Check if Booking departureDate input by the user is a valid DateTime format
                {
                    if (tempGuests.Count != 0) //If there is at least 1 guest for the booking
                    {
                        //Try to assign Booking properties from user input and create a NewBooking with associated guests and extras
                        try
                        {
                            Booking aBooking = new Booking(); //Create new Booking object
                            aBooking.ArrivalDate = arrivalDate; // (try to) Set booking arrivalDate from user input
                            aBooking.DepartureDate = departureDate; // (try to) Set booking departureDate from user input
                            aBooking.Guests = tempGuests; //Add list of Guests (previously created) to booking
                            aBooking.Extras = tempExtras; //Add list of Extras (previously created) to booking
                            aBooking.DietaryRequirements = dietaryRequirements; // Set the booking dietary requirements (if any) from user input
                            MainWindow mainWin = Owner as MainWindow; //Get a reference from Main Window to access its properties
                            facade.SetCurrentCostumer(Int32.Parse(mainWin.txt_costumerReferenceNumber.Text)); //Set CurrentCostumer in the facade using the reference number input by the user in Main WIndow
                            facade.AddBooking(aBooking); // Add the newly created booking to the CurrentCustomer (using the facade method)
                            facade.SaveToCSV(); //Save new Booking to CSV
                            // Update text and combo Boxes on the Booking Menu (on main window)
                            MessageBox.Show("Booking successfully added! Reference Number:" + aBooking.ReferenceNumber.ToString());// Inform the user the booking has been successfuly created
                            mainWin.cmbBox_Bookings.IsEnabled = true; //Enable the Bookings comboBox in Main window (so the user can select a booking)
                            mainWin.cmbBox_Bookings.Items.Add(aBooking.ReferenceNumber); //Add newly created booking to the Bookings comboBox in main window
                            this.Close(); //Close the window
                        }
                        // Catch exception messages if the assignemt of Booking properties fail
                        catch (Exception excep)
                        {
                            MessageBox.Show(excep.Message); //Print message in Message Box
                        }
                    }
                    else
                    {
                        MessageBox.Show("New bookings must have at least one guest. Please add a guest to booking");
                    }
                }
                else
                {
                    MessageBox.Show("Departure date entered is not valid. Please enter a valid date format (YYYY-MM-DD)");
                }
            }
            else
            {
                MessageBox.Show("Arrival date entered is not valid. Please enter a valid date format (YYYY-MM-DD)");
            }
        }
    }
}
