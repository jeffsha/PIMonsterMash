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

namespace PIMonsterMash
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 25);
            Console.BufferWidth = 80;
            Console.BufferHeight = 25;
            DrawUI();
            Console.ReadLine();
        }

        public static void DrawUI()
        {
            // clear screen, draw basic text ui
            Console.Clear();
            Utils.AlignText("Welcome to The PI Monster Mash!!!", Utils.LineLocation.Center);
            Utils.AlignText("Monster Name", Utils.LineLocation.Center, 50, 50, ConsoleColor.Red);
            Utils.AlignText("Player Name", Utils.LineLocation.BottomRight, 25, 25, ConsoleColor.Green);
        }
    }
}
