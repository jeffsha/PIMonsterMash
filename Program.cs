using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using OSIsoft.AF;
using OSIsoft.AF.PI;
using PIMonsterMash.Entities;

namespace PIMonsterMash
{
    class Program
    {
        const int startingHP = 100;
        const string ATTACKINSTRUCTIONS = "Press A to Attack, S to Slash, F to Firebolt, R to Run, M to toggle Music, or X to eXit!";

        static Monster monster;
        static Player player;
        static StatsManager statsManager;
        static List<string> messages = new List<string>();

        static void Main(string[] args)
        {
            SoundPlayer soundDevice = new SoundPlayer();
            soundDevice.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Forest.wav";
            soundDevice.PlayLooping();

            //Required Game Initialization
            InitializeGame();

            // Show the splash screen
            ShowSplashScreen();

            //Creates new player object and assigned event handlers
            SpawnPlayer();

            Console.Write("Please enter your PI Server: ");
            var serverName = Console.ReadLine();

            Console.WriteLine("Loading Game, Please Wait...");

            statsManager = new StatsManager(player.Name, serverName);
            statsManager.IntializePlayerStats();

            var terminationKey = new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false);
            ConsoleKeyInfo currentKey;

            // Spawn first monster
            monster = SpawnMonster();

            messages.Add(ATTACKINSTRUCTIONS);
            DrawUI();

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
                            messages.Add($"{player.Name} successfully runs away from {monster.Name}");
                            messages.Add(ATTACKINSTRUCTIONS);
                            monster = SpawnMonster();
                        }
                        else
                        {
                            messages.Add($"Failed Running from the {monster.Name}!!");
                            DrawUI();
                            MonsterAttemptsDamage();
                        }
                        break;
                    // Mute/unmute Music
                    case ConsoleKey.M:
                        MusicPlayer.ToggleMusic();
                        messages.Add(ATTACKINSTRUCTIONS);
                        DrawUI();
                        break;
                    // Bad Input
                    default:
                        messages.Add("Invalid Command, Try Again!");
                        messages.Add(ATTACKINSTRUCTIONS);
                        DrawUI();
                        break;
                }

                if (player.Health <= 0)
                    break;
            }

            Console.Clear();
            
            DisplayScoreboard(statsManager);
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
                messages.Add($"{monster.Name} rolled: {diceRoll}");
                messages.Add($"Attack Success!! {monster.Name} Dealt {player.Name} {damage} damage");
            }
            else
            {
                messages.Add($"{monster.Name} rolled: {diceRoll}");
                messages.Add("Attack Missed. :(");
            }

            messages.Add(ATTACKINSTRUCTIONS);

            DrawUI();
        }

        private static void AttackMissed(string typeOfAttack, int diceRoll)
        {
            messages.Add($"{player.Name} rolled: {diceRoll}");
            messages.Add("Attack Missed. :(");
            DrawUI();
        }

        private static void PlayerDealsDamage(int damage, int diceRoll)
        {
            damage = DiceBag.RollD12();
            monster.Damage(damage);
            messages.Add($"{player.Name} rolled: {diceRoll}");
            messages.Add($"Attack Success!! {player.Name} Dealt {monster.Name} {damage} damage");
            DrawUI();
            statsManager.UpdateAttackRollStat(diceRoll);
            statsManager.UpdateDamageRollStat(damage);
        }

        private static void InitializeGame()
        {
            Console.SetWindowSize(130, 35);
            Console.BufferWidth = 130;
            Console.BufferHeight = 35;
        }

        private static void ShowSplashScreen()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var line in Art.splashScreenList)
            {
                Console.WriteLine(line);
            }
            Console.ResetColor();
            Utils.AlignText("Press any key to continue...", Utils.LineLocation.BottomLeft);
            Console.ReadKey();
            Console.Clear();
        }

        #region Spawn Entities
        private static void SpawnPlayer()
        {
            Console.Write("Please enter your player name: ");
            player = EntityFactory.Create<Player>(Console.ReadLine(), startingHP);

            player.Spawned += (entity) =>
            {
                messages.Add($"{entity.Name} has joined the battle");
            };

            player.Damaged += (entity, e) => {};

            player.Spawn();
        }

        public static Monster SpawnMonster()
        {
            monster = EntityFactory.Create<Monster>();

            monster.Spawned += (entity) =>
            {
                MusicPlayer.Play(entity.MusicPath);
                messages.Add($"{entity.Name} has joined the battle");
                DrawUI();
            };

            monster.Damaged += (entity, e) =>
            {
                // If Monster is dead, spawn new monster
                if (entity.Health <= 0)
                {
                    statsManager.UpdateScoreStat();
                    MusicPlayer.Stop();
                    monster = SpawnMonster();
                }
            };

            monster.Spawn();
            return monster;
        }
        #endregion  

        public static void DrawUI()
        {
            {
                // clear screen, draw basic text ui
                Console.Clear();
                Utils.AlignText("Welcome to The PI Monster Mash!!!", Utils.LineLocation.Center);
                Utils.AlignText(monster.Name, Utils.LineLocation.Center, monster.Health, monster.MaxHealth, textColor: ConsoleColor.Red);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                var spaces = new string(' ', 30);
                foreach (string line in monster.Art)
                {
                    {
                        Console.WriteLine(spaces + line);
                    }
                }
                Utils.AlignText(player.Name, Utils.LineLocation.BottomRight, player.Health, player.MaxHealth, messages.Count, ConsoleColor.Green);
                Console.WriteLine();
                foreach (string message in messages)
                {
                    Console.WriteLine(message);
                }
                messages.Clear();
                // todo: prompt for hit roll?
                // todo: prompt for damage roll?
            }
        }

        private static void DisplayScoreboard(StatsManager statsManager)
        {
            var latestScores = statsManager.GetScoreStats();

            SoundPlayer soundDevice = new SoundPlayer();
            soundDevice.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\GameOver.wav";
            soundDevice.Play();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
@"
  ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▒█████   ██▒   █▓▓█████  ██▀███  
 ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒██▒  ██▒▓██░   █▒▓█   ▀ ▓██ ▒ ██▒
▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██░  ██▒ ▓██  █▒░▒███   ▓██ ░▄█ ▒
░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ▒██   ██░  ▒██ █░░▒▓█  ▄ ▒██▀▀█▄  
░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒   ░ ████▓▒░   ▒▀█░  ░▒████▒░██▓ ▒██▒
 ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░   ░ ▒░▒░▒░    ░ ▐░  ░░ ▒░ ░░ ▒▓ ░▒▓░
  ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ░ ▒ ▒░    ░ ░░   ░ ░  ░  ░▒ ░ ▒░
░ ░   ░   ░   ▒   ░      ░      ░      ░ ░ ░ ▒       ░░     ░     ░░   ░ 
      ░       ░  ░       ░      ░  ░       ░ ░        ░     ░  ░   ░     
                                                     ░                   
"
            );
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("Presseth a key to seeeth thy scores.");
            Console.ReadKey();

            soundDevice.Stop();
            soundDevice.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Score.wav";
            soundDevice.PlayLooping();
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(
@"
███████╗ ██████╗ ██████╗ ██████╗ ███████╗███████╗
██╔════╝██╔════╝██╔═══██╗██╔══██╗██╔════╝██╔════╝
███████╗██║     ██║   ██║██████╔╝█████╗  ███████╗
╚════██║██║     ██║   ██║██╔══██╗██╔══╝  ╚════██║
███████║╚██████╗╚██████╔╝██║  ██║███████╗███████║
╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚══════╝╚══════╝
"
            );
            Console.ResetColor();

            var scoresList = latestScores.ToList().OrderByDescending(score => score.Value);
            Console.WriteLine($"Vegeta : Over 9000");
            Console.WriteLine($"Ghandi : 1337");
            foreach (var score in scoresList)
            {
                Console.WriteLine($"{score.Key} : {score.Value}");
            }
            Console.WriteLine($"Barb : -10");

            Console.WriteLine();
            Console.WriteLine("All your base are belong to us.");

            Console.WriteLine();
            Console.WriteLine("Presseth a key to leave thy scores.");
            Console.ReadKey();
        }
    }
}
