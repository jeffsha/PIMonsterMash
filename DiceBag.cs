using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMonsterMash
{
    static class DiceBag
    {
        private static Random rand = new Random();

        public static int RollD4()
        {
            return rand.Next(1, 4);
        }

        public static int RollD6()
        {
            return rand.Next(1, 6);
        }

        public static int RollD8()
        {
            return rand.Next(1, 8);
        }

        public static int RollD10()
        {
            return rand.Next(1, 10);
        }

        public static int RollD12()
        {
            return rand.Next(1, 12);
        }

        public static int RollD20()
        {
            return rand.Next(1, 20);
        }

        public static int RollD100()
        {
            return rand.Next(1, 100);
        }
    }
}
