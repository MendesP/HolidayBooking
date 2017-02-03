//Author: Pedro Mendes     MatricNum:40218056
//Description: Class used to autogenerate distinct (auto-incremented) reference numbers for new Customers
//Date last modified: 2016-11-26
//Class uses the Singleton Design Pattern

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollydayBooking
{
    public class CostumerRefGenerator
    {   
        
        private static CostumerRefGenerator instance; //Declaration of a private static instance of the class
        private int refNumber; //holds the current CustomerRefNumber

        //Method always return the same instance of the class
        public static CostumerRefGenerator Instance()
        {
            //Create new instance if it hasnt been created previously (if it is null)
            if (instance == null)
            {
                instance = new CostumerRefGenerator(); //Create new instance
                CSVHandler CSVFile = CSVHandler.Instance(); //Get a reference to the CSVFacade
                instance.refNumber = CSVFile.GetCurrentCostumerRefNumber(); //Get last reference number given to a customer from the records in the CSV and assign the value to the reference number of this class
            }
            return instance; //Return the instance of the class (always the same one)
        }

       
        public int GetReference()
        {
            refNumber++; //Increment reference number by one
            return refNumber; //Return the new reference number
        }
    }
}
