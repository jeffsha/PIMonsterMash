using System;

namespace Entities {
    class Monster : Entity {
        public override char[] Draw() {
            Console.WriteLine("Rawr");
            return null;
        }
    }
}