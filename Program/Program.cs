using System;
using System.Collections.Generic;
using SearchTrees.Extensions;
using SearchTrees.Models;
using SearchTrees.Trees;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            AvlTree<int, int> tree = new AvlTree<int, int>();

            for (int i = 0; i < 150; i++)
            {
                tree.Insert(i, 0);
            }
            Console.Clear();
            tree.Delete(5);
            //tree.Delete(4);
            //tree.Delete(3);

            List<int> error = new List<int>();
            void action(Node<int, int> a)
            {
                if (a.LeftChildNode == a.RightChildNode && a.LeftChildNode != null)
                {
                    error.Add(a.Key);
                }

            }
            tree.LeftTraversal(action);
            error.ForEach(n => Console.Write($"{n} "));
            Console.Read();
        }

        public static void TestOne()
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
        }
    }
}
