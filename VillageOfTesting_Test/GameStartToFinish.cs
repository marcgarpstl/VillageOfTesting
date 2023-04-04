using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageOfTesting_Marc_Garpstål;
using Xunit.Abstractions;

namespace VillageOfTesting_Test
{
    public class GameStartToFinish
    {
        ITestOutputHelper output;
        public GameStartToFinish(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public void GameStartToFinishTestWith6WorkersShouldTake9Days()
        {
            var village = CreateVillageWithProject("Castle", 50, 50);
            AddWorkers(village, "John0", "John1", "John2", "John3", "John4", "John5");
            village.Food = 100;
            WorkersWork(village);
            
            int expected = 9;

            int actual = village.DaysGone;
            Assert.True(village.Buildings[3].complete);
            Assert.Equal(expected, actual);
            output.WriteLine(village.Buildings[3].name);
        }
        [Fact]
        public void GameStartToFinishTestWith10WorkersShouldTake5Days()
        {
            var village = CreateVillageWithProject("Castle", 50, 50);
            village.AvailableWorkers = 10;
            village.Food = 100;
            AddWorkers(village, "John0", "John1", "John2", "John3", "John4", "John5", "John6", "John7", "John8", "John9");
            WorkersWork(village);


            int expected = 5;
            int actual = village.DaysGone;

            Assert.Empty(village.InProgess);
            Assert.Equal(expected, actual);
            output.WriteLine(village.Buildings[3].name);
        }
        [Fact]
        public void GameStartToFinishTestWith1WorkerShouldTake50Days()
        {
            var village = CreateVillageWithProject("Castle", 50, 50);
            village.Food = 100;
            AddWorkers(village, "John");
            WorkersWork(village);

            int expected = 50;
            int actual = village.DaysGone;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GameStartToFinishTestWith1WorkerShouldStopAt49WhenNotFed()
        {
            var village = CreateVillageWithProject("Castle", 50, 50);
            village.Food = 49;
            AddWorkers(village, "John");
            WorkersWork(village);

            int expected = 49;
            int actual = village.DaysGone;
            output.WriteLine("Hungry worker day : " + village.Workers[0].DaysHungry.ToString());

            Assert.Single(village.InProgess); // Project still in list.
            Assert.Equal(expected, actual);
        }
        private void AddWorkers(Village village, params string[] workerNames)
        {
            foreach (var workerName in workerNames)
            {
                village.AddWorker(workerName, village);
            }
        }
        private Village CreateVillageWithProject(string projectName, int wood, int metal)
        {
            var village = new Village { Wood = wood, Metal = metal };
            village.AddProject(projectName);
            return village;
        }

        private void WorkersWork(Village village)
        {
            while (village.InProgess.Count != 0)
            {
                foreach (var worker in village.Workers)
                {
                    if (village.InProgess.Count == 0)
                    {
                        break;
                    }
                    else if (worker.Hungry == true)
                    {
                        return;
                    }
                    worker.DoWork("Build");
                }
                village.Days();
            }
        }
    }
}
