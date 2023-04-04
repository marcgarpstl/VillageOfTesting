using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageOfTesting_Marc_Garpstål;

namespace VillageOfTesting_Test
{
    public class Village_Workers_Test
    {
        [Fact]
        public void DaysWithOutWorkersShouldAddOneDayToDaysCount()
        {
            Village village = new Village();
            village.Days();
            int expected = 1;

            Assert.Equal(expected, village.DaysGone);
        }
        [Fact]
        public void DaysAddWorkerAndFeedShouldConsumeOneFoodPerWorker()
        {
            Village village = new Village();
            AddWorkers(village, "John", "Erdith", "Marina", "Grugg");
            village.Days();
            int expected = 6;

            Assert.Equal(expected, village.Food);
        }
        [Fact]
        public void DaysAddWorkerAndFeedWithOutFood()
        {
            Village village = new Village();
            village.Food = 0;
            AddWorkers(village, "John");
            village.Days();
            bool expected = true;

            Assert.Equal(expected, village.Workers[0].Hungry);
        }
        [Fact]
        public void WorkerListIsEmptyWhenStart()
        {
            Village village = new Village();

            Assert.Empty(village.Workers);
        }
        [Fact]
        public void AddOneWorkerShouldContainsOneElementInList()
        {
            Village village = new Village();
            AddWorkers(village, "John");

            Assert.Single(village.Workers);

        }
        [Fact]
        public void AddTwoWorkersShouldContainTwoElementsInList()
        {
            Village village = new Village();
            AddWorkers(village, "John", "Erdith");
            int expected = 2;

            Assert.Equal(expected, village.Workers.Count);

        }
        [Fact]
        public void AddThreeWorkersShouldContainThreeElementsInList()
        {
            Village village = new Village();
            AddWorkers(village, "John", "Erdith", "Grugg");
            int expected = 3;

            Assert.Equal(expected, village.Workers.Count);

        }
        [Fact]
        public void AddWorkersWhenHousePlacesAreMaxed()
        {
            Village village = new Village();
            AddWorkers(village, "John", "John2", "John3", "John4", "John5", "John6", "Unbornable Baby");
            int expected = 6;

            Assert.Equal(expected, village.Workers.Count);
        }
        private void AddWorkers(Village village, params string[] workerNames)
        {
            foreach (var workerName in workerNames)
            {
                village.AddWorker(workerName, village);
            }
        }
    }
}
