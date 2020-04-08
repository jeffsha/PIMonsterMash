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
        static void Main(string[] args)
        {
            StatsManager statsManager;

            Console.SetWindowSize(80, 25);
            Console.BufferWidth = 80;
            Console.BufferHeight = 25;

            SoundPlayer soundDevice = new SoundPlayer();
            soundDevice.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Forest.wav";
            soundDevice.PlayLooping();

            // Setup Player Name
            Console.WriteLine("Please enter your player name:");
            var playerName = Console.ReadLine();

            var currentPlayer = EntityFactory.Create<Player>(playerName, 25000);

            Console.WriteLine("Please enter your PI Server:");
            var serverName = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Loading Game, Please Wait...");

            statsManager = new StatsManager(playerName, serverName);

            statsManager.IntializePlayerStats();

            var terminationKey = new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false);
            ConsoleKeyInfo currentKey;

            Console.WriteLine("Press A to attack, Press S to Slash, Press F to Firebolt, Press R to Run, X to Leave the game!");

            // Generate Monster Factory
            List<Monster> monsters = new List<Monster>();
            monsters.Add(EntityFactory.Create<Monster>("Monster1"));

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

            // Calculate Player LIfe

            Console.Clear();
            DrawUI(currentMonster, currentPlayer);            
        }

        public static void DrawUI(Monster currentMonster, Player currentPlayer)
        {
            // clear screen, draw basic text ui
            Console.Clear();
            Utils.AlignText("Welcome to The PI Monster Mash!!!", Utils.LineLocation.Center);
            Utils.AlignText("Monster Name", Utils.LineLocation.Center, 50, 50, ConsoleColor.Red);
            foreach(string line in currentMonster.Art)
            {
                Utils.AlignText(line, Utils.LineLocation.Center);
            }
            Utils.AlignText("Player Name", Utils.LineLocation.BottomRight, 25, 25, ConsoleColor.Green);
        }
    }
}
