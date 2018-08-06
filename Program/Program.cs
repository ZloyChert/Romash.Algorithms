using System;
using System.Collections.Generic;
using SearchTrees.Extensions;
using SearchTrees.Trees;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            int Arraysize = 50;
            var _array = new int[Arraysize];
            var rand = new Random();
            for (int i = 0; i < Arraysize; i++)
            {
                _array[i] = i;
            }

            BinarySearchTree<int, int> a = new BinarySearchTree<int, int>();

            RedBlackTree<int, int> b = new RedBlackTree<int, int>();
            AvlTree<int, int> c = new AvlTree<int, int>();
            for (int i = 0; i < Arraysize; i++)
            {
                a.Insert(_array[i], 0);
                b.Insert(_array[i], 0);
                c.Insert(_array[i], 0);
            }
            
            a.LeftTraversal(n => Console.Write($"{n.Key}; "));
            Console.WriteLine("-----------");
            a.LeftTraversal(n => Console.Write($"{n.GetHeight()}; "));
            Console.WriteLine("-----------");
            b.LeftTraversal(n => Console.Write($"{n.Key}; "));
            Console.WriteLine("-----------");
            b.LeftTraversal(n => Console.Write($"{n.GetHeight()}; "));
            Console.WriteLine("-----------");
            c.LeftTraversal(n => Console.Write($"{n.Key}; "));
            Console.WriteLine("-----------");
            c.LeftTraversal(n => Console.Write($"{n.GetHeight()}; "));

            Console.Read();
        }
    }
}
