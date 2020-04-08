using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Data;
using OSIsoft.AF.PI;

namespace PIMonsterMash
{
    class Program
    {

//· Create PI Tags
//· Call other people’s functions

        static void Main(string[] args)
        {
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
                Console.WriteLine("Hello World, I will call your methods here.");
            }
        }

        static void SetupPIPoints(string playerName)
        {
            // Setup PI Tags
            var currentPISystem = PISystem.CreatePISystem("BSIMPSON55302", true);
            var currentPIServer = PIServer.FindPIServer(currentPISystem, "BSIMPSON55302");            

            // PlayerName - Assuming full name/Unique name
            // Try to create if not exist
            var points = PIPoint.FindPIPoints(currentPIServer, playerName + "*");
            var pointVals = points.ToList();

            if (pointVals.Count() < 1) {
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
