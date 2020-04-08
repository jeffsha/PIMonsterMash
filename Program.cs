using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Data;
using OSIsoft.AF.PI;
using PIMonsterMash;

namespace PIMonsterMash
{
    class Program
    {
        static void Main(string[] args)
        {
            StatsManager statsManager;

            Console.SetWindowSize(80, 25);
            Console.BufferWidth = 80;
            Console.BufferHeight = 25;            

            // Setup Player Name
            Console.WriteLine("Please enter your player name:");
            var playerName = Console.ReadLine();

            Console.WriteLine("Please enter your PI Server:");
            var serverName = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Loading Game, Please Wait...");

            statsManager = new StatsManager(playerName, serverName);

            statsManager.IntializePlayerStats();

            var terminationKey = new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false);
            var currentKey = new ConsoleKeyInfo();

            Console.WriteLine("Press H to attack, X to Leave the game!");



            while (!(currentKey = Console.ReadKey()).Equals(terminationKey))
            {
                var attackRoll = DiceBag.RollD20();
                statsManager.UpdateAttackRollStat(attackRoll);

                // Clear Screen
                Console.Clear();
                DrawUI();
            }
        }

        public static void DrawUI()
        {
            // clear screen, draw basic text ui
            Console.Clear();
            Utils.AlignText("Welcome to The PI Monster Mash!!!", Utils.LineLocation.Center);
            Utils.AlignText("Monster Name", Utils.LineLocation.Center, 50, 50, ConsoleColor.Red);
            Utils.AlignText("Player Name", Utils.LineLocation.BottomRight, 25, 25, ConsoleColor.Green);
        }
    }
}
