using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMonsterMash {
    static class Art {
        static Dictionary<int, List<string>> artList = new Dictionary<int, List<string>>();

        static Art() {
            var monsters = new List<string>();
            monsters.Add("             ,      ,");
            monsters.Add("            /(.-\"\"-.)\\");
            monsters.Add("        |\\  \\/      \\/  /|");
            monsters.Add("        | \\ / =.  .= \\ / |");
            monsters.Add("        \\( \\   o\\/o   / )/");
            monsters.Add("         \\_, '-/  \\-' ,_/ ");
            monsters.Add("           /   \\__/   \\");
            monsters.Add("           \\ \\__/\\__/ /");
            monsters.Add("         ___\\ \\|--|/ /___");
            monsters.Add("       /`    \\      /    `\\");
            monsters.Add("      /       '----'       \\");

            artList.Add(0, monsters.ToList());
            monsters.Clear();

            monsters.Add("                    ____");
            monsters.Add("                  .'* *.'");
            monsters.Add("               __/_*_*(_");
            monsters.Add("              / _______ \\");
            monsters.Add("             _\\_)/___\\(_/_");
            monsters.Add("            / _((\\- -/))_ \\");
            monsters.Add("            \\ \\())(-)(()/ /");
            monsters.Add("             ' \\(((()))/ '");
            monsters.Add("            / ' \\)).))/ ' \\");
            monsters.Add("         (   ( .;''';. .'  )");
            monsters.Add("          _\\\"__ /    )\\ __\" / _");
            monsters.Add("            \\/  \\   ' /  \\/");
            monsters.Add("             .'  '...' ' )");
            monsters.Add("              / /  |  \\ \\");
            monsters.Add("             / .   .   . \\");
            monsters.Add("            /   .     .   \\");
            monsters.Add("           /   /   |   \\   \\");
            monsters.Add("         .'   /    b    '.  '.");
            monsters.Add("     _.-'    /     Bb     '-. '-._");
            monsters.Add(" _.-'       |      BBb       '-.  '-.");
            monsters.Add("(________mrf\\____.dBBBb.________)____)");

            artList.Add(1, monsters.ToList());
            monsters.Clear();

            monsters.Add("        .-\"\"\"\".");
            monsters.Add("       /       \\");
            monsters.Add("   __ /   .-.  .\\");
            monsters.Add("  /  `\\  /   \\/  \\");
            monsters.Add("  | _ \\/   .==.==.");
            monsters.Add("  | (   \\  / ____\\__\\");
            monsters.Add("   \\ \\      (_()(_()");
            monsters.Add("    \\ \\            '---._");
            monsters.Add("     \\                   \\_");
            monsters.Add("  /\\ |`       (__)________ /");
            monsters.Add(" /  \\|     /\\___ /");
            monsters.Add("|    \\     \\|| VV");
            monsters.Add("|     \\     \\| \"\"\"\",");
            monsters.Add("|      \\     ______)");
            monsters.Add("\\       \\  /`");
            monsters.Add("         \\(");

            artList.Add(2, monsters.ToList());
            monsters.Clear();

            monsters.Add("          _/(               <~\\  /~>               )\\_                 ");
            monsters.Add("        .~   ~-.            /^-~~-^\\            .-~   ~.               ");
            monsters.Add("     .-~        ~-._       : /~\\/~\\ :       _.-~        ~-.            ");
            monsters.Add("  .-~               ~~--.__: \0/\0/ ;__,--~~               ~-.         ");
            monsters.Add(" /                        ./\\. ^^ ./\\.                        \\        ");
            monsters.Add(".                         |  ( )( )  |                         .       ");
            monsters.Add("-~~--.        _.---._    /~   U`'U   ~\\    _.---._        .--~~-       ");
            monsters.Add("      ~-. .--~       ~~-|              |-~~       ~--. .-~             ");
            monsters.Add("         ~              |  :        :  |_             ~                ");
            monsters.Add("                        `\\,'  :  :  `./' ~~--._                        ");
            monsters.Add("                       .(<___.'  `,___>),--.___~~-.                    ");
            monsters.Add("                       ~ (((( ~--~ ))))      _.~  _)                   ");
            monsters.Add("                          ~~~      ~~~/`.--~ _.--~                     ");
            monsters.Add("                                      \\,~~~~~                          ");

            artList.Add(3, monsters.ToList());
            monsters.Clear();

            monsters.Add("       .^----^.");
            monsters.Add("      (= o  O =)");
            monsters.Add("       (___V__)");
            monsters.Add("        _|==|_");
            monsters.Add("   ___/' |--| |");
            monsters.Add("  / ,._| |  | '");
            monsters.Add(" | \\__ |__}-|__}");
            monsters.Add("  \\___)`");

            artList.Add(4, monsters.ToList());
            monsters.Clear();

            monsters.Add("                        _,--~~~,");
            monsters.Add("                       .'        `.");
            monsters.Add("                       |           ;");
            monsters.Add("                       |           :");
            monsters.Add("                      /_,-==/     .'");
            monsters.Add("                    /'`\\*  ;      :");
            monsters.Add("                  :'    `-        :");
            monsters.Add("                  `~*,'     .     :");
            monsters.Add("                     :__.,._  `;  :");
            monsters.Add("                     `\\'    )  '  `,");
            monsters.Add("                         \\-/  '     )");
            monsters.Add("                         :'          \\ _");
            monsters.Add("                          `~---,-~    `,)");
            monsters.Add("          ___                   \\     /~`\\");
            monsters.Add("    \\---__ `;~~~-------------~~~(| _-'    `,");
            monsters.Add(" ---, ' \\`-._____     _______.---'         \\");
            monsters.Add(" \\--- `~~-`,      ~~~~~~                     `,");
            monsters.Add("\\----      )                                   \\");
            monsters.Add("\\----.  __ /                                    `-");
            monsters.Add(" \\----'` -~____");
            monsters.Add("               ~~~~~--------,.___");
            monsters.Add("                                 ```\\_");

            artList.Add(5, monsters.ToList());
            monsters.Clear();

        }

        public static List<string> GetRandom() {
            var rnd = new Random(DateTime.Now.Millisecond);
            return artList[rnd.Next(0, artList.Count)];
        }

    }
}
