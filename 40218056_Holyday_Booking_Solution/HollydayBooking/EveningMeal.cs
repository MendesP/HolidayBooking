//Author: Pedro Mendes     MatricNum:40218056
//Description: Class used to represent an Evening Meal. Inherits from extra. 
//Date last modified: 2016-11-18
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollydayBooking
{
    public class EveningMeal : Extra // Class inherits from Extra
    {
        // Returns the price of an Evening Meal (double)
        public override double Price
        {
            get
            {
                return 15.00;
            }
        }
    }
}
