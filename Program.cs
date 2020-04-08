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
            Console.WriteLine("Welcome to The PI Monster Mash!!!\n");

            //Console.WriteLine();
            Console.WriteLine("Ye find ye-self in yon Security Guild\n");

            Console.WriteLine("Thy Dice Bag API\n");

            Console.WriteLine("Roll to hit!!!");
            Console.WriteLine(DiceBag.RollD20());

            Console.WriteLine("Roll for damage!!!");
            Console.WriteLine(DiceBag.RollD6());

            Console.WriteLine("Roll for your chance to succeed on _____!!!");
            Console.WriteLine(DiceBag.RollD100());
        }
    }
}
