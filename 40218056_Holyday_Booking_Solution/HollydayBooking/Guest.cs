//Author: Pedro Mendes     MatricNum:40218056
//Description: Class used to represent a Guest. Constains Guest properties (with validation checking) and some usefull methods
//Date last modified: 2016-12-07

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollydayBooking
{
    public class Guest
    {
        private string name; //Holds the name guest name
        private string passportNumber; //holds the guest passport number
        private int age; //holds the guest age


        //Get/set methods for the Name property
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                //Name can not be set to the empty string (throws excepcion)
                if (value == "")
                {
                    throw new ArgumentException("Error: Please enter a valid Guest name");
                }
                name = value;
            }
        }

        //Get/set methods for the PassportNumber property
        public string PassportNumber
        { 
            get
            {
                return passportNumber;
            }
            set
            {
                //Passport number can not be set to the empty string and cannot be longer than 10 characters (throws excepcion)
                if (value.Length > 10 || value == "") // check string is 0-10 char long
                {
                    throw new ArgumentException("Error: Please enter a valid passport number (maximum 10 characters)!");
                }
                passportNumber = value;
            }
        }

        //Get/set methods for the Age property
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                //Age must be an integer between 0 and 101 (throws excepcion otherwise)
                if (value < 0 || value > 101)
                {
                    throw new ArgumentException("Error: Plase enter a valid age (between 0 and 101)!");
                }
                age = value;
            }
        }
    }
}
