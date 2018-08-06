using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SearchTrees.Models;
using SearchTrees.Models.Interfaces;
using SearchTrees.Trees;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            int Arraysize = 20;
            var _array = new int[Arraysize];
            var rand = new Random();
            for (int i = 0; i < Arraysize; i++)
            {
                _array[i] = rand.Next(0, 50);
            }

            BinarySearchTree<int, int> a = new BinarySearchTree<int, int>();

            AvlTree<int, int> b = new AvlTree<int, int>();
            for (int i = 0; i < Arraysize; i++)
            {
                a.Insert(_array[i], 0);
                b.Insert(_array[i], 0);
            }
            
            a.LeftTraversal(n => Console.Write($"{n.Key}; "));
            Console.WriteLine("-----------");
            b.LeftTraversal(n => Console.Write($"{n.Key}; "));

            Console.Read();
        }
    }
}
