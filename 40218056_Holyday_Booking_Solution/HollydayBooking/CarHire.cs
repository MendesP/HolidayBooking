//Author: Pedro Mendes     MatricNum:40218056
//Description: Class used to represent a CarHire. Inherits from extra. 
//Date last modified: 2016-11-18
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollydayBooking
{
    public class CarHire : Extra //inherit from extra
    {
        private string driverName; //holds the name of the driver
        private DateTime startDate; //holds the hiring startDate
        private DateTime returnDate; //holds the hiring endtDate

        //Get and set methods for the DriverName property. Driver Name can not be left blank, throw exception if an empty string is assigned to the driver name
        public string DriverName
        {
            get
            {
                return driverName;
            }
            set
            {
                if (value == "")
                {
                    throw new ArgumentException("Error: Please insert a valid Driver name!");
                }
                driverName = value;
            }
        }

        //Get and set methods for the StartDate property.
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
            }
        }

        //Get and set methods for the ReturnDate property. Except is thrown when ReturnDate is a Date before the StartDate (it would not make sence).
        public DateTime ReturnDate
        {
            get
            {
                return returnDate;
            }
            set
            {
                if (value < startDate)
                {
                    throw new ArgumentException("Error: Return date cannot be before pick-up date!");
                }
                returnDate = value;
            }
        }

        //Returns the rate of a carhire/day
        public override double Price
        {
            get
            {
                return 50.00;
            }
        }
    }
}
