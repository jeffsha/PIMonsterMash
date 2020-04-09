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

namespace PIMonsterMash {
    class Program {
        const int playerMaxHP = 25000;
        static Monster monster;
        static Player player;
        static StatsManager statsManager;
        static bool playerDied = false;
        static bool playerQuit;

        static void Main(string[] args) {
            Console.SetWindowSize(80, 25);
            Console.BufferWidth = 80;
            Console.BufferHeight = 25;
            Console.SetWindowSize(120, 35);
            Console.BufferWidth = 120;
            Console.BufferHeight = 35;

            // Setup Player Name
            Console.WriteLine("Please enter your player name:");
            var playerName = Console.ReadLine();

            player = EntityFactory.Create<Player>(playerName, playerMaxHP);

            //Console.WriteLine("Please enter your PI Server:");
            //var serverName = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Loading Game, Please Wait...");

            // statsManager = new StatsManager(playerName, serverName);

            // statsManager.IntializePlayerStats();

            var terminationKey = new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false);
            ConsoleKeyInfo currentKey;

            Console.WriteLine("Press A to attack, Press S to Slash, Press F to Firebolt, Press R to Run, X to Leave the game!");

            // Generate Monster Factory
            monster = SpawnMonster(player);

            playerQuit = Console.ReadKey().Equals(terminationKey);

            int playerHealth = 1;
            while (playerHealth > 0) {
                // Spawn Monster
                // Monster Spawn - Factory?

                // Select Monster
                var rand = new Random();

                int damageToMonster = 0;
                // Waiting for input
                while (!(currentKey = Console.ReadKey()).Equals(terminationKey)) {
                    switch (currentKey.Key) {
                        // Roll Big Attack!
                        case ConsoleKey.S:
                            if (DiceBag.RollD20() > 16) {
                                damageToMonster = DiceBag.RollD12();
                            }
                            break;
                        // Roll Attack
                        case ConsoleKey.A:
                            if (DiceBag.RollD20() > 13) {
                                damageToMonster = DiceBag.RollD8();
                            }
                            break;
                        // Roll Fire Attack
                        case ConsoleKey.F:
                            if (DiceBag.RollD20() > 10) {
                                damageToMonster = DiceBag.RollD20();
                            }
                            break;
                        // Run away from monster ?
                        case ConsoleKey.R:
                            // Skip Monster
                            break;
                    }

                    // Damage to Monster
                    DamageMonster(damageToMonster, monster, player);

                    // Check if Monster isAlive

                    // DO X, DO Y - Attack Monster
                    // Roll Damage
                    var attackRoll = DiceBag.RollD20();
                    //statsManager.UpdateAttackRollStat(attackRoll);

                    // Monster Turn
                    player.Damage(DiceBag.RollD8());
                    DrawUI(monster, player, "The Monster attacks! Press H to attack again.");

                    damageToMonster = 0;
                }
                playerHealth = 0;
            }
        }

        public static Monster SpawnMonster(Player player) {
            monster = EntityFactory.Create<Monster>();

            monster.Spawned += (entity) => {
                MusicPlayer.Play(entity.MusicPath);
            };

            monster.Damaged += (entity) => {
                // If Monster is dead and player is alive, spawn new monster
                if (entity.Health <= 0 && player.Health > 0) {
                    MusicPlayer.Stop();
                    monster = SpawnMonster(player);
                }
            };

            monster.Spawn();

            return monster;
        }

        public static void DamageMonster(int damageToMonster, Monster currentMonster, Player currentPlayer) {
            Console.Clear();
            DrawUI(currentMonster, currentPlayer, "The Monster attacks! Press H to attack again.");

            Console.Write("Player Dealt " + damageToMonster + " damage!");
            Console.Write("Press Any Button To Continue");
            Console.ReadKey();
        }

        public static void DrawUI(Monster m, Player p, string action) {
            {
                // clear screen, draw basic text ui	            // clear screen, draw basic text ui
                Console.Clear();
                Utils.AlignText("Welcome to The PI Monster Mash!!!", Utils.LineLocation.Center);
                Utils.AlignText(m.Name, Utils.LineLocation.Center, m.Health, 500000, ConsoleColor.Red);
                foreach (string line in m.Art)              // TODO: Need also a max hp
                    Utils.AlignText(line, Utils.LineLocation.Center, textColor: ConsoleColor.Red);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                string spaces = new string(' ', 30);
                Utils.AlignText(p.Name, Utils.LineLocation.BottomRight, 25, 25, ConsoleColor.Green);
                Console.WriteLine();
                Console.WriteLine(action);
                // todo: prompt for hit roll?
                // todo: prompt for damage roll?
            }
        }

        static void SetupPIPoints(string playerName, string serverName) {
            // Setup PI Tags
            var currentPISystem = PISystem.CreatePISystem(serverName, true);
            var currentPIServer = PIServer.FindPIServer(currentPISystem, serverName);

            // PlayerName - Assuming full name/Unique name
            // Try to create if not exist
            var points = PIPoint.FindPIPoints(currentPIServer, playerName + "*");

            if (points.Count() < 1) {
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
