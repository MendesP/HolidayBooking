//Author: Pedro Mendes     MatricNum:40218056
//Description: Classed used to load and save records to/from the CSV file 
//Date last modified: 2016-12-03
//Class uses the Singleton Design Pattern and the facade pattern (Works as a facade containing all the methods to access the CSV file, hiding the complexity of this operation)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace HollydayBooking
{
    public class CSVHandler
    {
        
        private static CSVHandler instance; //Creates static instance of the class (typical of the cingleton disign pattern)
        private string csvCostumerPath = @"H:\Software development 2\CourseWork2\HollydayBooking\costumer.csv"; //holds the path to the costumer.csv file
        private string csvBookingPath = @"H:\Software development 2\CourseWork2\HollydayBooking\booking.csv"; //holds the path to the booking.csv file
        private string csvGuestsPath = @"H:\Software development 2\CourseWork2\HollydayBooking\guests.csv"; //holds the path to the guests.csv file
        private string csvExtrasPath = @"H:\Software development 2\CourseWork2\HollydayBooking\extras.csv"; //holds the path to the extras.csv file
  

        // Method always returns the same instance of the class (no more than one instance can be created)
        public static CSVHandler Instance()
        {
            //Create new instance if one does not exist, otherwise return the existing instance
            if (instance == null)
            {
                instance = new CSVHandler();

            }
            return instance;
        }


        protected CSVHandler()
        {
            
        }

        // Method returns the last (max) reference number for a customer from the costumer.csv file
        public int GetCurrentCostumerRefNumber()
        {
            int ccrf = 0; //local int variable (used as a paceholder for the costumer reference number)
            // Try to read from costumer.csv file (will fail if file does not exist)
            try
            { 
                // Create a streamReader object to access the file
                StreamReader reader = new StreamReader(File.OpenRead(csvCostumerPath));
                //Read file line by line until endOfFile
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine(); //reads the entire line
                    var values = line.Split(','); // splits the line on the commas in creates a list with the values
                    if (Int32.Parse(values[0]) >= ccrf) //Check if the reference number (values[0]) is greater than the previously stored reference number
                    {
                        ccrf = Int32.Parse(values[0]); //Assign new reference number if its greater then the previously stored value
                    }
                }
                reader.Dispose(); //Dispose the reader 
                return ccrf; //Return (the greatest) reference number found in the file
            }
            // If reading from file fails, return 0;
            catch
            {
                return ccrf;
            }
        }


        // Same as the previous method but for booking
        public int GetCurrentBookingRefNumber()
        {
            int cbrf = 0;
            try
            {
                StreamReader reader = new StreamReader(File.OpenRead(csvBookingPath));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (Int32.Parse(values[1]) >= cbrf)
                    {
                        cbrf = Int32.Parse(values[1]);
                    }
                }
                reader.Dispose();
                return cbrf;
            }
            catch
            {
                return cbrf;
            }
        }

        //Method Loads all the records from the CSV files into appropriate objects
        public void LoadAllRecords()
        {
            ListOfCostumers costumerList = ListOfCostumers.Instance(); //Get an instance of the ListOfCustomer (Singleton) class
            if (File.Exists(csvCostumerPath)) //check if customer.csv file exists
            {
                GetCostumers(); //cal the GetCostumers() method
            }
            if (File.Exists(csvBookingPath)) //check if booking.csv file exists
            {
                foreach (Costumer aCostumer in costumerList.CostumerList) //Iterates through all the costumers in the list
                {
                    GetBookings(aCostumer); //Call the GetBooking method (Populates the booking list (of each customer) with all its bookings)
                }
                foreach (Costumer aCostumer in costumerList.CostumerList) //Iterates through all the costumers in the list
                {
                    foreach (Booking aBooking in aCostumer.CostumerBookings) //Iterates through all the bookings for a specific customer
                    {
                        if (File.Exists(csvGuestsPath)) //check if guests.csv file exists
                        {
                            GetGuests(aBooking); //Call the GetGuests method
                        }
                        if (File.Exists(csvExtrasPath)) //check if extras.csv file exists
                        {
                            GetExtras(aBooking); //Call the GetExtras method
                        }
                    }
                }
            }
        }

        // Loads all the customers from the CSV into a list of customers (ListOfCostumer Singleton Class)
        public void GetCostumers()
        {
            // Create a streamReader object to access the file
            StreamReader reader = new StreamReader(File.OpenRead(csvCostumerPath)); 
            ListOfCostumers costumerList = ListOfCostumers.Instance();//Get an instance of the ListOfCustomer (Singleton) class
            // Create a streamReader object to access the file
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                Costumer aCostumer = new Costumer(Int32.Parse(values[0]), values[1], values[2]); //Create a new customer and sets its properties to the values on the csv
                costumerList.AddCostumer(aCostumer); //adds the customer to the custumer list (in the ListOfCustumers in the singleton class)
            }
            reader.Dispose();
        }

        // Reads from booking.csv file to populate the booking list (of each customer) with all its bookings 
        public void GetBookings(Costumer aCostumer)
        {
            StreamReader reader = new StreamReader(File.OpenRead(csvBookingPath));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine(); 
                var values = line.Split(','); 
                if (Int32.Parse(values[0]) == aCostumer.ReferenceNumber) //if the booking belongs to the custumer (aCostumer)
                { 
                    Booking aBooking = new Booking(Int32.Parse(values[1]), DateTime.Parse(values[2]), DateTime.Parse(values[3]), values[4]); //Create a new booking object an set its values from the CSV record
                    aCostumer.AddBooking(aBooking); //Add new booking to the Custumer listofbookings
                }
            }
            reader.Dispose();
        }

        // Reads from guests.csv file to populate the a booking list of guests  (creates Guest object and adds it to the list)
        public void GetGuests(Booking aBooking)
        {
            StreamReader reader = new StreamReader(File.OpenRead(csvGuestsPath));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                if (Int32.Parse(values[0]) == aBooking.ReferenceNumber)
                {
                    Guest aGuest = new Guest();
                    aGuest.Name = values[1];
                    aGuest.PassportNumber = values[2];
                    aGuest.Age = Int32.Parse(values[3]);
                    aBooking.Guests.Add(aGuest);
                }
            }
            reader.Dispose();
        }

        // Reads from extras.csv file to populate the a booking list of extras  (creates extra objects (Breakfasts, MealHire, CarHire) and adds it to the list)
        public void GetExtras(Booking aBooking)
        {
            StreamReader reader = new StreamReader(File.OpenRead(csvExtrasPath));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                if (Int32.Parse(values[0]) == aBooking.ReferenceNumber)
                {
                    if (values[1] == "HollydayBooking.Breakfast") //Check if extra is a breakfast
                    {
                        Breakfast aBreakfast = new Breakfast();
                        aBooking.Extras.Add(aBreakfast);
                    }
                    else if (values[1] == "HollydayBooking.EveningMeal") //Check if extra is an EveningMeal
                    {
                        EveningMeal anEveningMeal = new EveningMeal();
                        aBooking.Extras.Add(anEveningMeal);
                    }
                    else if (values[1] == "HollydayBooking.CarHire") //Check if extra is CarHire
                    {
                        CarHire aCarHire = new CarHire();
                        aCarHire.DriverName = values[2];
                        aCarHire.StartDate = DateTime.Parse(values[3]);
                        aCarHire.ReturnDate = DateTime.Parse(values[4]);
                        aBooking.Extras.Add(aCarHire);
                    }
                }
            }
            reader.Dispose();
        }

        //Method Saves all the objects (customers, booking, guests and extras) in the system to the csv files
        public void SaveAllRecords()
        {   
            //Delete all the csv files (to write back from scratch)
            File.Delete(csvCostumerPath);
            File.Delete(csvBookingPath);
            File.Delete(csvGuestsPath);
            File.Delete(csvExtrasPath);
            ListOfCostumers costumerList = ListOfCostumers.Instance();
            foreach (Costumer aCostumer in costumerList.CostumerList)
            {
                WriteCostumer(aCostumer);
                foreach (Booking aBooking in aCostumer.CostumerBookings)
                {
                    WriteBooking(aCostumer.ReferenceNumber, aBooking);
                }
            }
        }

        //Method writes (appends) a costumer to the costumer.csv file 
        public void WriteCostumer (Costumer aCostumer)
        {
            string line = aCostumer.ReferenceNumber.ToString() + "," 
                   + aCostumer.Name.ToString() + "," 
                   + aCostumer.Address.ToString();
            string filepath = csvCostumerPath;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(line);
            System.IO.File.AppendAllText(filepath, sb.ToString());
        }

        public void WriteBooking(int costumerRef, Booking aBooking)
        {
            StringBuilder sb = new StringBuilder();

            // Write booking details to booking.csv    
            string line = costumerRef.ToString() + ","
                          + aBooking.ReferenceNumber.ToString() + ","
                          + aBooking.ArrivalDate.ToString(aBooking.DatePattern) + ","
                          + aBooking.DepartureDate.ToString(aBooking.DatePattern) + ","
                          + aBooking.DietaryRequirements.ToString();
            string filepath = csvBookingPath;
            sb.AppendLine(line);
            System.IO.File.AppendAllText(filepath, sb.ToString());
            sb.Clear(); //Clears the buffer's current content

            //Write Guest details to guests.csv
            filepath = csvGuestsPath; //write guest details to guests.csv
            foreach (Guest guest in aBooking.Guests)
            {
                 line = aBooking.ReferenceNumber.ToString() + ","
                        + guest.Name.ToString() + ","
                        + guest.PassportNumber.ToString() + ","
                        + guest.Age.ToString();
                sb.AppendLine(line);
            }
            System.IO.File.AppendAllText(filepath, sb.ToString());
            sb.Clear();

            //Write Extra details to extras.csv
            filepath = csvExtrasPath; //write extra details to extra.csv
            foreach (Extra extra in aBooking.Extras)
            {
                if (extra.GetType().ToString() == "HollydayBooking.Breakfast" || extra.GetType().ToString() == "HollydayBooking.EveningMeal")
                {
                    line = aBooking.ReferenceNumber.ToString() + "," + extra.GetType().ToString();
                    sb.AppendLine(line);
                }
                else
                {
                    CarHire aCarHire = new CarHire();
                    aCarHire = (CarHire)extra;
                    line = aBooking.ReferenceNumber.ToString() + ","
                      + extra.GetType().ToString() + ","
                      + aCarHire.DriverName.ToString() + ","
                      + aCarHire.StartDate.ToString(aBooking.DatePattern) + ","
                      + aCarHire.ReturnDate.ToString(aBooking.DatePattern);
                    sb.AppendLine(line);
                }
            }
            System.IO.File.AppendAllText(filepath, sb.ToString());
        }
    }
}
