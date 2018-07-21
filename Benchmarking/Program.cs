﻿using System;
using BenchmarkDotNet.Running;

namespace Benchmarking
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<AvlTreeBenchmarking>();
            Console.Read();
        }
    }
}
