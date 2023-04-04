using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageOfTesting_Marc_Garpstål;

namespace VillageOfTesting_Test
{
    public class Village_Projects_Test
    {

        [Fact]
        public void AddAllProjectsShouldContain5Projects()
        {
            Village village = new Village();

            Assert.Equal(5, village.Projects.Count);
        }


        [Fact]
        public void NewProjectUpdatesInProgressShouldBeOneElementInList()
        {
            var village = CreateVillageWithProject("Farm", 5, 2);
            Assert.Single(village.InProgess);
        }

        [Fact]
        public void NewProjectWithInvalidBuildingShouldNotUpdateInProgress()
        {
            var village = CreateVillageWithProject("Kalle", 5, 2);
            Assert.Empty(village.InProgess);
        }

        [Fact]
        public void AddProjectToVillageInprogessListShouldFarm()
        {
            //Given
            var expectedProjectName = "Farm";
            var village = CreateVillageWithProject(expectedProjectName, 5, 2);

            //When

            string actualProjectName = village.InProgess[0].name.ToString();

            //Then
            Assert.Equal(expectedProjectName, actualProjectName);
        }
        [Fact]
        public void AddProjectToVillageInprogessListShouldQuarry()
        {
            //Given
            var expectedProjectName = "Quarry";
            var village = CreateVillageWithProject(expectedProjectName, 3, 5);

            //When

            string actualProjectName = village.InProgess[0].name.ToString();

            //Then
            Assert.Equal(expectedProjectName, actualProjectName);
        }
        [Fact]
        public void AddProjectToVillageInprogessListShouldBeCastle()
        {
            //Given
            var expectedProjectName = "Castle";
            var village = CreateVillageWithProject(expectedProjectName, 50, 50);

            //When

            string actualProjectName = village.InProgess[0].name.ToString();

            //Then
            Assert.Equal(expectedProjectName, actualProjectName);
        }
        [Fact]
        public void AddProjectResourcesAmountWoodShouldBe0()
        {

            //Given
            var village = CreateVillageWithProject("Farm", 5, 2);
            int expected = 0;

            //When

            Assert.Equal(expected, village.Wood);
        }
        [Fact]
        public void AddProjectResourcesAmountMetalShouldBe0()
        {
            // Given
            var village = CreateVillageWithProject("Farm", 5, 2);
            int expected = 0;

            //When

            Assert.Equal(expected, village.Metal);
        }
        [Fact]
        public void AddProjectResourcesAmountWoodQuarryShouldRemove3FromWood()
        {
            //Given
            var village = CreateVillageWithProject("Quarry", 15, 15);
            int expected = 12;

            //When

            Assert.Equal(expected, village.Wood);
        }
        [Fact]
        public void AddProjectResourcesAmountMetalQuarryShouldRemove5FromMetal()
        {
            
            //Given
            var village = CreateVillageWithProject("Quarry", 5, 12);
            int expected = 7;

            //Then
            Assert.Equal(expected, village.Metal);
        }
        [Fact]
        public void TryToAddProjectWithOutResourcesShouldBeEmptyList()
        {
            //Given
            var village = CreateVillageWithProject("Farm", 0, 0);


            //Then
            Assert.Empty(village.InProgess);
        }

        private Village CreateVillageWithProject(string projectName, int wood, int metal)
        {
            var village = new Village { Wood = wood, Metal = metal };
            village.AddProject(projectName);
            return village;
        }
    }
}
