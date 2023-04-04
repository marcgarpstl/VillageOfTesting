using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageOfTesting_Marc_Garpstål
{
    public class Building
    {

        public string name;
        public int woodCost;
        public int metalCost;
        public int daysWorkedOn;
        public int daysToComplete;
        public bool complete;


        public Building(string name, int woodCost, int metalCost, int daysWorkedOn, int daysToComplete, bool complete)
        {
            this.name = name;
            this.woodCost = woodCost;
            this.metalCost = metalCost;
            this.daysWorkedOn = daysWorkedOn;
            this.daysToComplete = daysToComplete;
            this.complete = complete;
        }
    }
}


