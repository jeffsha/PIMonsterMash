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
using System.Media;
using PIMonsterMash.Entities;

namespace PIMonsterMash
{
    class Program
    {
        const int playerMaxHP = 25000;

        static void Main(string[] args)
        {
            StatsManager statsManager;

            Console.SetWindowSize(80, 25);
            Console.BufferWidth = 80;
            Console.BufferHeight = 25;
            Console.SetWindowSize(120, 35);
            Console.BufferWidth = 120;
            Console.BufferHeight = 35;

            SoundPlayer soundDevice = new SoundPlayer();
            soundDevice.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Forest.wav";
            soundDevice.PlayLooping();

            // Setup Player Name
            Console.WriteLine("Please enter your player name:");
            var playerName = Console.ReadLine();

            var currentPlayer = EntityFactory.Create<Player>(playerName, 25000);
            var player = EntityFactory.Create<Player>(playerName, playerMaxHP);

            Console.WriteLine("Please enter your PI Server:");
            var serverName = Console.ReadLine();

            //var playerName = "Player1";
            //var serverName = Environment.MachineName;
            //var player = EntityFactory.Create<Player>(playerName, playerMaxHP);


            Console.Clear();
            Console.WriteLine("Loading Game, Please Wait...");

            statsManager = new StatsManager(playerName, serverName);

            statsManager.IntializePlayerStats();

            var terminationKey = new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false);
            ConsoleKeyInfo currentKey;

            Console.WriteLine("Press A to attack, Press S to Slash, Press F to Firebolt, Press R to Run, X to Leave the game!");

            // Generate Monster Factory
            bool playerQuit = Console.ReadKey().Equals(terminationKey);
            bool playerDied = false;

            int playerHealth = 1;
            while (playerHealth > 0)
            {
                // Spawn Monster
                // Monster Spawn - Factory?
                soundDevice.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Forest.wav";
                soundDevice.PlayLooping();

                // Select Monster
                var rand = new Random();
                Monster currentMonster = monsters[rand.Next(0, monsters.Count)];

                bool attackSuccess = false;
                int damageToMonster = 0;
                // Waiting for input
                while (!(currentKey = Console.ReadKey()).Equals(terminationKey))
                {
                    switch (currentKey.Key)
                    {
                        // Roll Big Attack!
                        case ConsoleKey.S:
                            if (!attackSuccess && DiceBag.RollD20() > 16)
                            {                             
                                damageToMonster = DiceBag.RollD12();
                            }
                            break;
                        // Roll Attack
                        case ConsoleKey.A:
                            if (!attackSuccess && DiceBag.RollD20() > 13)
                            {                                
                                damageToMonster = DiceBag.RollD8();
                            }
                            break;
                        // Roll Fire Attack
                        case ConsoleKey.F:
                            if (!attackSuccess && DiceBag.RollD20() > 10)
                            {                                
                                damageToMonster = DiceBag.RollD20();
                            }
                            break;
                        // Run away from monster ?
                        case ConsoleKey.R:
                            // Skip Monster
                            break;
                    }

                    // Damage to Monster
                    DamageMonster(damageToMonster, currentMonster, currentPlayer);

                    // Check if Monster isAlive

                    // DO X, DO Y - Attack Monster
                    // Roll Damage
                    var attackRoll = DiceBag.RollD20();
                    statsManager.UpdateAttackRollStat(attackRoll);
                    // Monster Turn
                    MonsterTurn(currentMonster, currentPlayer);

                    damageToMonster = 0;
                }
                playerHealth = 0;
            }
        }

        public static void DamageMonster(int damageToMonster, Monster currentMonster, Player currentPlayer)
        {
            Console.Clear();
            DrawUI(currentMonster, currentPlayer);

            Console.Write("Player Dealt " + damageToMonster + " damage!");
            Console.Write("Press Any Button To Continue");
            Console.ReadKey();
        }

        public static void MonsterTurn(Monster currentMonster, Player currentPlayer)
        {
            int playerDamage = DiceBag.RollD8();

                    player.Damage(500);
                    if (player.Health > 0)
                    {
                        DrawUI(monster, player, "The Monster attacks! Press H to attack again.");
                        key = Console.ReadKey();
                    }
                    else
                    {
                        DrawUI(monster, player, "You have died!");
                        playerDied = true;
                    }

                    if (key == terminationKey)
                    {
                        playerQuit = true;
                        //break;
                    }
                }
            }

            if (playerQuit)
                Console.WriteLine("Thanks for playing!");
            else if (playerDied)
                Console.WriteLine("Better luck next time :(");
            Console.ReadLine();
        }

        public static void DrawUI(Monster currentMonster, Player currentPlayer)
        {
            // clear screen, draw basic text ui
            Console.Clear();
            Utils.AlignText("Welcome to The PI Monster Mash!!!", Utils.LineLocation.Center);
            Utils.AlignText("Monster Name", Utils.LineLocation.Center, 50, 50, ConsoleColor.Red);
            foreach(string line in currentMonster.Art)
            {
                Console.WriteLine(spaces + line);
            }
            Utils.AlignText(p.Name, Utils.LineLocation.BottomRight, p.Health, playerMaxHP, ConsoleColor.Green);
            Console.WriteLine();
            Console.WriteLine(action);
            // todo: prompt for hit roll?
            // todo: prompt for damage roll?
        }

        static void SetupPIPoints(string playerName, string serverName)
        {
            // Setup PI Tags
            var currentPISystem = PISystem.CreatePISystem(serverName, true);
            var currentPIServer = PIServer.FindPIServer(currentPISystem, serverName);

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
