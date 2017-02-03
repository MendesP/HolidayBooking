//Author: Pedro Mendes     MatricNum:40218056
//Description: Class used to store a unique list of costumer (containing a list with references to all the existing customers). 
//Date last modified: 2016-11-26
//Class uses the Singleton Design Pattern

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollydayBooking
{
    // Singleton Class ListofCostumers represent a list of all the costumers of the Hollyday booking System
    public class ListOfCostumers
    {
        private static ListOfCostumers instance; //create a static instance of the class
        private List<Costumer> costumerList = new List<Costumer>(); //create a list of customers

        // Returns always the same instance of the class. Creates a new instance if one has not yet been created
        public static ListOfCostumers Instance()
        {
            if (instance == null)
            { 
                instance = new ListOfCostumers();
            }
            return instance; 
        }

        //Property used to get the entire list of customers
        public List<Costumer> CostumerList
        {
            get
            {
                return costumerList;
            }
        }
        
        //Method adds a costumer object to the list of costumers
        public void AddCostumer(Costumer costumer)
        {
            this.costumerList.Add(costumer);
        }

        //Return the costumer given its reference number (return null if costumer with the given reference number does not exist)
        public Costumer FindCostumer(int costumerReference)
        { 
            foreach(Costumer costumer in costumerList)
            {
                if (costumer.ReferenceNumber == costumerReference)
                {
                    return costumer;
                }
            }
            return null;
        }

        // Remove a costumer from a list of costumer given the costumer object
        public void RemoveCostumer(Costumer costumer)
        {
            costumerList.Remove(costumer);
        }

        // Remove a costumer from a list of costumer given its reference number 
        public void RemoveCostumer(int refNumber)
        {
            foreach (Costumer aCostumer in this.costumerList)
            {
                if (aCostumer.ReferenceNumber == refNumber)
                {
                    costumerList.Remove(aCostumer);
                    break;
                }
            }  
        }
    }
}
