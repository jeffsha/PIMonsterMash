using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Data;
using OSIsoft.AF.PI;

namespace PIMonsterMash
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 25);
            Console.BufferWidth = 80;
            Console.BufferHeight = 25;
            CenterText("Welcome to The PI Monster Mash!!!");
            DrawUI();
            Console.ReadLine();
        }

        /// <summary>
        /// HP of Monster top center
        ///        Monster
        ///        Hp of player bottom right
        /// Instructions: SOME TEXT
        /// TODO: Move to a utils file
        /// </summary>
        public static void DrawUI()
        {
            // clear screen, draw basic text ui
            //Console.Clear();
            CenterText("Monster 1", 50, 50, ConsoleColor.Red);
            BottomRightText("Player Name", 25, 25, ConsoleColor.Green);
        }

        public static void BottomRightText(string s, int hpCurrent = -1, int hpMax = -1, ConsoleColor textColor = ConsoleColor.White)
        {
            Console.ForegroundColor = textColor;
            if (hpCurrent != -1 && hpMax != -1)
            {
                s = $"{s} HP: {hpCurrent}/{hpMax}";
            }

            Console.SetCursorPosition((Console.WindowWidth - s.Trim().Length), Console.BufferHeight - 5);
            Console.WriteLine(s);
            Console.ResetColor();
        }

        public static void CenterText(string s, int hpCurrent = -1, int hpMax = -1, ConsoleColor textColor = ConsoleColor.White)
        {
            Console.ForegroundColor = textColor;
            Console.WriteLine();

            if (hpCurrent != -1 && hpMax != -1)
            {
                s = $"{s} HP: {hpCurrent}/{hpMax}";
            }

            Console.SetCursorPosition((Console.WindowWidth - s.Trim().Length) / 2, Console.CursorTop);
            Console.WriteLine(s.Trim());
            Console.ResetColor();
        }
    }
}
