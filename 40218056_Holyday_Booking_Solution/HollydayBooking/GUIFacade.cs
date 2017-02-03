//Author: Pedro Mendes     MatricNum:40218056
//Description: Class used as a facade between the GUI classes and the business classes. It contains methods that provide all the functionality of the business classes
//Date last modified: 2016-12-03
//Class uses the Singleton Design Pattern and the facade pattern (Works as an abstraction layer between the GUI classes and the business classes)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollydayBooking
{
    public class GUIFacade
    {
        private static GUIFacade instance; //static instance of GUIFacade 
        private static ListOfCostumers costumerList; //instance of the ListOfCustomer class
        private static CSVHandler CSVFile; //instance of the CSVHandler class
        private Costumer currentCostumer; //holds the current custumer (the one that has been selected by the user)
        private Booking currentBooking; //holds the current booking (the one that has been selected by the user)

        // Property used to get the current custumer
        public Costumer CurrentCostumer
        {
            get
            {
                return currentCostumer;
            }
        }

        // Property used to get the current booking
        public Booking CurrentBooking
        {
            get
            {
                return currentBooking;
            }
        }

        // Always return the same instance of the class. Create a new instance if one has not already been created
        public static GUIFacade Instance
        {
            get
            {
                if (instance == null) 
                { 
                    instance = new GUIFacade();
                    costumerList = ListOfCostumers.Instance();
                    CSVFile = CSVHandler.Instance();
                }
                return instance;
            }
        }

        // Set the current custumer given its reference number (when selected by the user)
        public void SetCurrentCostumer(int refNumber)
        {
            currentCostumer = costumerList.FindCostumer(refNumber);
        }

        // Set the current booking given its reference number (when selected by the user)
        public void SetCurrentBooking(int refNumber)
        {
            currentBooking = currentCostumer.GetBooking(refNumber);
        }

        //Load everything from the csv files into objects
        public void LoadFromCSV()
        {
            CSVFile.LoadAllRecords();
        }

        //Saves all objects in memory to the csv files
        public void SaveToCSV()
        {
            CSVFile.SaveAllRecords();
        }

        //Return a costumer given its reference number
        public Costumer GetCostumer(int refNumber)
        {
            return costumerList.FindCostumer(refNumber);
        }

        //Adds a new custumer to the custumer list given a costumer object
        public void AddCostumer(Costumer aCostumer)
        {
            costumerList.AddCostumer(aCostumer);
        }


        //Removes a costumer from the costumer list given its reference number
        public void RemoveCostumer(int refNumber)
        {
            costumerList.RemoveCostumer(refNumber);
        }

        // Returns a booking belonging to the selected custumer by passing in its reference number
        public Booking GetBooking(int refNumber)
        {
            return currentCostumer.GetBooking(refNumber);
        }

        // Adds a booking to the selected custumer booking list given the booking object
        public void AddBooking(Booking aBooking)
        {
            currentCostumer.CostumerBookings.Add(aBooking);
        }

        // Deletes a booking from the selected customer list of bookings given the booking reference number
        public void RemoveBooking(int refNumber)
        {
            currentCostumer.RemoveBooking(refNumber);
        }

        // Returns a guest from the selected booking list of guests given its index
        public Guest GetGuest(int index)
        {
            return currentBooking.GetGuest(index); 
        }

        // Adds a new guest to the selected booking list of guests given the guest object
        public void AddGuest(Guest aGuest)
        {
            currentBooking.AddGuest(aGuest);
        }

        // Deletes a guest from he selected booking list of guests given its index
        public void RemoveGuest(int index)
        {
            currentBooking.RemoveGuest(index);
        }

        // Returns an extra from the selected booking list of extras given the extra index
        public Extra GetExtra(int index)
        {
            return currentBooking.GetExtra(index);
        }

        //Adds an extra to the selected booking list of extras given the extra object
        public void AddExtra(Extra anExtra)
        {
            currentBooking.AddExtra(anExtra);
        }

        //Removes an extra from the selected booking list of extras given its index
        public void RemoveExtra(int index)
        {
            currentBooking.RemoveExtra(index);
        }
    }
}
