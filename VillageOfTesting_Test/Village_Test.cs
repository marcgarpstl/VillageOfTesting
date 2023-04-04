using Microsoft.VisualStudio.TestPlatform.Utilities;
using VillageOfTesting_Marc_Garpstål;
using Xunit.Abstractions;

namespace VillageOfTesting_Test
{
    public class Village_Test
    {
        ITestOutputHelper output;
        public Village_Test(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        [Fact]
        public void NewVillageShouldStartWith10Food()
        {
            Village village = new Village();
            int expected = 10;

            int actual = village.Food;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NewVillageShouldStartWtih0Wood()
        {
            Village village = new Village();
            int expected = 0;

            int actual = village.Wood;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NewVillageShouldStartWith0Metal()
        {
            Village village = new Village();
            int expected = 0;

            int actual = village.Metal;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NewVillageShouldHave5FoodPerDay()
        {
            Village village = new Village();
            int expected = 5;

            int actual = village.foodPerDay;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NewVillageShouldHave1WoodPerDay()
        {
            Village village = new Village();
            int expected = 1;

            int actual = village.woodPerDay;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NewVillageShouldHave1MetalPerDay()
        {
            Village village = new Village();
            int expected = 1;

            int actual = village.metalPerDay;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NewVillageShouldHave6AvailableWorkersSpots()
        {
            Village village = new Village();
            int expected = 6;

            int actual = village.AvailableWorkers;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NewVillageShouldStartWith0DaysGone()
        {
            Village village = new Village();
            int expected = 0;

            int actual = village.DaysGone;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NewVillageShouldContain3HousesInBuildings()
        {
            Village village = new Village();
            int expected = 3;

            Assert.Equal(expected, village.Buildings.Count);
        }

        [Fact]
        public void NewVillageHasEmptyInProgress()
        {
            var village = new Village();
            Assert.Empty(village.InProgess);
        }

        [Fact]
        public void DoesWorkerDoWhatWorkerIsAssignedToFood()
        {
            Village village = new Village();
            AddWorkers(village, "Worker");
            village.Workers[0].DoWork("Food");
            int expected = 15;

            Assert.Equal(expected, village.Food);
        }
        [Fact]
        public void DoesWorkerDoWhatWorkerIsAssignedToWood()
        {
            Village village = new Village();
            AddWorkers(village, "Worker");
            village.Workers[0].DoWork("Wood");
            int expected = 1;

            Assert.Equal(expected, village.Wood);
        }
        [Fact]
        public void DoesWorkerDoWhatWorkerIsAssignedToMetal()
        {
            Village village = new Village();


            AddWorkers(village, "Worker");
            village.Workers[0].DoWork("Metal");
            int expected = 1;

            Assert.Equal(expected, village.Metal);
        }
        [Fact]
        public void DoesWoodmillWorkShouldAdd2ToWoodPerDay()
        {
            
            var village = CreateVillageWithProject("Woodmill", 5, 1);
            output.WriteLine("Before finish Woodmill. Wood / day : " + village.woodPerDay);
            AddWorkers(village, "John", "Erdith", "Grugg", "Jhonny", "Sam", "Erica");
            WorkersWork(village);

            int expected2 = 3;
            
            Assert.Equal(expected2, village.woodPerDay);
            output.WriteLine("After finish Woodmill. Wood / day : " + village.woodPerDay);
        }
        [Fact]
        public void DoesQuarryWorkShouldAdd2ToMetalPerDay()
        {
            var village = CreateVillageWithProject("Quarry", 3, 5);
            output.WriteLine("Before finish Quarry. Metal / day : " + village.metalPerDay);
            AddWorkers(village, "John", "Erdith", "Grugg", "Jhonny", "Sam", "Erica");
            village.InProgess[0].daysWorkedOn = 2;
            WorkersWork(village);

            int expected2 = 3;

            Assert.Equal(expected2, village.metalPerDay);
            output.WriteLine("After finish Quarry. Metal / day : " + village.metalPerDay);
        }
        [Fact]
        public void DoesFarmWorkShouldAdd5ToFoodPerDay()
        {
            var village = CreateVillageWithProject("Farm", 5, 2);
            output.WriteLine("Before finish Farm. Food / day : " + village.foodPerDay);
            AddWorkers(village, "John", "Erdith", "Grugg", "Jhonny", "Sam", "Erica");
            WorkersWork(village);
            
            int expected2 = 15;

            Assert.Equal(expected2, village.foodPerDay);
            output.WriteLine("Before finish Farm. Food / day : " + village.foodPerDay);
        }
        [Fact]
        public void DaysWorkedOnHouseIsCorrect() 
        {
            var village = CreateVillageWithProject("House", 5, 0);
            AddWorkers(village, "Noah", "Erdith", "Grugg");
            // Will get assigned on a daily basis, one day less per worker assigned to "Build"
            WorkersWork(village);
            
            int expected2 = 8;

            Assert.Equal(expected2, village.AvailableWorkers);
        }
        [Fact]
        public void WorkerDiededFromStarvation()
        {
            Village village = new Village();
            village.Food = 0;
            AddWorkers(village, "Starving Worker");
            village.Workers[0].DaysHungry = 11;
            village.Days();          
            int expected = 0; // Dead from stavation and buried.

            Assert.Equal(expected, village.Workers.Count);
        }
        [Fact]
        public void CantFeedDiededWorker()
        {
            Village village = new Village();
            village.Food = 0;
            AddWorkers(village, "Starving Worker");
            village.Workers[0].DaysHungry = 11;
            village.Days();
            village.Food = 1;
            village.Days();
            int expected = 0; // Cannot feed, Cus of dead and buried.

            Assert.Equal(expected, village.Workers.Count);
        }
        [Fact]
        public void HungryWorkerFeedShouldNotBeHungry()
        {
            Village village = new Village();
            AddWorkers(village, "Feed Worker");
            village.Workers[0].Hungry = true;
            village.FeedWorkers();
            bool expected = false;

            Assert.Equal(expected, village.Workers[0].Hungry);
        }
        [Fact]
        public void AllWorkerDiesShouldEndGame() // Not sure how to test.
        {
            Village village = new Village();
            AddWorkers(village, "Last Alive Worker");
            village.Workers[0].DaysHungry = 11;
            village.Days();


        }
        [Fact]
        public void BuryTheDead()
        {
            Village village = new Village();
            village.Food = 0;
            AddWorkers(village, "Dead Worker");
            village.Workers[0].DaysHungry = 11;
            village.Days();
            int expected = 0;

            Assert.Equal(expected, village.Workers.Count);
        }
        [Fact]
        public void AddFoodTestShouldAddFiveMoreFood()
        {
            Village village = new Village();
            // Starting food is 10
            village.AddFood();
            int expected = 15;
            int actual = village.Food;
            Assert.Equal(expected, actual);

        }
        [Fact]
        public void AddWoodTestShouldAddOneMoreWood()
        {
            Village village = new Village();
            // Starting wood is 0
            village.AddWood();
            int expected = 1;
            int actual = village.Wood;
            Assert.Equal(expected, actual);

        }
        [Fact]
        public void AddMetalTestShouldAddOneMoreMetal()
        {
            Village village = new Village();
            // Starting food is 0
            village.AddMetal();
            int expected = 1;
            int actual = village.Metal;
            Assert.Equal(expected, actual);

        }
        [Fact]
        public void BuildTestWhenUsingBuildShouldIncreaseDaysWorkedOnWithOne()
        {
            var village = CreateVillageWithProject("Castle", 50, 50);
            village.Build();
            int expected = 1;
            int actual = village.InProgess[0].daysWorkedOn;
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void BuildTestWhenUsingBuildMoreThenOneTimeShouldIncreaseDaysWorkedOnWithOnePerBuildCall()
        {
            var village = CreateVillageWithProject("Castle", 50, 50);
            village.Build();
            village.Build();
            village.Build();
            village.Build();
            village.Build();
            int expected = 5;
            int actual = village.InProgess[0].daysWorkedOn;
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void BuildTestWhenBuildingCompleteShouldBeRemoveFromInProgressList()
        {
            var village = CreateVillageWithProject("House", 5, 5);
            AddWorkers(village, "John");
            WorkersWork(village);
            Assert.Empty(village.InProgess);
        }

        private Village CreateVillageWithProject(string projectName, int wood, int metal)
        {
            var village = new Village { Wood = wood, Metal = metal };
            village.AddProject(projectName);
            return village;
        }

        private void AddWorkers(Village village, params string[] workerNames)
        {
            foreach(var workerName in workerNames)
            {
                village.AddWorker(workerName, village);
            }
        }
        private void WorkersWork(Village village)
        {
            while (village.InProgess.Count != 0)
            {
                foreach (var worker in village.Workers)
                {
                    worker.DoWork("Build");
                    if (village.InProgess.Count == 0)
                    {
                        break;
                    }
                }
                village.Days();
            }
        }
    }
}