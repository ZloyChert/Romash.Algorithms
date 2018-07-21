using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using SearchTrees.Trees;

namespace Benchmarking
{
    [ClrJob(isBaseline: true)]
    [RPlotExporter, RankColumn]
    public class AvlTreeBenchmarking
    {
        private int[] _array;

        [Params(100, 500, 5000, 10000, 30000, 60000, 80000, 130000)]
        public int Arraysize;

        [GlobalSetup]
        public void Setup()
        {
            _array = new int[Arraysize];
            for (int i = 0; i < Arraysize; i++)
            {
                _array[i] = new Random().Next();
            }
        }

        [Benchmark]
        public void IterativeInsert()
        {
            for (int j = 0; j < 15; j++)
            {
                AvlTree<int, int> temp = new AvlTree<int, int>();
                for (int i = 0; i < Arraysize; i++)
                {
                    temp.InsertIterative(_array[i], 0);
                }
            }
           
        }

        [Benchmark]
        public void RecursiveInsert()
        {
            for (int j = 0; j < 15; j++)
            {
                AvlTree<int, int> temp = new AvlTree<int, int>();
                for (int i = 0; i < Arraysize; i++)
                {
                    temp.InsertRecursive(_array[i], 0);
                }
            }
        }

        [Benchmark]
        public void PseudoCleverInsert()
        {
            for (int j = 0; j < 15; j++)
            {
                AvlTree<int, int> temp = new AvlTree<int, int>();
                for (int i = 0; i < Arraysize; i++)
                {
                    temp.Insert(_array[i], 0);
                }
            }
        }

    }
}
