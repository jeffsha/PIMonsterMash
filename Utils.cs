﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMonsterMash
{
    static class Utils
    {
        public enum LineLocation
        {
            Center,
            BottomRight,
            BottomLeft,
        }

        public static void AlignText(string s, LineLocation lineLocation, int hpCurrent = -1, int hpMax = -1, int buffer = 0, ConsoleColor textColor = ConsoleColor.White)
        {
            Console.ForegroundColor = textColor;
            s = (hpCurrent != -1 && hpMax != -1) ? $"{s} HP: {hpCurrent}/{hpMax}" : s;
            if (hpCurrent != -1)
            {
                Console.WriteLine();
            }
            int cursorLeft = (Console.WindowWidth - s.TrimEnd().Length);
            int cursorTop = 0;
            switch (lineLocation)
            {
                case LineLocation.BottomLeft:
                    cursorLeft = 0;
                    cursorTop = Console.BufferHeight - buffer - 5;
                    break;
                case LineLocation.BottomRight:
                    cursorTop = Console.BufferHeight - buffer - 5;
                    break;
                case LineLocation.Center:
                    cursorLeft /= 2;
                    cursorTop = Console.CursorTop;
                    break;
                default:
                    break;
            }
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.WriteLine(s);
            Console.ResetColor();
        }
    }
}
