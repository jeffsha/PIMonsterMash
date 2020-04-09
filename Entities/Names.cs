using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMonsterMash.Entities {
    static class Names {
        static Dictionary<int, string> nameList = new Dictionary<int, string>();
        static Dictionary<int, string> adjList = new Dictionary<int, string>();

        static Names() {
            nameList.Add(0, "Ryan");
            nameList.Add(1, "Rachel");
            nameList.Add(2, "Andrew");
            nameList.Add(3, "Cory");
            nameList.Add(4, "Benjamin");
            nameList.Add(5, "Joe");
            nameList.Add(6, "Jeff");
            nameList.Add(7, "Jim");
            nameList.Add(8, "Stephen");

            adjList.Add(0, "Mighty");
            adjList.Add(1, "Evil");
            adjList.Add(2, "Tired");
            adjList.Add(3, "Angry");
            adjList.Add(4, "Diseased");
            adjList.Add(5, "Brutal");
            adjList.Add(6, "Puny");
            adjList.Add(7, "Magical");
            adjList.Add(8, "Retro");
            adjList.Add(9, "Mythical");
            adjList.Add(10, "Elusive");
            adjList.Add(11, "Trendy");
            adjList.Add(12, "Infectious");
        }

        public static string GetRandom() {
            var rnd = new Random(DateTime.Now.Millisecond);
            return $"{adjList[rnd.Next(0, adjList.Count)]} {nameList[rnd.Next(0, nameList.Count)]}";
        }

    }
}
