//Author: Pedro Mendes     MatricNum:40218056
//Description: Class used to represent a Booking extra. Different types of extras inherit from this class
//Date last modified: 2016-12-07
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollydayBooking
{
    public abstract class Extra //Abstract class extra. No instance of this class will ever be created. Represents a purely conceptual object that contains nothing else than a price
    {
        private double price;

        public abstract double Price { get; }
    }
}
