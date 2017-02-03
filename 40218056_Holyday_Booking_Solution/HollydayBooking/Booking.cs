//Author: Pedro Mendes     MatricNum:40218056
//Description: Class used to represent a Booking. Constains booking properties (with validation checking) and some usefull methods
//Date last modified: 2016-12-07
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollydayBooking
{
    public class Booking
    {
        private int referenceNumber; //holds the booking reference number that uniquely identifies the booking
        private string datePattern; //holds the string pattern used to convert DateTime fields to string
        private DateTime arrivalDate; //holds the booking arrival date
        private DateTime departureDate; //holds the booking departuredate
        private string dietaryRequirements; //holds possible nutritional requirements associated with the booking
        private List<Guest> guests = new List<Guest>(); //Holds references to all the guests associated with the booking
        private List<Extra> extras = new List<Extra>(); //Holds references to all the extras associated with the booking

        //contructer used to create new bookings
        public Booking()
        {
            //Booking reference number automatically generated using the BookingRefGenerator class
            BookingRefGenerator bookingRefGen = BookingRefGenerator.Instance();
            referenceNumber = bookingRefGen.GetReference();
            datePattern = "yyyy-MM-dd"; //pattern for dates used in DateTime fields
        }

        // Contructer used to create (exesting) Bookings loaded from the CSVFile
        public Booking(int refNumber, DateTime arrivalDate, DateTime departureDate, string dietaryRequirements)
        {
            this.referenceNumber = refNumber;
            this.arrivalDate = arrivalDate;
            this.departureDate = departureDate;
            this.dietaryRequirements = dietaryRequirements;
            datePattern = "yyyy-MM-dd";
        }

        //ReferenceNumber property containing the get method. Does not contain a set method since the referenceNumber cannot be changed after the object creation
        public int ReferenceNumber
        {
            get
            {
                return referenceNumber;
            }
        }

        //DatePattern property the get method. Value is assigned when object is created. (no need for a set method)
        public string DatePattern
        {
            get
            {
                return datePattern;
            }
        }

        //get and set methods for the ArrivalDate property
        public DateTime ArrivalDate
        {
            get
            {
                return arrivalDate;
            }
            set
            {
                arrivalDate = value;
            }
        }

        //get and set methods for the DepartureDate property
        public DateTime DepartureDate
        {
            get
            {
                return departureDate;
            }
            set
            {
                //Departure date must be after the arrival date
                if (value < arrivalDate)
                {
                    throw new ArgumentException("Error: departure date cannot be before the arrival date");
                }
                departureDate = value;
            }
        }

        //get and set methods for the DietaryRequirements property
        public string DietaryRequirements
        {
            get
            {
                return dietaryRequirements;
            }
            set 
            {
                dietaryRequirements = value; 
            }
        }

        //get and set methods for the Guests property
        public List<Guest> Guests
        {
            get
            {
                return guests;
            }
            set
            {
                guests = value;
            }
        }

        //get and set methods for the Extras property
        public List<Extra> Extras
        {
            get
            {
                return extras;
            }
            set
            {
                extras = value;
            }
        }

        // Method to add a guest to the list of guests accepting a guest object as only argument
        public void AddGuest(Guest guest)
        {
            //Add guest to list if there is less than 4 guests in the list
            if (guests.Count < 4)
            {
                guests.Add(guest);
            }
            //throw exception if the list is full (4 guests already)
            else
            {
                throw new ArgumentException("Error: A booking can have a maximum of 4 guests!");
            }
        }

        //Method used to get a certain guest from the list of guests given its index
        public Guest GetGuest(int index)
        {
            // return the guest correspondent to the index provided
            if (index < this.Guests.Count() && index >= 0)
            {
                return this.Guests.ElementAt(index);
            }
            // return null if index provided does not exist 
            else
            {
                return null;
            }
        }

        //Method to remove a guest from the list of guests given the guest index 
        public void RemoveGuest(int index)
        {
            if (index > Guests.Count || index < 0)
            {
                throw new ArgumentException("Guest with index provided does not exist");
            }
            else
            {
                Guests.RemoveAt(index);
            }
        }

        //Adds an extra to the list of extras given an extra object
        public void AddExtra(Extra extra)
        {
            extras.Add(extra);
        }

        //Returns an extra object given its index
        public Extra GetExtra(int index)
        {
            // return the extra correspondent to the index provided
            if (index < this.Extras.Count())
            {
                return this.Extras.ElementAt(index);
            }
            //return null if index does not exist
            else
            {
                return null;
            }
        }

        //Removes an extra from the list given its index
        public void RemoveExtra(int index)
        {
            if (index < 0 || index > Extras.Count)
            {
                throw new ArgumentException("Extra with index provided does not exist");
            }
            else
            {
                Extras.RemoveAt(index);
            }
            
        }

        //Gets the total cost for a booking (adding the total stay charge + extras)
        public double GetCost()
        {
            double totalCost = 0;
            // Get the total amount (for the stay) for each of the guests guests
            foreach (Guest guest in guests)
            {
                //If guest is child
                if (guest.Age <= 18)
                {
                    // Then, cost for the guest is £30.00 (child rate) * number of nights
                    totalCost += (30.00 * (this.departureDate - this.arrivalDate).TotalDays); //add value to totalCost
                }
                else
                {
                    // otherwise, cost for the guest is £50.00 (adult rate) * number of nights
                    totalCost += (50.00 * (this.departureDate - this.arrivalDate).TotalDays); //add value to totalCost
                }
            }
            // Get the total amount (for each extra) for all extras
            foreach (Extra extra in extras)
            {
                // If extra is of type brekfast
                if (extra is Breakfast)
                {
                    //Add to totalCost the price of a Brekfast * the number of nights * number of Guests
                    totalCost += extra.Price * (this.DepartureDate - this.ArrivalDate).Days * guests.Count();
                }
                else if (extra is EveningMeal)
                {
                    //Add to totalCost the price of an Evening Meal * the number of nights * number of Guests
                    totalCost += extra.Price * (this.DepartureDate - this.ArrivalDate).Days * guests.Count();
                }
                else if (extra is CarHire)
                {
                    CarHire aCarHire = (CarHire)extra; // Cast from extra to CarHire object in order to access CarHire specific properties
                    //Add to totalCost the price of Hiring a car per night * the number of days Hired
                    totalCost += aCarHire.Price * (aCarHire.ReturnDate - aCarHire.StartDate).TotalDays;
                }
            }
            return totalCost; // Returns the totalCost of the Booking
        }
    }
}
