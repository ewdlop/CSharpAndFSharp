﻿using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using MyFSharpInterop.Number;
using MyQSharpInterop;
using static System.Diagnostics.Debug;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CSharpConsoleApp
{
    static class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            int productOfTwo = Number.productOfTwo(1, 2);
            Console.WriteLine(string.Format("{0}", productOfTwo));
            int sumOfThree = Number.sumOfThree(1, 2, 3);
            Console.WriteLine(string.Format("{0}", sumOfThree));
            var bits = new[] { false, true, false };
            using var sim = new QuantumSimulator();

            Console.WriteLine($"Input: {bits.ToDelimitedString()}");

            var restored = await RunAlgorithm.Run(sim, new QArray<bool>(bits));
            Console.WriteLine($"Output: {restored.ToDelimitedString()}");

            Assert(bits.Parity() == restored.Parity());
        }

        static bool Parity(this IEnumerable<bool> bitVector) =>
            bitVector.Aggregate(
                (acc, next) => acc ^ next
            );

        static string ToDelimitedString(this IEnumerable<bool> values) =>
            string.Join(", ", values.Select(x => x.ToString()));
    }
}
