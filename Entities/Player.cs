using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMonsterMash.Entities {
    class Player : Entity {
        public override List<string> Art { get {
                var player = new List<string>();
                player.Add("     ,/|\\");
                player.Add("     //&')");
                player.Add("     '')(");
                player.Add("      (( )");
                player.Add("      )( (");
                player.Add("      (=M=[)###########>");
                player.Add("      (( )");
                player.Add("      (( )_");
                player.Add("      ((__,)");
                return player;
            }
        }
    }
}
