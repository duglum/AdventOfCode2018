using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day07
{
    class Order
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile();
            var result = Part1(input);
            Console.WriteLine($"Part 1: {result}");

            var result2 = Part2(input);
            Console.WriteLine($"Part 2: {result2}");
        }

        public static string Part1(IEnumerable<string> input)
        {
            var (dependencies, all) = GetDependenciesAndSteps(input);

            var result = string.Empty;
            while (all.Any())
            {
                var dependenciesResolved = all.First(s => dependencies.All(d => d.step != s));
                result += dependenciesResolved;
                all.Remove(dependenciesResolved);
                dependencies.RemoveAll(d => d.condition == dependenciesResolved);
            }

            return result;
        }

        public static int Part2(IEnumerable<string> input)
        {
            var (dependencies, all) = GetDependenciesAndSteps(input);
            var elves = new List<int>(5) {0, 0, 0, 0, 0};
            var second = 0;
            var done = new List<(char step, int finish)>();

            while (all.Any() || elves.Any(e => e > second))
            {
                done.Where(d => d.finish <= second).ToList().ForEach(x => dependencies.RemoveAll(d => d.condition == x.step));
                done.RemoveAll(d => d.finish <= second);

                var valid = all.Where(s => !dependencies.Any(d => d.step == s)).ToList();

                for (var e = 0; e < elves.Count && valid.Any(); e++)
                {
                    if (elves[e] <= second)
                    {
                        elves[e] = GetWorkTime(valid.First()) + second;
                        all.Remove(valid.First());
                        done.Add((valid.First(), elves[e]));
                        valid.RemoveAt(0);
                    }
                }

                second++;
            }
            return second;
        }

        private static int GetWorkTime(char v)
        {
            return (v - 'A') + 61;
        }

        private static (List<(char condition, char step)> dependencies, List<char> steps) GetDependenciesAndSteps(IEnumerable<string> input)
        {
            var dependencies = new List<(char condition, char step)>();
            foreach (var line in input)
            {
                dependencies.Add((line[5], line[36]));
            }

            var steps = dependencies.Select(d => d.step);
            var conditions = dependencies.Select(d => d.condition);
            var all = steps.Concat(conditions).Distinct().OrderBy(x => x).ToList();

            return (dependencies, all);
        }

        private static IEnumerable<string> ReadInputFile()
        {
            return File.ReadAllLines("Input.txt").ToList();
        }
    }
}
