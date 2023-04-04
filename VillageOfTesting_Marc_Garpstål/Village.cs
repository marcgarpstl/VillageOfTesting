using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VillageOfTesting_Marc_Garpstål
{
    public class Village
    {

        private int food;
        private int wood;
        private int metal;
        private List<Worker> workers = new List<Worker>();
        private List<Building> buildings = new List<Building>();
        private List<Building> inProgess = new List<Building>();
        private List<Building> projects = new List<Building>();
        public int metalPerDay = 1;
        public int woodPerDay = 1;
        public int foodPerDay = 5;
        private int availableWorkers;
        private int daysGone;
        public DatabaseConnection DatabaseConnection = new DatabaseConnection();
        

        public Village(int food, int wood, int metal, List<Worker> workers, List<Building> buildings, List<Building> inProgess, List<Building> projects, int metalPerDay, int woodPerDay, int foodPerDay, int availableWorkers, int daysGone)
        {
            this.food = food;
            this.wood = wood;
            this.metal = metal;
            this.workers = workers;
            this.buildings = buildings;
            this.inProgess = inProgess;
            this.projects = projects;
            this.metalPerDay = metalPerDay;
            this.woodPerDay = woodPerDay;
            this.foodPerDay = foodPerDay;
            this.availableWorkers = availableWorkers;
            this.daysGone = daysGone; 
        }

        public Village(DatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }

        public Village()
        {
            StartSetUp();
            AddAllProjects(); 
        }
        public int AvailableWorkers
        {
            get { return availableWorkers; }
            set { availableWorkers = value; }
        }
        public int DaysGone
        {
            get { return daysGone; }
            set { daysGone = value; }
        }
        public int Metal
        {
            get { return metal; }
            set { metal = value; }
        }
        public int Wood
        {
            get { return wood; }
            set { wood = value; }
        }
        public int Food
        {
            get { return food; }
            set { food = value; }
        }
        public List<Worker> Workers 
        {
            get { return workers; }
            set { workers = value; }
        }
        public List<Building> Buildings
        {
            get { return buildings; }
            set { buildings = value; }
        }
        public List<Building> InProgess
        {
            get { return inProgess; }
            set { inProgess = value; }
        }
        public List<Building> Projects
        {
            get { return projects; }
            set { projects = value; }
        }
        public void ShowAllRescouces()
        {
            Console.WriteLine("Food gathered: " + Food + "\nWood gathered: " + wood + "\nMetal gathered: " + metal);
            Console.WriteLine("Workers: " + Workers.Count + "/" + availableWorkers + "\nDays passed: " + daysGone + "\n");
        }
        internal void StartSetUp()
        {
            food = 10;
            availableWorkers = 6;
            Building house = new Building(
                "House", 5, 0, 3, 3, true);
            buildings.Add(house);
            buildings.Add(house);
            buildings.Add(house);
        }
        internal void AddAllProjects()
        {
            Building house = new(
                "House", 5, 0, 0, 3, false);
            Building woodmill = new(
                "Woodmill", 5, 1, 0, 5, false);
            Building quarry = new(
                "Quarry", 3, 5, 0, 7, false);
            Building farm = new(
                "Farm", 5, 2, 0, 5, false);
            Building castle = new(
                "Castle", 50, 50, 0, 50, false);
            projects.Add(house);
            projects.Add(woodmill);
            projects.Add(quarry);
            projects.Add(farm);
            projects.Add(castle);
        }
        public void AddWorker(string name, Village village)
        {
            if (Workers.Count < availableWorkers)
            {
                Worker worker = new(name, village);
                Console.WriteLine(worker.Name + " is added to the workforce");
                Workers.Add(worker);
            }
            else
            {
                Console.WriteLine("You need to build more houses");
            }
        }
        public void AddProject(string name)
        {

            foreach (var building in projects)
            {
                if (name == building.name)
                {
                    if (wood >= building.woodCost && metal >= building.metalCost)
                    {
                        inProgess.Add(building);
                        wood -= building.woodCost;
                        metal -= building.metalCost;
                        Console.WriteLine(building.name + " will take " + building.daysToComplete + " day(s) to complete.");
                        projects.Remove(building);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Not enough resources for: " + name);
                    }
                }
            }
        }
        public void Days()
        {
            HungryWorkers();
            BuryDead();
            Console.WriteLine("A new day has dawn");
            daysGone++;
            FeedWorkers();
        }
        public void AddFood() => food += foodPerDay;
        public void AddWood() => wood += woodPerDay;
        public void AddMetal() => metal += metalPerDay;
        public void Build()
        {
            if (inProgess[0].daysWorkedOn < inProgess[0].daysToComplete)
            {
                inProgess[0].daysWorkedOn++;
                Console.WriteLine(inProgess[0].name + " has been worked on for " + inProgess[0].daysWorkedOn + " day(s).");
                if (inProgess[0].daysToComplete == InProgess[0].daysWorkedOn)
                {
                    if (inProgess[0].name == "House")
                    { House(); }
                    else if (inProgess[0].name == "Woodmill")
                    { Woodmill(); }
                    else if (inProgess[0].name == "Quarry")
                    { Quarry(); }
                    else if (inProgess[0].name == "Farm")
                    { Farm(); }
                    else if (inProgess[0].name == "Castle")
                    { Castle(); }
                }
            }
        }
        public void FeedWorkers()
        {
            for (int i = 0; i < Workers.Count; i++)
            {

                if (Food > 0)
                {
                    Food -= 1;
                    Console.WriteLine(Workers[i].Name + " has been fed");
                    Workers[i].DaysHungry = 0;
                    Workers[i].Hungry = false;
                }

                if (Food == 0)
                {
                    Workers[i].Hungry = true;
                    Workers[i].DaysHungry++;
                }
            }

        }
        public bool HungryWorkers()
        {
            for (int i = 0; i < Workers.Count; i++)
            {
                if (Workers[i].DaysHungry >= 8 && Workers[i].DaysHungry <= 10)
                {
                    Console.WriteLine("WARNING! " + Workers[i].Name + " has been without food for " + Workers[i].DaysHungry + " days");
                    
                }
                if (Workers[i].DaysHungry > 10)
                {
                    return Workers[i].Alive = false;
                }
            }  
            return true;
        }
        public bool BuryDead()
        {
            if (HungryWorkers() == false)
            {
                for (int i = 0; i < Workers.Count; i++)
                {
                    Console.WriteLine(Workers[i].Name + " has died during the night.");
                    Console.WriteLine(Workers[i].Name + " is being buried without a ceremony");
                    Console.WriteLine("You should feel ashamed that you couldn't feed your people\nYou TYRANT!");
                    Workers.Remove(Workers[i]);
                    return false;
                }
            }

            if (!Workers.Any())
            {
                return true;
            }
            return false;
        }
        protected int House()
        {
            FinishedBuilding();
            Building house = new(
                "House", 5, 0, 0, 3, false);
            projects.Insert(0, house);
            return availableWorkers += 2; }
        protected int Woodmill()
        {
            FinishedBuilding();
            Building woodmill = new(
                "Woodmill", 5, 1, 0, 5, false);
            projects.Insert(1, woodmill);
            return woodPerDay += 2;
        }
        protected int Quarry()
        {
            FinishedBuilding();
            Building quarry = new(
                "Quarry", 3, 5, 0, 7, false);
            projects.Insert(2, quarry);
            return metalPerDay += 2;
        }
        protected int Farm()
        {
            FinishedBuilding();
            Building farm = new(
                "Farm", 5, 2, 0, 5, false);
            projects.Insert(3, farm);
            return foodPerDay += 10;
        }
        private void Castle()
        {
            
            FinishedBuilding();
            Console.WriteLine("You have won the game!");
            Console.WriteLine("It took " + DaysGone + " days!");

        }
        public void FinishedBuilding()
        {
            inProgess[0].complete = true;
            Console.WriteLine(inProgess[0].name + " is finished");
            buildings.Add(inProgess[0]);
            inProgess.RemoveAt(0);

        }
        public void GameEndFromDeath()
        {
            Console.ReadKey();
            Environment.Exit(1);
        } // Not sure how to test this.
        public void SaveProgress()
        {
            DatabaseConnection.Save(this);
        }
        public void LoadProgress()
        {
            Village v = DatabaseConnection.Load();
            food = v.food;
            wood = v.wood;
            metal = v.metal;
            foodPerDay = v.foodPerDay;
            woodPerDay = v.woodPerDay;
            metalPerDay = v.metalPerDay;
            workers = v.workers;
            buildings = v.buildings;
            inProgess = v.inProgess;
            projects = v.projects;
            availableWorkers = v.availableWorkers;
            daysGone = v.daysGone;
            
        }
    }
}
