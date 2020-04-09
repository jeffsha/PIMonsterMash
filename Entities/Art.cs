using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMonsterMash
{
    static class Art
    {
        static Dictionary<int, List<string>> artList = new Dictionary<int, List<string>>();
        public static List<string> splashScreenList = new List<string>()
        {
            @"                        __      __         .__                                         __                                       ",
            @"                       /  \    /  \  ____  |  |    ____   ____    _____    ____      _/  |_  ____                               ",
            @"                       \   \/\/   /_/ __ \ |  |  _/ ___\ /  _ \  /     \ _/ __ \     \   __\/  _ \                              ",
            @"                        \        / \  ___/ |  |__\  \___(  <_> )|  Y Y  \\  ___/      |  | (  <_> )                             ",
            @"                         \__/\  /   \___  >|____/ \___  >\____/ |__|_|  / \___  >     |__|  \____/                              ",
            @"                              \/        \/            \/              \/      \/                                                ",
            @"                                                                                                                                ",
            @"  __________ .___        _____                             __                         _____                   .__         ._.._.",
            @"  \______   \|   |      /     \    ____    ____    _______/  |_   ____ _______       /     \  _____     ______|  |__      | || |",
            @"   |     ___/|   |     /  \ /  \  /  _ \  /    \  /  ___/\   __\_/ __ \\_  __ \     /  \ /  \ \__  \   /  ___/|  |  \     | || |",
            @"   |    |    |   |    /    Y    \(  <_> )|   |  \ \___ \  |  |  \  ___/ |  | \/    /    Y    \ / __ \_ \___ \ |   Y  \     \| \|",
            @"   |____|    |___|    \____|__  / \____/ |___|  //____  > |__|   \___  >|__|       \____|__  /(____  //____  >|___|  /     __ __",
            @"                              \/              \/      \/             \/                    \/      \/      \/      \/      \/ \/",
        };

        static Art()
        {
            var goblin = new List<string>();
            goblin.Add("             ,      ,");
            goblin.Add("            /(.-\"\"-.)\\");
            goblin.Add("        |\\  \\/      \\/  /|");
            goblin.Add("        | \\ / =.  .= \\ / |");
            goblin.Add("        \\( \\   o\\/o   / )/");
            goblin.Add("         \\_, '-/  \\-' ,_/ ");
            goblin.Add("           /   \\__/   \\");
            goblin.Add("           \\ \\__/\\__/ /");
            goblin.Add("         ___\\ \\|--|/ /___");
            goblin.Add("       /`    \\      /    `\\");
            goblin.Add("      /       '----'       \\");

            artList.Add(0, goblin);

            var wizard = new List<string>();
            wizard.Add("                    ____");
            wizard.Add("                  .'* *.'");
            wizard.Add("               __/_*_*(_");
            wizard.Add("              / _______ \\");
            wizard.Add("             _\\_)/___\\(_/_");
            wizard.Add("            / _((\\- -/))_ \\");
            wizard.Add("            \\ \\())(-)(()/ /");
            wizard.Add("             ' \\(((()))/ '");
            wizard.Add("            / ' \\)).))/ ' \\");
            wizard.Add("         (   ( .;''';. .'  )");
            wizard.Add("          _\\\"__ /    )\\ __\" / _");
            wizard.Add("            \\/  \\   ' /  \\/");
            wizard.Add("             .'  '...' ' )");
            wizard.Add("              / /  |  \\ \\");
            wizard.Add("             / .   .   . \\");
            wizard.Add("            /   .     .   \\");
            wizard.Add("           /   /   |   \\   \\");
            wizard.Add("         .'   /    b    '.  '.");
            wizard.Add("     _.-'    /     Bb     '-. '-._");
            wizard.Add(" _.-'       |      BBb       '-.  '-.");
            wizard.Add("(________mrf\\____.dBBBb.________)____)");

            artList.Add(1, wizard);
        }

        public static List<string> GetRandom()
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            return artList[rnd.Next(0, artList.Count)];
        }
    }
}
