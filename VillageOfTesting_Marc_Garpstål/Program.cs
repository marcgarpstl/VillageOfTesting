using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VillageOfTesting_Marc_Garpstål
{
    public class Program
    {

        public static void Main(string[] args)
        {

            Village? village = null;
            while (true)
            {
                Game game = new Game();
                Console.WriteLine("1.New game");
                Console.WriteLine("2.Load game");
                Console.WriteLine("3.Exit");
                string gameStart = Console.ReadLine();
                switch (gameStart) 
                {
                    case "1":
                        village = game.GamePlay();
                        break;
                    case "2":
                        village = game.GamePlay(village);
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Not valid choice");
                        break;
                }
            }
        }
    }
}
