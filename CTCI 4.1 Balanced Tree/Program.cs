using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI_4._1_Balanced_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeaderMsg(4, 1, "Check if Binary Tree is Balanced");

            


            Console.ReadLine();
        }


        private static void PrintHeaderMsg(int chapter, int problem, string title)
        {
            Console.WriteLine("Cracking the Coding Interview");
            Console.WriteLine("Chapter " + chapter + ", Problem " + chapter + "." + problem + ": " + title);
            Console.WriteLine();
        }
    }
}
