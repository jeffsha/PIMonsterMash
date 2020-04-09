using System;
using System.Collections.Generic;
using System.Linq;
using OSIsoft.AF;
using OSIsoft.AF.PI;
using PIMonsterMash.Entities;

namespace PIMonsterMash {
    class Program {
        const int startingHP = 25000;
        const string attackString = "Press A to attack, Press S to Slash, Press F to Firebolt, Press R to Run, X to Leave the game!";

        static Monster monster;
        static Player player;
        static StatsManager statsManager;
        static List<string> messages = new List<string>();

        static void Main(string[] args) {
            //TODO: Show splash screen - our current intro needs work, generate maui team ascii art

            //Required Console Dimensions
            InitializeConsole();

            //Creates new player object and assigned event handlers
            SpawnPlayer();

            Console.Write("Please enter your PI Server:");
            var serverName = Console.ReadLine();

            Console.WriteLine("Loading Game, Please Wait...");

            statsManager = new StatsManager(player.Name, serverName);
            statsManager.IntializePlayerStats();

            var terminationKey = new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false);
            ConsoleKeyInfo currentKey;


            // Spawn first monster
            monster = SpawnMonster();

            messages.Add(attackString);
            DrawUI();

            var damage = 0;

            while (!(currentKey = Console.ReadKey()).Equals(terminationKey)) {
                bool ranAway = false;
                switch (currentKey.Key) {
                    // Roll Big Attack!
                    case ConsoleKey.S:
                        if (DiceBag.RollD20() > 16) {
                            damage = DiceBag.RollD12();
                        }
                        break;
                    // Roll Attack
                    case ConsoleKey.A:
                        if (DiceBag.RollD20() > 13) {
                            damage = DiceBag.RollD8();
                        }
                        break;
                    // Roll Fire Attack
                    case ConsoleKey.F:
                        if (DiceBag.RollD20() > 10) {
                            damage = DiceBag.RollD20();
                        }
                        break;
                    // Run away from monster
                    case ConsoleKey.R:
                        messages.Add($"{player.Name} successfully runs away from {monster.Name}");
                        monster = SpawnMonster();
                        ranAway = true;
                        break;
                }

                if (damage > 0) {
                    monster.Damage(damage);
                }
                else if (!ranAway) {
                    messages.Add($"{player.Name} Missed!");
                }

                // Monster Turn
                //TODO: Could add miss logic like above for player
                player.Damage(DiceBag.RollD8());

                damage = 0;

                messages.Add(attackString);

                DrawUI();
            }
        }

        private static void InitializeConsole() {
            Console.SetWindowSize(120, 35);
            Console.BufferWidth = 120;
            Console.BufferHeight = 35;
        }

        #region Spawn Entities
        private static void SpawnPlayer() {
            Console.Write("Please enter your player name: ");
            player = EntityFactory.Create<Player>(Console.ReadLine(), startingHP);

            player.Spawned += (entity) => {
                messages.Add($"{entity.Name} has joined the battle");
                //TODO: Can play intro music? 
                //TODO: Can display "blah blah adventurer joins the battle..." 
            };

            player.Damaged += (entity, e) => {
                if (entity.Health <= 0 && monster.Health > 0) {
                    //TODO: Game over message
                }
            };

            player.Spawn();
        }

        public static Monster SpawnMonster() {
            monster = EntityFactory.Create<Monster>();

            monster.Spawned += (entity) => {
                MusicPlayer.Play(entity.MusicPath);
                messages.Add($"{entity.Name} has joined the battle");
            };

            monster.Damaged += (entity, e) => {
                // If Monster is dead and player is alive, spawn new monster
                if (entity.Health <= 0 && player.Health > 0) {
                    //TODO: Metrics to record the # of monsters killed goes here
                    MusicPlayer.Stop();
                    monster = SpawnMonster();

                    //If monster and player are still alive, update ui
                }
                else if (entity.Health > 0 && player.Health > 0) {
                    DamageMonster(e.Damage, (Monster)entity, player);
                }
            };

            monster.Spawn();
            return monster;
        }
        #endregion  

        public static void DamageMonster(int damageToMonster, Monster currentMonster, Player currentPlayer) {
            Console.Clear();

            messages.Add($"{player.Name} Dealt {damageToMonster} damage!");
            messages.Add(attackString);
            DrawUI();

            Console.ReadKey();
        }

        public static void DrawUI() {
            {
                // clear screen, draw basic text ui	            
                Console.Clear();
                Utils.AlignText("Welcome to The PI Monster Mash!!!", Utils.LineLocation.Center);
                Utils.AlignText(monster.Name, Utils.LineLocation.Center, monster.Health, monster.MaxHealth, textColor: ConsoleColor.Red);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                var spaces = new string(' ', 30);
                foreach (string line in monster.Art) {
                    {
                        Console.WriteLine(spaces + line);
                    }
                }
                Utils.AlignText(player.Name, Utils.LineLocation.BottomRight, player.Health, player.MaxHealth, messages.Count, ConsoleColor.Green);
                Console.WriteLine();
                foreach (string message in messages) {
                    Console.WriteLine(message);
                }
                messages.Clear();
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
