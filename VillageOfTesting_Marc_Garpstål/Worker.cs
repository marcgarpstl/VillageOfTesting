using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageOfTesting_Marc_Garpstål
{
    public class Worker
    {

        private string name;
        private bool hungry;
        private int daysHungry;
        private bool alive = true;
        public delegate void WorkDelegate();
        WorkDelegate foodDelegate;
        WorkDelegate woodDelegate;
        WorkDelegate metalDelegate;
        WorkDelegate buildBuilding;

        public Worker(string name, Village village)
        {
            this.name = name;
            foodDelegate = new WorkDelegate(village.AddFood);
            woodDelegate = new WorkDelegate(village.AddWood);
            metalDelegate = new WorkDelegate(village.AddMetal);
            buildBuilding = new WorkDelegate(village.Build);
        }
        public void DoWork(string occupation)
        {
            if (occupation == "Food")
            {
                foodDelegate();
            }
            else if (occupation == "Wood")
            {
                woodDelegate();
            }
            else if (occupation == "Metal")
            {
                metalDelegate();
            }
            else if (occupation == "Build")
            {
                buildBuilding();
            } 
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public bool Hungry
        {
            get { return hungry; }
            set { hungry = value; }
        }
        public int DaysHungry
        {
            get { return daysHungry; }
            set { daysHungry = value; }
        }
        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }
    }
}