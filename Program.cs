using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Data;
using OSIsoft.AF.PI;
using Entities;

namespace PIMonsterMash
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Welcome to The PI Monster Mash!!!");
            Console.ReadLine();
            var monster = EntityFactory.Create<Monster>("TestMonster" , 20000);
            Console.WriteLine(monster.Name);
            monster.Draw();
        }
    }
}
