using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VillageOfTesting_Marc_Garpstål;
using Xunit.Abstractions;

namespace VillageOfTesting_Test
{
    public class Village_Mock_Test
    {

        [Fact]
        public void LoadProgressFromDatabase()
        {
            Village villageTest = new(10, 10, 0, null, null, null, null, 0, 0, 0, 0, 0); // Skapar en Test Village för att kontrollera vad som ska retuneras.

            Mock<DatabaseConnection> dbcMock = new Mock<DatabaseConnection>(); // Gör en mock av DatabaseConnection
            Village village1 = new(dbcMock.Object); // Skapar en ny Village som jag ska ladda variablar till, som tar en parameter (DatabaseConnection) vilket min mock är. dbcMock.Object
            int woodExpected = 10;

            dbcMock.Setup(dbc => dbc.Load()).Returns(villageTest); // Använder mitt mock object i en setup som tillåter dig att använda metoderna från Databaseconnection.
                                                                   // Som jag bestämmer ska retunera den village jag skapar överst med förutbestämda värden.

            village1.LoadProgress(); // Nu måste jag ladda in mina värden i min Village1 som innehåller metoden LoadProgress
                                     // Nu har jag lagt in värdena från Databasen till min village1 och kan asserta mot min expected.
            Assert.Equal(woodExpected, village1.Wood);
        }
        [Fact]
        public void LoadProgressWith10Food10Wood10Metal5DaysGone()
        {
            Village villageTest = new(10, 10, 10, null, null, null, null, 0, 0, 0, 0, 5);

            Mock<DatabaseConnection> dbcMock = new Mock<DatabaseConnection>();
            Village village1 = new(dbcMock.Object);
            int foodExpected = 10;
            int woodExpected = 10;
            int metalExpected = 10;
            int daysGoneExpected = 5;

            dbcMock.Setup(dbc => dbc.Load()).Returns(villageTest);
            village1.LoadProgress();

            Assert.Equal(foodExpected, village1.Food);
            Assert.Equal(woodExpected, village1.Wood);
            Assert.Equal(metalExpected, village1.Metal);
            Assert.Equal(daysGoneExpected, village1.DaysGone);
        }
        [Fact]
        public void DoRandomWorkTestReturnNumber1ShouldThenGatherFood()
        {
            Mock<RandomWork> randomMock = new Mock<RandomWork>();
            Village village1 = new();
            Worker worker = new("John", village1);
            int exepcted = 15;

            randomMock.Setup(rndMock => rndMock.RandomWorkNumber()).Returns(1); // When returning number 1 Food will be gathered.
            string one = AssignedWorkFromRandom(randomMock.Object);
            worker.DoWork(one);
            

            int actual = village1.Food;

            Assert.Equal(exepcted, actual);

        }
        [Fact]
        public void DoRandomWorkTestReturnNumber2ShouldThenChopWood()
        {
            Mock<RandomWork> randomMock = new Mock<RandomWork>();
            Village village1 = new();
            Worker worker = new("John", village1);
            int exepcted = 1;

            randomMock.Setup(rndMock => rndMock.RandomWorkNumber()).Returns(2); // When returning number 2 Wood will be gathered.
            string two = AssignedWorkFromRandom(randomMock.Object);
            worker.DoWork(two);

            int actual = village1.Wood;

            Assert.Equal(exepcted, actual);

        }
        [Fact]
        public void DoRandomWorkTestReturnNumber3ShouldThenMineMetal()
        {
            Mock<RandomWork> randomMock = new Mock<RandomWork>();
            Village village1 = new();
            Worker worker = new("John", village1);
            int exepcted = 1;

            randomMock.Setup(rndMock => rndMock.RandomWorkNumber()).Returns(3); // When returning number 3 Metal will be gathered.
            string three = AssignedWorkFromRandom(randomMock.Object);
            worker.DoWork(three);

            int actual = village1.Metal;

            Assert.Equal(exepcted, actual);
        }
        private void AddWorkers(Village village, params string[] workerNames)
        {
            foreach (var workerName in workerNames)
            {
                village.AddWorker(workerName, village);
            }
        }
        private string AssignedWorkFromRandom(RandomWork random)
        {
            
            int ran = random.RandomWorkNumber();
            if(ran == 1)
            {
                return "Food";
            }
            if(ran == 2) 
            {
                return "Wood";
            }
            else
            {
                return "Metal";
            }
            
        }
    }
}

