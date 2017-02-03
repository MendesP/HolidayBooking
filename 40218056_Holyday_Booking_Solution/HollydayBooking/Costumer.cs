//Author: Pedro Mendes     MatricNum:40218056
//Description: Class used to represent a Costumer. Constains Costumer properties and its set and get methods (with validation checking) and some other useful methods
//Date last modified: 2016-12-07
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollydayBooking
{
    // Class represents a Costumer
    public class Costumer 
    {
        private int referenceNumber;//holds the reference number of the custumer which uniquely identifies it
        private string name; //Holds the custumer name
        private string address;//holds the customer address
        private List<Booking> costumerBookings; //hold a list of references to all the bookings of the customer

        //Constructer used to create new Customers 
        public Costumer()
        {
            CostumerRefGenerator costRefGen = CostumerRefGenerator.Instance();
            referenceNumber = costRefGen.GetReference(); //use the CostumerRefGenerator class to generate the right reference number for the booking
            costumerBookings = new List<Booking>(); //Creates an empty list of bookings
        }

        //Constructor used to create pre-existing custumers (details of custumers loaded from CSV when the application starts (including custumer reference numbers))
        public Costumer(int refNumber, string name, string address)
        {
            this.referenceNumber = refNumber;
            this.name = name;
            this.address = address;
            costumerBookings = new List<Booking>();
        }


        //ReferenceNumber can not be set after the object has been created.
        public int ReferenceNumber
        {
            get 
            {
                return referenceNumber;
            }
        }

        //Get set methods for the Name property. Throw exception is the empty string is assigned to name.
        public string Name
        {
            get 
            {
                return name;
            }
            set 
            {
                if (value == "")
                {
                    throw new ArgumentException("Customer name can not be left blank");
                }
                name = value;
            }
        }

        //Get set methods for the Address property. Throw exception is the empty string is assigned to Address.
        public string Address
        {
            get 
            {
                return address;
            }
            set
            {
                if (value == "")
                {
                    throw new ArgumentException("Customer address can not be left blank");
                }
                address = value;
            }
        }

        //Get set methods for the CostumerBookings property
        public List<Booking> CostumerBookings
        { 
            get
            {
                return costumerBookings;
            }
            set
            {
                costumerBookings = value;
            }
        }

        //Adds a new booking to costumer given a Booking object
        public void AddBooking(Booking booking)
        {
            costumerBookings.Add(booking);
        }

        // Returns a booking from this custumer's booking list given a Booking reference
        public Booking GetBooking(int refNumber)
        { 
            foreach (Booking aBooking in costumerBookings)
            {
                if (aBooking.ReferenceNumber == refNumber)
                {
                    return aBooking;
                }   
            }
            return null; //Returns null instead if custumer does not exist
        }

        // Removes a booking from this custumer's booking list given a Booking reference (if booking exist)
        public void RemoveBooking(int refNum)
        {
            foreach (Booking aBooking in this.CostumerBookings)
            {
                if (aBooking.ReferenceNumber == refNum)
                {
                    aBooking.Guests.Clear();
                    aBooking.Extras.Clear();
                    this.CostumerBookings.Remove(aBooking);
                    break;//Break out of loop when booking is deleted (no point in continuing iterating through the list)
                }
            }
        }

        // Removes a booking from the customer's list of bookings given a booking
        public void DeleteBooking(Booking aBooking)
        {
            aBooking.Guests.Clear(); //Clear the Booking list of guests
            aBooking.Extras.Clear(); //Clear the Booking list of extras
            this.CostumerBookings.Remove(aBooking); //Remove the booking from the costumer list of bookings
        }
    }
}
