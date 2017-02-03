using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HollydayBooking;

namespace BookingClassTest
{
    [TestClass]
    public class BankClassTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        //Method will pass if Argument exception is thrown (When )
        public void DepartureDate_Is_Before_Arrival_Date_Test() 
        {
            DateTime arrivalDate = DateTime.Parse("2016-08-08");
            DateTime departureDate = DateTime.Parse("2016-08-06");

            Booking aBooking = new Booking();
            aBooking.ArrivalDate = arrivalDate;
            aBooking.DepartureDate = departureDate; //Should throw exception
        }

        [TestMethod]
        //Method will pass if departure date can be successfuly set
        public void DepartureDate_Is_After_Arrival_Date_Test()
        {
            DateTime arrivalDate = DateTime.Parse("2016-08-08");
            DateTime departureDate = DateTime.Parse("2016-08-11");

            Booking aBooking = new Booking();
            aBooking.ArrivalDate = arrivalDate;
            aBooking.DepartureDate = departureDate;
            //Check if the departure date for booking was successfuly set
            Assert.AreEqual(aBooking.DepartureDate, departureDate, "Booking departure date successfully set!");
        }

        [TestMethod]
        //Method will pass if Guest is added to Booking guest list successfuly
        public void Add_Guests_Working_Correctly()
        {
            Booking aBooking = new Booking();
            Guest aGuest = new Guest();
            aBooking.AddGuest(aGuest);
            //The number of guests in the guest list must be 1 at this point
            Assert.AreEqual(aBooking.Guests.Count, 1, "Guest sucessfuly added to booking!");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        //Method throw exception when adding a new guest to alist with 4 guests (maximum number of guests = 4)
        public void Add_Guest_Fail_When_4_Guests_Already_In_The_List()
        {
            Booking aBooking = new Booking();
            //Create 5 guests
            Guest Guest1 = new Guest();
            Guest Guest2 = new Guest();
            Guest Guest3 = new Guest();
            Guest Guest4 = new Guest();
            Guest Guest5 = new Guest();

            //Add guests to booking list
            aBooking.AddGuest(Guest1);
            aBooking.AddGuest(Guest2);
            aBooking.AddGuest(Guest3);
            aBooking.AddGuest(Guest4);
            aBooking.AddGuest(Guest5); //This line should throw an Argument exception
        }

        [TestMethod]
        // Returns a null guest if guest cannot be found in the list
        public void GetGuest_Method_Returns_Null_If_Guest_Does_Not_Exist()
        {
            Booking aBooking = new Booking();
            //Create Guests
            Guest Guest1 = new Guest();
            Guest Guest2 = new Guest();
            //Add guests to booking list
            aBooking.AddGuest(Guest1);
            aBooking.AddGuest(Guest2);

            int index = 2;
            Guest Guest3 = aBooking.GetGuest(index); //Should return a null Guest 
            Assert.AreEqual(Guest3, null, "Guest can not be found! (null)");
        }

        [TestMethod]
        // Returns the right guest if guest exist
        public void GetGuest_Method_Returns_The_Right_Guest_If_It_Does_Exist_In_The_List()
        {
            Booking aBooking = new Booking();
            //Create Guests
            Guest Guest1 = new Guest();
            Guest1.Name = "zero";
            aBooking.AddGuest(Guest1);

            Guest Guest2 = new Guest();
            Guest2.Name = "one";
            aBooking.AddGuest(Guest2);

            Guest Guest3 = new Guest();
            Guest3.Name = "two";
            aBooking.AddGuest(Guest3);


            int index = 0;
            Guest GuestOne = aBooking.GetGuest(index); //Should return the guest at index 0 (Guest1)
            Assert.AreEqual(Guest1.Name, GuestOne.Name, "Method returns the right guest");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        // Throws exception if guest with the provided index does not exist
        public void RemoveGuest_Method_Throws_Exception_If_Guest_Does_Not_Exist()
        {
            Booking aBooking = new Booking();
            //Create Guest
            Guest Guest1 = new Guest();
            //Add guest to booking list
            aBooking.AddGuest(Guest1);

            int index = 2;
            aBooking.RemoveGuest(index); // Should throw exception since guest at index 2 does not exist
        }


        [TestMethod]
        // Throws exception if guest with the provided index does not exist
        public void RemoveGuest_Method_Removes_Guest_If_Guest_Does_Exist()
        {
            Booking aBooking = new Booking();
            //Create Guest
            Guest Guest1 = new Guest();
            Guest Guest2 = new Guest();
            //Add guest to booking list
            aBooking.AddGuest(Guest1);
            aBooking.AddGuest(Guest2);

            int index = 1;
            aBooking.RemoveGuest(index); // Should remove the Guest at index 1
            Assert.AreEqual(aBooking.Guests.Count, 1, "Guest was removed successfuly");
        }

        [TestMethod]
        // Should add an extra successfuly given an extra object
        public void AddExtra_Method_Works_Correctly()
        {
            Booking aBooking = new Booking();
            //Create Guest
            Extra Extra1 = new Breakfast();
            
            aBooking.AddExtra(Extra1); //Should add the extra to the list of extras correctly
            Assert.AreEqual(aBooking.Extras.Count, 1, "Guest was removed successfuly");
        }


        [TestMethod]
        // Should return the right extra given its index
        public void GetExtra_Method_Returns_The_Right_Extra_If_It_Does_Exist_In_The_List()
        {
            Booking aBooking = new Booking();
            //Create Extras
            Extra Extra1 = new Breakfast();
            Extra Extra2 = new CarHire();
            Extra Extra3 = new Breakfast();

            aBooking.AddExtra(Extra1);
            aBooking.AddExtra(Extra2);
            aBooking.AddExtra(Extra3);

            CarHire carhire1 = (CarHire)Extra2;
            int index = 1;
            CarHire carhire2 = (CarHire)aBooking.GetExtra(index); //Should return Extra2

            Assert.AreEqual(carhire1.DriverName, carhire2.DriverName, "Method returns the right extra");
        }

        [TestMethod]
        // Returns a null extra if extra cannot be found in the list
        public void GetExtra_Method_Returns_Null_If_Extra_Does_Not_Exist()
        {
            Booking aBooking = new Booking();
            //Create Extras
            Extra Extra1 = new Breakfast();
            Extra Extra2 = new CarHire();
            //Add Extras to booking list
            aBooking.AddExtra(Extra1);
            aBooking.AddExtra(Extra2);

            int index = 2;
            Extra extra3 = aBooking.GetExtra(index); //Should return a null Extra
            Assert.AreEqual(extra3, null, "Guest can not be found! (null)");
        }

        [TestMethod]
        public void RemoveExtra_Method_Removes_Guest_If_Guest_Does_Exist()
        {
            Booking aBooking = new Booking();
            //Create Extras
            Extra Extra1 = new Breakfast();
            Extra Extra2 = new CarHire();
            //Add Extras to booking list
            aBooking.AddExtra(Extra1);
            aBooking.AddExtra(Extra2);

            int index = 1;
            aBooking.RemoveExtra(index); // Should remove the Extra at index 1
            Assert.AreEqual(aBooking.Extras.Count, 1, "Extra was removed successfuly");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        // Throws exception if extra with the provided index does not exist
        public void RemoveExtra_Method_Throws_Exception_If_Extra_Does_Not_Exist()
        {
            Booking aBooking = new Booking();
            //Create extra
            Extra extra1 = new Breakfast();
            //Add extra to booking list
            aBooking.AddExtra(extra1);

            int index = 2;
            aBooking.RemoveExtra(index); // Should throw exception since extra at index 2 does not exist
        }

        [TestMethod]
        public void GetCost_Method_Calculates_Cost_Of_Bookings_Correctly()
        {
            Booking aBooking = new Booking();
            // Booking for 2 nights
            aBooking.ArrivalDate = DateTime.Parse("2000-01-01");
            aBooking.DepartureDate = DateTime.Parse("2000-01-03");

            //Create 2 guests 
            Guest guest1 = new Guest();
            guest1.Age = 12; //Create a guest (child charged at £30.00/night)
            aBooking.AddGuest(guest1);
            Guest guest2 = new Guest();
            guest2.Age = 32; //Create a guest (adult charged at £50.00/night)
            aBooking.AddGuest(guest2);

            //Create Extras
            Breakfast breakfast1 = new Breakfast(); //Breakfast charged at £5.00 per guest per night
            CarHire carhire1 = new CarHire(); //CarHire charged at £50.00/day
            //Car hire booked for 1 day
            carhire1.StartDate = DateTime.Parse("2000-01-01");
            carhire1.ReturnDate = DateTime.Parse("2000-01-02");
            aBooking.AddExtra(breakfast1);
            aBooking.AddExtra(carhire1);


            double expected = 230.00; //expected cost for booking 
            double calculated = aBooking.GetCost(); //Cost returned from the GetCost method

            Assert.AreEqual(expected, calculated, "GetCost method calculated booking costs correctly!");
        }
    }
}
