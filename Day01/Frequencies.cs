using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    class Frequencies
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile();
            var sum = AddUp(input);
            Console.WriteLine($"Sum: {sum}");
            
            var reachedTwice = ReachedTwice(input);
            Console.WriteLine($"Reached twice: {reachedTwice}");
        }

        public static int AddUp(IEnumerable<string> input)
        {
            return input.Sum(int.Parse);
        }

        private static IEnumerable<string> ReadInputFile()
        {
            return File.ReadAllLines("Input.txt").ToList();
        }

        public static int ReachedTwice(IEnumerable<string> input)
        {
            var reached = new HashSet<int>();
            var sum = 0;
            while (true)
            {
                foreach (var value in input)
                {
                    sum += int.Parse(value);
                    if (!reached.Add(sum))
                    {
                        return sum;
                    }
                }
            }
        }
    }
}
