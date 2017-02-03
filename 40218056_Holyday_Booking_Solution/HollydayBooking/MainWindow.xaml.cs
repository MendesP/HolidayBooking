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

namespace HollydayBooking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        GUIFacade facade = GUIFacade.Instance; //Get an instance of the GUI facade

        public MainWindow()
        {
            InitializeComponent();
            facade.LoadFromCSV(); //Load all records from CSV
        }

        // Opens a new window to allow the user to add new costumer
        private void btn_addCostumer_Click(object sender, RoutedEventArgs e)
        {
            AddNewCostumer addCostumerWin = new AddNewCostumer();
            addCostumerWin.ShowDialog(); 
        }


        // Finds a Costumer by getting a valid reference number input from the user (Does some validation of user input)
        private void btn_findCostumer_Click(object sender, RoutedEventArgs e)
        {
            int refNumber;
            if (!Int32.TryParse(txt_costumerReferenceNumber.Text, out refNumber)) //if refnumber input is not an int
            {
                MessageBox.Show("Please input a valid reference number");
                txt_costumerReferenceNumber.Text = "";
            }
            else // if reference number is a valid int
            {
                Costumer aCostumer = facade.GetCostumer(refNumber);
                if (aCostumer != null) //if costumer was found
                {
                    cmbBox_Bookings.Items.Clear();
                    cmbBox_Bookings.IsEnabled = false;
                    facade.SetCurrentCostumer(refNumber);
                    EnableCostumerMenuOptions();
                    lbl_costumerNameOutput.Content = aCostumer.Name;
                    lbl_costumerAddressOutput.Content = aCostumer.Address;
                    
                    // Add all the booking reference numbers to the booking menu comboBox
                    foreach (Booking booking in aCostumer.CostumerBookings)
                    {
                        cmbBox_Bookings.Items.Add(booking.ReferenceNumber);
                        cmbBox_Bookings.IsEnabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Costumer with the reference number entered does not exist.");
                    DisableCostumerMenuOptions();
                }
            }
        }

        //Method deletes a Customer Given the reference number
        private void btn_removeCostumer_Click(object sender, RoutedEventArgs e)
        {
            int refNumber;
            if (!Int32.TryParse(txt_costumerReferenceNumber.Text, out refNumber))
            {
                MessageBox.Show("Please input a valid reference number");
            }
            else
            {
                Costumer aCostumer = facade.GetCostumer(refNumber);
                if (aCostumer != null)
                {
                    facade.RemoveCostumer(refNumber);
                    facade.SaveToCSV();
                    MessageBox.Show("Costumer Successfully removed");
                }
                else
                {
                    MessageBox.Show("Costumer with the reference number entered does not exist.");
                }
            }
            txt_costumerReferenceNumber.Text = "";
            DisableCostumerMenuOptions();
        }

        //Change some main window properties on text changed (disable buttons and clears textboxs and comboboxes)
        private void txt_costumerReferenceNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            cmbBox_Bookings.Items.Clear();
            cmbBox_Bookings.IsEnabled = false;
            btn_searchBooking.IsEnabled = false;
            DisableCostumerMenuOptions();
        }

        //Open new window to allow user to amend an existing customer
        private void btn_amendCostumer_Click(object sender, RoutedEventArgs e)
        {
            AmendCostumer amendCostumerWin = new AmendCostumer();
            amendCostumerWin.Owner = this;
            amendCostumerWin.txt_amendCostumerRefNumber.Text = txt_costumerReferenceNumber.Text;
            amendCostumerWin.txt_amendCostumerName.Text = lbl_costumerNameOutput.Content.ToString();
            amendCostumerWin.txt_amendCostumerAddress.Text = lbl_costumerAddressOutput.Content.ToString();
            amendCostumerWin.ShowDialog();
        }


        //Open new window to allow user to add a new booking
        private void btn_addBooking_Click(object sender, RoutedEventArgs e)
        {
            int refNumb;
            if (Int32.TryParse(txt_costumerReferenceNumber.Text, out refNumb))
            {
                AddNewBooking addNewBookingWin = new AddNewBooking();
                addNewBookingWin.Owner = this;
                addNewBookingWin.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a costumer before adding a booking.");
            }
        }

        //Change some main window properties on text changed (disable buttons and clears textboxs and comboboxes)
        private void cmbBox_Bookings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbBox_Bookings.SelectedItem != null)
            {
                if (cmbBox_Bookings.SelectedItem.ToString() == "")
                {
                    btn_removeBooking.IsEnabled = false;
                    btn_searchBooking.IsEnabled = false;
                    btn_invoice.IsEnabled = false;
                }
                else
                {
                    facade.SetCurrentBooking(Int32.Parse(cmbBox_Bookings.SelectedItem.ToString()));
                    btn_removeBooking.IsEnabled = true;
                    btn_searchBooking.IsEnabled = true;
                    btn_invoice.IsEnabled = true;
                }
            }
            else
            {
                btn_removeBooking.IsEnabled = false;
                btn_searchBooking.IsEnabled = false;
                btn_invoice.IsEnabled = false;
            }
        }


        // Loads the booking selected in the combobox and loads its details to main window  
        private void btn_searchBooking_Click(object sender, RoutedEventArgs e)
        {
            Costumer aCostumer = facade.CurrentCostumer;
            Booking aBooking = null;
            if (cmbBox_Bookings.SelectedItem != null)
            {
                aBooking = facade.GetBooking(Int32.Parse(cmbBox_Bookings.SelectedItem.ToString()));
            }

            if (aBooking == null)
            {
                MessageBox.Show("Booking does not exist");
            }
            else
            {
                facade.SetCurrentBooking(aBooking.ReferenceNumber);
                ClearBookingMenu();
                txt_bookingReferenceNumber.Text = aBooking.ReferenceNumber.ToString();
                txt_bookingArrivalDate.Text = aBooking.ArrivalDate.ToString(aBooking.DatePattern);
                txt_bookingDepartureDate.Text = aBooking.DepartureDate.ToString(aBooking.DatePattern);
                txt_bookingDietaryRequirements.Text = aBooking.DietaryRequirements.ToString();
                btn_removeBooking.IsEnabled = true;
                btn_addGuest.IsEnabled = true;
                btn_addExtra.IsEnabled = true;
                btn_saveChanges.IsEnabled = true;
                btn_invoice.IsEnabled = true;
                foreach (Guest aGuest in aBooking.Guests)
                {
                    cmb_bookingGuests.Items.Add(aGuest.Name);
                }
                cmb_bookingGuests.IsEnabled = true;

                if (aBooking.Extras.Count > 0)
                {
                    cmb_bookingExtras.IsEnabled = true;
                    foreach (Extra anExtra in aBooking.Extras)
                    {
                        if (anExtra.GetType().ToString() == "HollydayBooking.Breakfast")
                        {
                            cmb_bookingExtras.Items.Add("Breakfast");
                        }
                        else if (anExtra.GetType().ToString() == "HollydayBooking.EveningMeal")
                        {
                            cmb_bookingExtras.Items.Add("Evening meal");
                        }
                        else if (anExtra.GetType().ToString() == "HollydayBooking.CarHire")
                        {
                            cmb_bookingExtras.Items.Add("Car hire");
                        }
                    }
                }

            }
        }

        //Add a new guest to an existing booking
        private void btn_addGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Costumer aCostumer = facade.CurrentCostumer;
                Booking aBooking = facade.CurrentBooking;
                Guest aGuest = new Guest();
                aGuest.Name = txt_guestName.Text;
                aGuest.PassportNumber = txt_guestPassportNumber.Text;
                int guestAge;
                if (Int32.TryParse(txt_guestAge.Text, out guestAge))
                {
                    aGuest.Age = Int32.Parse(txt_guestAge.Text);
                    aBooking.Guests.Add(aGuest);
                    cmb_bookingGuests.Items.Add(aGuest.Name.ToString());
                    MessageBox.Show("New guest was successfully added!");
                    txt_guestName.Text = "";
                    txt_guestPassportNumber.Text = "";
                    txt_guestAge.Text = "";
                }
                else
                {
                    MessageBox.Show("Please insert a valid Guest age!");
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        // Loads details of the selected guest in the combo box
        private void cmb_bookingGuests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Costumer aCostumer = facade.CurrentCostumer;
            Guest aGuest = null;
            if (cmb_bookingGuests.SelectedItem != null)
            {
                Booking aBooking = facade.CurrentBooking;
                if (cmb_bookingGuests.SelectedIndex < aBooking.Guests.Count())
                { 
                    aGuest = facade.GetGuest(cmb_bookingGuests.SelectedIndex);
                }  
            }

            if (aGuest != null)
            {
                txt_guestName.Text = aGuest.Name;
                txt_guestPassportNumber.Text = aGuest.PassportNumber;
                txt_guestAge.Text = aGuest.Age.ToString();
                btn_deleteGuest.IsEnabled = true;
                btn_amendGuest.IsEnabled = true;
            }
            else
            {
                btn_deleteGuest.IsEnabled = false;
                btn_amendGuest.IsEnabled = false;
            }
        }

        // Delete the selected booking from the combobox
        private void btn_removeBooking_Click(object sender, RoutedEventArgs e)
        {
            int bookingRefNumber = Int32.Parse(cmbBox_Bookings.SelectedItem.ToString());
            facade.RemoveBooking(bookingRefNumber);
            facade.SaveToCSV();
            MessageBox.Show("Booking with referece number: " + bookingRefNumber + " has been successfully deleted!");
            cmbBox_Bookings.Items.RemoveAt(cmbBox_Bookings.SelectedIndex);
            cmbBox_Bookings.Items.Clear();
            cmbBox_Bookings.IsEnabled = false;
            ClearBookingMenu();
        }

        // Delete the selected guest from the combobox
        private void btn_deleteGuest_Click(object sender, RoutedEventArgs e)
        {
            facade.RemoveGuest(cmb_bookingGuests.SelectedIndex);
            MessageBox.Show("Guest sucessfully deleted!");
            cmb_bookingGuests.Items.RemoveAt(cmb_bookingGuests.SelectedIndex);
            txt_guestName.Text = "";
            txt_guestPassportNumber.Text = "";
            txt_guestAge.Text = "";

            if (cmb_bookingGuests.Items.Count == 0)
            {
                btn_deleteGuest.IsEnabled = false;
                btn_amendGuest.IsEnabled = false;
            }
        }

        // Amend a guest given its index in the list of guests
        private void btn_amendGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int guestAge;
                if (Int32.TryParse(txt_guestAge.Text, out guestAge))
                {
                    Guest aGuest = facade.GetGuest(cmb_bookingGuests.SelectedIndex);
                    aGuest.Name = txt_guestName.Text;
                    aGuest.PassportNumber = txt_guestPassportNumber.Text;
                    aGuest.Age = Int32.Parse(txt_guestAge.Text);
                    MessageBox.Show("Guest details have been successfully updated!");
                }
                else
                {
                    MessageBox.Show("Please enter a valid age for guest!");
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        
        }

        // Open new window that allows user to add a new extra to booking
        private void btn_addExtra_Click(object sender, RoutedEventArgs e)
        {
            addExtra extraWin = new addExtra();
            extraWin.cmb_extraTypes.Items.Add("Breakfast");
            extraWin.cmb_extraTypes.Items.Add("Evening meal");
            extraWin.cmb_extraTypes.Items.Add("Car hire");
            extraWin.Owner = this;
            extraWin.ShowDialog();
        }


        // Saves all changes made to booking (to the csv file)
        private void btn_saveChanges_Click(object sender, RoutedEventArgs e)
        {
            DateTime arrivalDate;
            DateTime departureDate;
            if (DateTime.TryParse(txt_bookingArrivalDate.Text, out arrivalDate))
            {
                if (DateTime.TryParse(txt_bookingDepartureDate.Text, out departureDate))
                {
                    try
                    {
                        Booking aBooking = facade.CurrentBooking;
                        aBooking.ArrivalDate = arrivalDate;
                        aBooking.DepartureDate = departureDate;
                        facade.SaveToCSV();
                        MessageBox.Show("Booking details were successfully saved!");
                    }
                    catch (Exception excep)
                    {
                        MessageBox.Show(excep.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid Departure Date (YYYY-MM-DD)");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Arrival Date (YYYY-MM-DD)");
            }
        }


        // Loads the extra selected extra details to Mainwindow
        private void cmb_bookingExtras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Extra selectedExtra = null;
            if (cmb_bookingExtras.SelectedItem != null)
            {
                selectedExtra = facade.GetExtra(cmb_bookingExtras.SelectedIndex);
            }

            if (selectedExtra != null)
            {
                if (cmb_bookingExtras.SelectedItem.ToString() == "Breakfast" || cmb_bookingExtras.SelectedItem.ToString() == "Evening meal")
                {
                    lbl_extraDietaryRequirements.Visibility = System.Windows.Visibility.Visible;
                    txt_extraDietaryRequirements.Visibility = System.Windows.Visibility.Visible;
                    lbl_extraDriverName.Visibility = System.Windows.Visibility.Hidden;
                    txt_extraDriverName.Visibility = System.Windows.Visibility.Hidden;
                    lbl_extraStartDate.Visibility = System.Windows.Visibility.Hidden;
                    txt_extraStartDate.Visibility = System.Windows.Visibility.Hidden;
                    lbl_extraEndDate.Visibility = System.Windows.Visibility.Hidden;
                    txt_extraEndDate.Visibility = System.Windows.Visibility.Hidden;
                    txt_extraDietaryRequirements.Text = txt_bookingDietaryRequirements.Text;
                }
                else if (cmb_bookingExtras.SelectedItem.ToString() == "Car hire")
                {
                    CarHire aCarHire = (CarHire)selectedExtra;
                    lbl_extraDietaryRequirements.Visibility = System.Windows.Visibility.Hidden;
                    txt_extraDietaryRequirements.Visibility = System.Windows.Visibility.Hidden;
                    lbl_extraDriverName.Visibility = System.Windows.Visibility.Visible;
                    txt_extraDriverName.Visibility = System.Windows.Visibility.Visible;
                    lbl_extraStartDate.Visibility = System.Windows.Visibility.Visible;
                    txt_extraStartDate.Visibility = System.Windows.Visibility.Visible;
                    lbl_extraEndDate.Visibility = System.Windows.Visibility.Visible;
                    txt_extraEndDate.Visibility = System.Windows.Visibility.Visible;
                    txt_extraDriverName.Text = aCarHire.DriverName;
                    txt_extraStartDate.Text = aCarHire.StartDate.ToString("yyyy-MM-dd");
                    txt_extraEndDate.Text = aCarHire.ReturnDate.ToString("yyyy-MM-dd");
                }
                btn_deleteExtra.IsEnabled = true;
                btn_amendExtra.IsEnabled = true;
            }
            else
            {
                btn_deleteExtra.IsEnabled = false;
                btn_amendExtra.IsEnabled = false;
            }
        }


        //Deletes the selected extra
        private void btn_deleteExtra_Click(object sender, RoutedEventArgs e)
        {
            facade.RemoveExtra(cmb_bookingExtras.SelectedIndex);
            MessageBox.Show("Extra sucessfully deleted!");
            cmb_bookingExtras.Items.RemoveAt(cmb_bookingExtras.SelectedIndex);

            lbl_extraDietaryRequirements.Visibility = System.Windows.Visibility.Hidden;
            txt_extraDietaryRequirements.Visibility = System.Windows.Visibility.Hidden;
            lbl_extraDriverName.Visibility = System.Windows.Visibility.Hidden;
            txt_extraDriverName.Visibility = System.Windows.Visibility.Hidden;
            lbl_extraStartDate.Visibility = System.Windows.Visibility.Hidden;
            txt_extraStartDate.Visibility = System.Windows.Visibility.Hidden;
            lbl_extraEndDate.Visibility = System.Windows.Visibility.Hidden;
            txt_extraEndDate.Visibility = System.Windows.Visibility.Hidden;

            txt_extraDietaryRequirements.Text = "";
            txt_extraDriverName.Text = "";
            txt_extraStartDate.Text = "";
            txt_extraEndDate.Text = "";


            if (cmb_bookingExtras.Items.Count == 0)
            {
                btn_deleteExtra.IsEnabled = false;
                btn_amendExtra.IsEnabled = false;
            }
        }

        // Amends the selected extra
        private void btn_amendExtra_Click(object sender, RoutedEventArgs e)
        {

            Booking aBooking = facade.CurrentBooking;
            Extra anExtra = facade.GetExtra(cmb_bookingExtras.SelectedIndex);
            if (anExtra.GetType().ToString() == "HollydayBooking.Breakfast") //Check if extra is of type Brekfast
            {
                Breakfast aBreakfast = (Breakfast) anExtra;
                aBooking.DietaryRequirements = txt_extraDietaryRequirements.Text;
                txt_bookingDietaryRequirements.Text = txt_extraDietaryRequirements.Text;
                MessageBox.Show("Extra details have been successfully updated!");
            }
            else if (anExtra.GetType().ToString() == "HollydayBooking.EveningMeal") //Check if extra is of type EveningMeal
            {
                EveningMeal anEveningMeal = (EveningMeal) anExtra;
                aBooking.DietaryRequirements = txt_extraDietaryRequirements.Text;
                txt_bookingDietaryRequirements.Text = txt_extraDietaryRequirements.Text;
                MessageBox.Show("Extra details have been successfully updated!");
            }
            else if (anExtra.GetType().ToString() == "HollydayBooking.CarHire") //Check if extra is of type CarHire
            {
                DateTime arrivalDate;
                DateTime departureDate;
                if (DateTime.TryParse(txt_extraStartDate.Text, out arrivalDate))
                {
                    if (DateTime.TryParse(txt_extraEndDate.Text, out departureDate))
                    {
                        try
                        {
                            CarHire aCarHire = (CarHire)anExtra;
                            aCarHire.DriverName = txt_extraDriverName.Text; 
                            aCarHire.StartDate = arrivalDate;
                            aCarHire.ReturnDate = departureDate;
                            MessageBox.Show("Extra details have been successfully updated!");
                        }
                        catch (Exception excep)
                        {
                            MessageBox.Show(excep.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid return date. (YYYY-MM-DD)");
                        txt_extraEndDate.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid pick-up date. (YYYY-MM-DD)");
                    txt_extraStartDate.Text = "";
                }
            }
        }


        // Enable the window properties for the Costumer menu section
        public void EnableCostumerMenuOptions()
        {
            lbl_costumerName.Visibility = System.Windows.Visibility.Visible;
            lbl_costumerAddress.Visibility = System.Windows.Visibility.Visible;
            lbl_costumerNameOutput.Visibility = System.Windows.Visibility.Visible;
            lbl_costumerAddressOutput.Visibility = System.Windows.Visibility.Visible;

            btn_removeCostumer.IsEnabled = true;
            btn_amendCostumer.IsEnabled = true;
            btn_addBooking.IsEnabled = true;
        }

        // Disable the window properties for the Costumer menu section
        public void DisableCostumerMenuOptions()
        {
            btn_removeCostumer.IsEnabled = false;
            btn_amendCostumer.IsEnabled = false;
            btn_addBooking.IsEnabled = false;
            lbl_costumerNameOutput.Content = "";
            lbl_costumerAddressOutput.Content = "";
            lbl_costumerName.Visibility = System.Windows.Visibility.Hidden;
            lbl_costumerAddress.Visibility = System.Windows.Visibility.Hidden;
            lbl_costumerNameOutput.Visibility = System.Windows.Visibility.Hidden;
            lbl_costumerAddressOutput.Visibility = System.Windows.Visibility.Hidden;
            ClearBookingMenu();
        }

        // Clears/Disable window properties for the for the Booking menu section
        private void ClearBookingMenu()
        {
            btn_removeBooking.IsEnabled = false;
            txt_bookingReferenceNumber.Text = "";
            txt_bookingArrivalDate.Text = "";
            txt_bookingDepartureDate.Text = "";
            txt_bookingDietaryRequirements.Text = "";
            cmb_bookingGuests.Items.Clear();
            cmb_bookingGuests.IsEnabled = false;
            btn_addGuest.IsEnabled = false;
            btn_amendGuest.IsEnabled = false;
            btn_deleteGuest.IsEnabled = false;
            txt_guestName.Text = "";
            txt_guestPassportNumber.Text = "";
            txt_guestAge.Text = "";
            cmb_bookingExtras.Items.Clear();
            cmb_bookingExtras.IsEnabled = false;
            btn_addExtra.IsEnabled = false;
            btn_amendExtra.IsEnabled = false;
            btn_deleteExtra.IsEnabled = false;
            btn_saveChanges.IsEnabled = false;
            lbl_extraDietaryRequirements.Visibility = System.Windows.Visibility.Hidden;
            txt_extraDietaryRequirements.Visibility = System.Windows.Visibility.Hidden;
            lbl_extraDriverName.Visibility = System.Windows.Visibility.Hidden;
            txt_extraDriverName.Visibility = System.Windows.Visibility.Hidden;
            lbl_extraStartDate.Visibility = System.Windows.Visibility.Hidden;
            txt_extraStartDate.Visibility = System.Windows.Visibility.Hidden;
            lbl_extraEndDate.Visibility = System.Windows.Visibility.Hidden;
            txt_extraEndDate.Visibility = System.Windows.Visibility.Hidden;
        }

        // Opens a new window and prints a detailed invoice for the selected booking
        private void btn_invoice_Click(object sender, RoutedEventArgs e)
        {
            Invoice inv = new Invoice();
            Costumer aCostumer = facade.CurrentCostumer;
            Booking aBooking = facade.CurrentBooking;
            inv.lbl_invoiceDateOutput.Content = DateTime.Now.ToString("yyyy-MM-dd");
            inv.lbl_invoiceBookingIDOutput.Content = aBooking.ReferenceNumber;
            inv.lbl_invoiceNumberOfGuestsOutput.Content = aBooking.Guests.Count();
            inv.lbl_customerIDOutput.Content = aCostumer.ReferenceNumber;
            inv.lbl_customerNameOutput.Content = aCostumer.Name;
            inv.lbl_customerAddressOutput.Content = aCostumer.Address;
            foreach (Guest aGuest in aBooking.Guests)
            {
                if (aGuest.Age <= 18)
                {
                    inv.lbl_bookingDescription.Content += "Guest" + (aBooking.Guests.IndexOf(aGuest) + 1) + ": £30.00 (child rate) x " + (aBooking.DepartureDate - aBooking.ArrivalDate).TotalDays + " nights\n";
                    inv.lbl_bookingAmount.Content += 30.00*((aBooking.DepartureDate - aBooking.ArrivalDate).TotalDays) + "£\n";
                }
                else
                {
                    inv.lbl_bookingDescription.Content += "Guest" + (aBooking.Guests.IndexOf(aGuest) + 1) + ": £50.00 (adult rate) x " + (aBooking.DepartureDate - aBooking.ArrivalDate).TotalDays + " nights\n";
                    inv.lbl_bookingAmount.Content += 50.00 * ((aBooking.DepartureDate - aBooking.ArrivalDate).TotalDays) + "£\n";
                }
            }
            foreach (Extra anExtra in aBooking.Extras)
            {
                if (anExtra.GetType().ToString() == "HollydayBooking.Breakfast")
                {
                    inv.lbl_bookingDescription.Content += "Breakfast: " + "£5.00 x " + aBooking.Guests.Count()+ " guests x " + (aBooking.DepartureDate - aBooking.ArrivalDate).TotalDays + " nights\n";
                    inv.lbl_bookingAmount.Content += anExtra.Price * aBooking.Guests.Count() * ((aBooking.DepartureDate - aBooking.ArrivalDate).TotalDays) + "£\n";
                }
                else if (anExtra.GetType().ToString() == "HollydayBooking.EveningMeal")
                {
                    inv.lbl_bookingDescription.Content += "Evening Meal: " + "£15.00 x " + aBooking.Guests.Count() + " guests x " + (aBooking.DepartureDate - aBooking.ArrivalDate).TotalDays + " nights\n";
                    inv.lbl_bookingAmount.Content += anExtra.Price * aBooking.Guests.Count() * ((aBooking.DepartureDate - aBooking.ArrivalDate).TotalDays) + "£\n";
                }
                else if (anExtra.GetType().ToString() == "HollydayBooking.CarHire")
                {
                    CarHire aCarHire = (CarHire)anExtra;
                    inv.lbl_bookingDescription.Content += "Car Hire: " + "£50.00 x " + (aCarHire.ReturnDate - aCarHire.StartDate).TotalDays + " days\n";
                    inv.lbl_bookingAmount.Content += aCarHire.Price * ((aCarHire.ReturnDate - aCarHire.StartDate).TotalDays) + "£\n";
                }
            }
            inv.lbl_invoiceTotalAmountOutput.Content = aBooking.GetCost() + "£";
            inv.ShowDialog();
        }
    }
}
