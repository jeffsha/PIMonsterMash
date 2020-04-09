using System;
using System.Collections.Generic;
using System.Linq;
using OSIsoft.AF;
using OSIsoft.AF.PI;
using PIMonsterMash.Entities;

namespace PIMonsterMash
{
    class Program {
        const int playerMaxHP = 250;
        static Monster monster;
        static Player player;
        static StatsManager statsManager;
        const string ATTACKINSTRUCTIONS = "Press A to attack, Press S to Slash, Press F to Firebolt, Press R to Run, X to Leave the game!";

        // TODO:
        // Opening Maui Splash Screen
        // Monster Names
        // AC for monsters (optional)?
        // More Monsters
        // More Music For Different Monsters
        // Score board - Number of Monsters Killed - Most Damage Dealt

        static void Main(string[] args)
        {
            //Required Game Initialization
            InitializeGame();

            //Creates new player object and assigned event handlers
            SpawnPlayer();

            Console.Write("Please enter your PI Server:");
            var serverName = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Loading Game, Please Wait...");

            statsManager = new StatsManager(player.Name, serverName);
            statsManager.IntializePlayerStats();

            var terminationKey = new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false);
            ConsoleKeyInfo currentKey;
            
            Console.WriteLine(ATTACKINSTRUCTIONS);

            // Spawn first monster
            monster = SpawnMonster();
            
            var diceRoll = 0;

            while (!(currentKey = Console.ReadKey()).Equals(terminationKey))
            {
                switch (currentKey.Key)
                {
                    // Roll FireBolt!!
                    case ConsoleKey.F:
                        if ((diceRoll = DiceBag.RollD20()) >= 14)                        
                            PlayerDealsDamage(DiceBag.RollD20(), diceRoll);                                                    
                        else                        
                            AttackMissed("Firebolt", diceRoll);
                        MonsterAttemptsDamage();
                        break;
                    // Roll Slash Attack
                    case ConsoleKey.S:
                        if ((diceRoll = DiceBag.RollD20()) >= 10)                        
                            PlayerDealsDamage(DiceBag.RollD12(), diceRoll);                        
                        else
                            AttackMissed("Slash Attack", diceRoll);
                        MonsterAttemptsDamage();
                        break;
                    // Roll Basic Attack
                    case ConsoleKey.A:
                        if ((diceRoll = DiceBag.RollD20()) >= 6)                        
                            PlayerDealsDamage(DiceBag.RollD8(), diceRoll);                        
                        else
                            AttackMissed("Basic Attack", diceRoll);
                        MonsterAttemptsDamage();
                        break;
                    // Run away from monster
                    case ConsoleKey.R:
                        if ((diceRoll = DiceBag.RollD20()) > 7)
                        {
                            DrawUI(monster, player, "Successfully Fled!");
                            Console.WriteLine("Press any button to Continue.");
                            Console.ReadKey();
                            monster = SpawnMonster();                            
                        }
                        else                        
                            DrawUI(monster, player, "Failed Running from the monster!!");
                            MonsterAttemptsDamage();
                        break;
                    // Bad Input
                    default:
                        DrawUI(monster, player, "Invalid Command, Try Again!");
                        break;
                }

                if (player.Health <= 0)
                    break;
            }
           
            Console.Clear();
            Console.WriteLine("GAME OVER");
            Console.WriteLine("Press any key to continue.");            
            Console.ReadKey();

            // TODO: Display Updated Scoreboard
        }

        private static void MonsterAttemptsDamage()
        {
            Console.WriteLine("The Monster is attacking! Press any button to Continue.");
            Console.ReadKey();

            int diceRoll;
            if ((diceRoll = DiceBag.RollD20()) > 12)
            {
                var damage = DiceBag.RollD20();
                player.Damage(damage);
                DrawUI(monster, player, monster.Name + " rolled: " + diceRoll + " Attack Success!! Dealt " + player.Name + " " + damage + " damage");
            } else
                DrawUI(monster, player, monster.Name + " rolled: " + diceRoll + " Attack Missed. :(");

        }

        private static void AttackMissed(string typeOfAttack, int diceRoll)
        {
            DrawUI(monster, player, player.Name + " rolled: " + diceRoll + " Attack Missed. :(");
        }

        private static void PlayerDealsDamage(int damage, int diceRoll)
        {
            damage = DiceBag.RollD12();
            monster.Damage(damage);
            DrawUI(monster, player, player.Name + " rolled: " + diceRoll + " Attack Success!! Dealt the monster " + damage + " damage");
            statsManager.UpdateDamageRollStat(damage);
        }

        private static void InitializeGame()
        {
            Console.SetWindowSize(120, 35);
            Console.BufferWidth = 120;
            Console.BufferHeight = 35;

            //TODO: Show splash screen - our current intro needs work, generate maui team ascii art

            MusicPlayer.Play(AppDomain.CurrentDomain.BaseDirectory + "\\Forest.wav");
        }

        #region Spawn Entities
        private static void SpawnPlayer()
        {
            Console.Write("Please enter your player name: ");
            player = EntityFactory.Create<Player>(Console.ReadLine(), playerMaxHP);

            // Don't think we need
            player.Spawned += (sender) =>
            {
                //TODO: Can play intro music? 
                //TODO: Can display "blah blah adventurer joins the battle..." 
            };

            // Don't think we need
            player.Damaged += (sender, e) =>
            {
                if (sender.Health <= 0)
                {
                    //TODO: Game over message
                }
            };

            player.Spawn();
        }

        public static Monster SpawnMonster() {
            // TODO: Need Monster Names
            monster = EntityFactory.Create<Monster>("Monster1");

            monster.Spawned += (entity) => {
                MusicPlayer.Play(entity.MusicPath);

                DrawUI(monster, player, "Monster appears!");
            };

            monster.Damaged += (entity, e) => {
                // If Monster is dead, spawn new monster
                if (entity.Health <= 0 ) {
                    statsManager.UpdateScoreStat();                    
                    MusicPlayer.Stop();
                    monster = SpawnMonster();
                }
            };

            monster.Spawn();
            return monster;
        }
        #endregion  

        public static void DrawUI(Monster monster, Player player, string action) {
            {
                // clear screen, draw basic text ui	            
                Console.Clear(); 
                Utils.AlignText("Welcome to The PI Monster Mash!!!", Utils.LineLocation.Center);
                Utils.AlignText(monster.Name, Utils.LineLocation.Center, monster.Health, ConsoleColor.Red);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                var spaces = new string(' ', 30);
                foreach (string line in monster.Art) {
                    {
                        Console.WriteLine(spaces + line);
                    }
                }
                Utils.AlignText(player.Name, Utils.LineLocation.BottomRight, player.Health, ConsoleColor.Green);
                Console.WriteLine();
                Console.WriteLine(action);
                Console.WriteLine();
                Console.WriteLine(ATTACKINSTRUCTIONS);
            }
        }        
    }
}
