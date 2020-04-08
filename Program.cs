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
            Console.SetWindowSize(80, 25);
            Console.BufferWidth = 80;
            Console.BufferHeight = 25;            

            // Setup Player Name
            Console.WriteLine("Please enter your player name:");
            var playerName = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Loading Game, Please Wait...");
            SetupPIPoints(playerName);

            var terminationKey = new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false);
            var currentKey = new ConsoleKeyInfo();

            Console.WriteLine("Press H to attack, X to Leave the game!");

            while (!(currentKey = Console.ReadKey()).Equals(terminationKey))
            {
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

        static void SetupPIPoints(string playerName)
        {
            // Setup PI Tags
            var currentPISystem = PISystem.CreatePISystem("BSIMPSON55302", true);
            var currentPIServer = PIServer.FindPIServer(currentPISystem, "BSIMPSON55302");

            // PlayerName - Assuming full name/Unique name
            // Try to create if not exist
            var points = PIPoint.FindPIPoints(currentPIServer, playerName + "*");            

            if (points.Count() < 1)
            {
                IDictionary<string, object> intAttributeProperties = new Dictionary<string, object> {
                    { PICommonPointAttributes.PointType, PIPointType.Int32 },
                    { PICommonPointAttributes.Compressing, 0 },
                    { PICommonPointAttributes.Shutdown, 0 }
                };

                IDictionary<string, IDictionary<string, object>> pointsAttributesTable = new Dictionary<string, IDictionary<string, object>>();
                pointsAttributesTable.Add(playerName + "Score", intAttributeProperties);
                pointsAttributesTable.Add(playerName + "Rolls", intAttributeProperties);
                pointsAttributesTable.Add(playerName + "Turns", intAttributeProperties);

                currentPIServer.CreatePIPoints(pointsAttributesTable);
            }
        }
    }
}
