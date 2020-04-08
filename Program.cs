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
            //soundDevice.PlayLooping();            

            // Setup Player Name
            Console.WriteLine("Please enter your player name:");
            var playerName = Console.ReadLine();

            var player = EntityFactory.Create<Player>(playerName, 25000);

            Console.WriteLine("Please enter your PI Server:");
            var serverName = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Loading Game, Please Wait...");

            statsManager = new StatsManager(playerName, serverName);

            statsManager.IntializePlayerStats();

            var terminationKey = new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false);
            var currentKey = new ConsoleKeyInfo();

            Console.WriteLine("Press H to attack, X to Leave the game!");

            // Generate Monster Factory
            List<Monster> monsters = new List<Monster>();
            monsters.Add(EntityFactory.Create<Monster>("Monster1"));

            while (!(currentKey = Console.ReadKey()).Equals(terminationKey))
            {
                foreach (Monster monster in monsters)
                {
                    // While Monster isAlive

                    // DO X, DO Y - Attack Monster
                    // Roll Damage
                    var attackRoll = DiceBag.RollD20();
                    statsManager.UpdateAttackRollStat(attackRoll);

                    // Monster Spawn - Factory?
                    soundDevice.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Forest.wav";
                    //soundDevice.PlayLooping();

                    // Updating UI
                    // First Draw - ASCII Monster Art Monster Health Player Health
                    // LOOPED
                    // Attack of PLayer - Monster Health Updates Redraw
                    // Action Text -> Player Does X Damage, Instructions hit x to do damage
                    // Attack of Monster - Player Health Updates Redraw                
                    // Action Text -> Monster Does X Damage
                    Console.Clear();
                    DrawUI(monster, player);

                    // Monster Attack Player
                    // Reset

                    // Calculate Monster Death
                    // Calculate Player Death
                }
            }
        }

        public static void DrawUI(Monster m, Player p)
        {
            // clear screen, draw basic text ui
            Console.Clear();
            Utils.AlignText("Welcome to The PI Monster Mash!!!", Utils.LineLocation.Center);
            Utils.AlignText("Monster Name", Utils.LineLocation.Center, 50, 50, ConsoleColor.Red);
            foreach(string line in m.Art)
            {
                Utils.AlignText(line, Utils.LineLocation.Center);
            }
            Utils.AlignText("Player Name", Utils.LineLocation.BottomRight, 25, 25, ConsoleColor.Green);
        }
    }
}
