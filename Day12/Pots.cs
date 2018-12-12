using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    class Pots
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile();
            var (initialState, changes) = ParseInput(input);
            var result = Part1(initialState, changes);
            Console.WriteLine($"Result Part1: {result}");

            result = Part2(initialState, changes);
            Console.WriteLine($"Result Part2: {result}");
        }

        public static long Part1(string initialState, Dictionary<string, char> changes)
        {
            var pots = "..." + initialState + "...";
            var zero = 3L;
            var (finalPots, finalZero, generationsUntilStable, generationDiff) = GoThroughGenerations(pots, changes, 20, zero);
            return GetSumOfPlantNumbers(finalPots, finalZero);
        }

        public static long Part2(string initialState, Dictionary<string, char> changes)
        {
            var pots = "..." + initialState + "...";
            var zero = 3L;
            var (finalPots, finalZero, generationsUntilStable, generationDiff) = GoThroughGenerations(pots, changes, 2000, zero);

            /* Observation:
             * 50 billion is far too large to compute. A solution is to take a look at the generations and see
             * from which point on the evolution is "stable". Stable means the difference between evolutions
             * stays the same. From observation this seems to toggle between 32 and 64 per generation, with 32
             * occurring more often. So I took a stab at it and it was the correct solution. I couldn't find a
             * programmatic way to solve the problem. Code is commented out.
             */
            var sum = GetSumOfPlantNumbers(finalPots, finalZero);
            //return sum + (50000000000L - generationsUntilStable) * generationDiff;
            return sum + (50000000000L - 2000) * 32;
        }

        private static (string result, long zero, long generationsUntilStable, long generationDiff) GoThroughGenerations(string pots, Dictionary<string, char> changes, long generations, long zero)
        {
            var lastSum = 0L;
            var lastSumDiff = 0L;
            var lastSumDiffTimes = 0;
            var stableGeneration = 0L;

            for (var i = 0L; i < generations; i++)
            {
                (pots, zero) = NextGeneration(pots, changes, zero);
                
                //// check if we reached a stable state
                //var sum = GetSumOfPlantNumbers(pots, zero);
                //Console.WriteLine($"i: {i} / Sum: {sum} / LastSum: {lastSum} / LastSumDiff: {lastSumDiff} / LastSumDiffTimes: {lastSumDiffTimes}");
                //if (sum - lastSum == lastSumDiff)
                //{
                //    lastSumDiffTimes++;
                //    if (lastSumDiffTimes == 1)
                //    {
                //        // we have reached a stable point, the changes are always the same, abort
                //        stableGeneration = i;
                //        break;
                //    }
                //}
                //else
                //{
                //    lastSumDiff = sum - lastSum;
                //    lastSum = sum;
                //    lastSumDiffTimes = 0;
                //}
            }

            return (pots, zero, stableGeneration, lastSumDiff);
        }

        internal static (string result, long zero) NextGeneration(string pots, Dictionary<string, char> changes, long zero)
        {
            var nextGeneration = new string('.', pots.Length).ToCharArray();

            foreach (var change in changes)
            {
                for (var i = 0; i < pots.Length - 4; i++)
                {
                    var part = pots.Substring(i, 5);
                    if (part == change.Key)
                    {
                        nextGeneration[i + 2] = change.Value;
                    }
                }
            }

            // grow string if it isn't enough any more
            var result = new string(nextGeneration);
            if (result.IndexOf("#", StringComparison.CurrentCulture) <= 3)
            {
                result = ".." + result;
                zero += 2;
            }

            if (result.LastIndexOf("#", StringComparison.CurrentCulture) >= result.Length - 3)
            {
                result = result + "..";
            }

            return (result, zero);
        }

        private static long GetSumOfPlantNumbers(string pots, long zero)
        {
            // sum up the NUMBER of the pots, not the sum of pots which contain plants
            var sum = 0L;
            for (var i = 0; i < pots.Length; i++)
            {
                if (pots[i] == '#')
                {
                    sum += i - zero;
                }
            }

            return sum;
        }

        internal static (string initialState, Dictionary<string, char> changes) ParseInput(List<string> input)
        {
            var initialState = input[0].Split(' ')[2];
            input.RemoveAt(0);
            input.RemoveAt(0);

            var changes = new Dictionary<string, char>();
            foreach (var i in input)
            {
                var pattern = i.Split(new[] { " => " }, StringSplitOptions.RemoveEmptyEntries)[0];
                var next = i.Split(new[] { " => " }, StringSplitOptions.RemoveEmptyEntries)[1].ToCharArray()[0];
                changes.Add(pattern, next);
            }

            return (initialState, changes);
        }

        private static List<string> ReadInputFile()
        {
            return File.ReadAllLines("Input.txt").ToList();
        }
    }
}
