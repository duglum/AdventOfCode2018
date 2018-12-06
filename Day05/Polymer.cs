using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day05
{
    class Polymer
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile();
            var result = Part1(input);
            Console.WriteLine($"Part 1: {result}");

            result = Part2(input);
            Console.WriteLine($"Part 2: {result}");

            var result2 = StartB(input);
            Console.WriteLine($"Part B: {result}");

            Console.WriteLine($"Match: {result.Equals(result2)}");
        }

        public static int Part1(string input)
        {
            while (true)
            {
                var done = true;
                for (var i = 0; i < input.Length - 1;)
                {
                    var a = (int)input[i];
                    var b = (int)input[i + 1];

                    // check distance between ASCII characters
                    if (Math.Abs(a - b) == 32)
                    {
                        input = input.Remove(i, 2);
                        done = false;
                    }
                    else
                    {
                        i++;
                    }
                }

                if (done)
                    break;
            }
            

            return input.Length;
        }

        public static int Part2(string input)
        {
            var shortest = int.MaxValue;
            for (var i = 65; i < 97; i++)
            {
                var currentInput = input.Replace(char.ConvertFromUtf32(i), string.Empty).Replace(char.ConvertFromUtf32(i + 32), string.Empty);
                shortest = Math.Min(Part1(currentInput), shortest);
            }
            return shortest;
        }

        public static int StartB(string input)
        {
            var solutions = new List<int>();
            for (var iChar = 'a'; iChar <= 'z'; ++iChar)
            {
                var newPolymer = string.Concat(input.Where(c => c != iChar && c != char.ToUpper(iChar)));
                solutions.Add(Part1(newPolymer));
            }

            return solutions.Min();
        }

        private static string ReadInputFile()
        {
            return File.ReadAllLines("Input.txt").ToList()[0];
        }
    }
}
