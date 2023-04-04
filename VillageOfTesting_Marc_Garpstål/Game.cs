using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace VillageOfTesting_Marc_Garpstål
{
    public class Game
    {
        
        public Village GamePlay(Village? village=null)
        {
            village ??= new Village();
            RandomWork random = new RandomWork();
            Random rnd = new Random();
            Console.WriteLine("Welcome to the city of Alexandria");
            Console.WriteLine("This is a game with mutliple choices you can make, make the wrong choices and your workers die.");
            Console.WriteLine("If they die, you will lose!");
            Console.WriteLine("Otherwise you might be prosperous and make you way to build the Castle, which is the main goal.");
            while (true)
            {
                Console.WriteLine("1.Add worker.");
                Console.WriteLine("2.Show all workers");
                Console.WriteLine("3.Add project.");
                Console.WriteLine("4.Show all projects.");
                Console.WriteLine("5.Show all resources.");
                Console.WriteLine("6.Do work.");
                Console.WriteLine("7.Save game.");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Random rndNames = new Random();
                        string[] workerNames = { "John", "Merdith", "Noah", "Grugg", "Jim",
                                     "Jenny", "Micheal", "Theresa", "Sebastian",
                                     "Elisa", "Anna", "Emma", "Arthur", "Emmanuel",
                                     "Emily", "Tim", "Petra", "Jack", "Galadriel",
                                     "Frodo", "Sam", "Rose", "Lily", "Eowyn",
                                     "Aurora", "Cathrine", "Jonas", "Oliver", "Carla"};

                        var existingWorkers = new List<string>();
                        foreach (var worker in village.Workers)
                        {
                            existingWorkers.Add(worker.Name);
                        }

                        bool addWorker = false;
                        string workerToAdd = string.Empty;
                        while (!addWorker)
                        {
                            int workerNamesIndex = rndNames.Next(workerNames.Length);
                            workerToAdd = workerNames[workerNamesIndex];
                            addWorker = !existingWorkers.Contains(workerToAdd);
                        }

                        village.AddWorker(workerToAdd, village);

                        break;
                    case "2":
                        if (village.Workers.Count == 0)
                        {
                            Console.WriteLine("No workers added");
                        }
                        foreach (var worker in village.Workers)
                        {
                            Console.WriteLine(worker.Name);
                        }
                        break;
                    case "3":
                        Console.WriteLine("What project would you like to start?");
                        Console.WriteLine("1. House\n2. Woodmill\n3. Quarry\n4. Farm\n5. Castle");
                        string project = Console.ReadLine();
                        switch (project)
                        {
                            case "1":
                                string project1 = "House";
                                village.AddProject(project1);
                                break;
                            case "2":
                                string project2 = "Woodmill";
                                village.AddProject(project2);
                                break;
                            case "3":
                                string project3 = "Quarry";
                                village.AddProject(project3);
                                break;
                            case "4":
                                string project4 = "Farm";
                                village.AddProject(project4);
                                break;
                            case "5":
                                string project5 = "Castle";
                                village.AddProject(project5);
                                break;
                            default:
                                Console.WriteLine("Not valid");
                                break;
                        }
                        break;
                    case "4":
                        foreach (var item in village.Projects)
                        {
                            Console.WriteLine(item.name +
                                "\nWood cost: " + item.woodCost +
                                "\nMetal cost: " + item.metalCost + "\n");
                        }
                        break;
                    case "5":
                        village.ShowAllRescouces();
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Once you have assigned all workers to a chore, a new day will dawn!");
                        Console.WriteLine("Be aware! Once assigned, the assignment is set in stone. No undo's");
                        if (village.Workers.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("There are no workers to assign work to.");
                            break;
                        }
                        foreach (var worker in village.Workers)
                        {
                            if (worker.Hungry == true)
                            {
                                Console.WriteLine(worker.Name + " is hungry and refusing to work!");
                                continue;
                            }
                            else
                                Console.WriteLine("What chores will " + worker.Name + " do today?");
                            bool running = true;
                            while (running)
                            {
                                Console.WriteLine("1.Gather food\n2.Chop wood\n3.Mine metal\n4.Build\n5.Apply work randomly");
                                string chore = Console.ReadLine();
                                switch (chore)
                                {
                                    case "1":
                                        string chore1 = "Food";
                                        Console.WriteLine(worker.Name + " will gather food.");
                                        worker.DoWork(chore1);
                                        running = false;
                                        break;
                                    case "2":
                                        string chore2 = "Wood";
                                        Console.WriteLine(worker.Name + " will chop wood.");
                                        worker.DoWork(chore2);
                                        running = false;
                                        break;
                                    case "3":
                                        string chore3 = "Metal";
                                        Console.WriteLine(worker.Name + " will mine metal.");
                                        worker.DoWork(chore3);
                                        running = false;
                                        break;
                                    case "4":
                                        if (village.InProgess.Count == 0)
                                        {
                                            Console.WriteLine("You have not started any project yet.");
                                            break;
                                        }
                                        else
                                        {
                                            string chore4 = "Build";
                                            Console.WriteLine(worker.Name + " will do construction.");
                                            worker.DoWork(chore4);
                                            running = false;
                                            break;
                                        }
                                    case "5":
                                        int x = random.RandomWorkNumber();
                                        if (x == 1)
                                        {
                                            Console.WriteLine(worker.Name + " will gather food.");
                                            worker.DoWork("Food");
                                        }
                                        else if (x == 2)
                                        {
                                            Console.WriteLine(worker.Name + " will chop wood.");
                                            worker.DoWork("Wood");
                                        }
                                        else if (x == 3)
                                        {
                                            Console.WriteLine(worker.Name + " will mine metal.");
                                            worker.DoWork("Metal");
                                        }
                                        running = false;
                                        break;
                                    default:
                                        Console.WriteLine("Please choose from presented chores");
                                        break;
                                }
                            }
                        }
                        Console.WriteLine("Press any key to pass the day");
                        Console.ReadKey();
                        Console.Clear();
                        village.Days();
                        if (village.BuryDead() == true)
                        {
                            Console.WriteLine("Game over \nAll workers has dieded");
                            village.GameEndFromDeath();
                        }
                        break;
                    case "7":
                        village.SaveProgress();
                        Console.Clear();
                        Console.WriteLine("Game is saved!");
                        return village;
                    default:
                        Console.WriteLine("You may choose something else to do");
                        break;
                }
            }
        }
    }
}
