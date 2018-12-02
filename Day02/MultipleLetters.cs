using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day02
{
    class MultipleLetters
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile();
            var checksum = Checksum(input);
            Console.WriteLine($"Checksum: {checksum}");

            var match = Part2(input);
            Console.WriteLine($"Match: {match}");
        }

        public static int Checksum(IEnumerable<string> inputList)
        {
            var twice = inputList.Count(input => input.GroupBy(c => c).Any(c => c.Count() == 2));
            var thrice = inputList.Count(input => input.GroupBy(c => c).Any(c => c.Count() == 3));
            return twice * thrice;
        }

        public static string Part2(IEnumerable<string> inputList)
        {
            foreach (var input in inputList)
            {
                CompareStrings(input, inputList, out var match);
                if (!string.IsNullOrEmpty(match))
                {
                    return RemoveDifferingCharacter(input, match);
                }
            }
            return string.Empty;
        }

        private static IEnumerable<string> ReadInputFile()
        {
            return File.ReadAllLines("Input.txt").ToList();
        }

        private static void CompareStrings(string input, IEnumerable<string> inputList, out string match)
        {
            foreach (var input2 in inputList)
            {
                var diff = input.Where((i1, i2) => i1 != input2[i2]).Count();
                if (diff == 1)
                {
                    match = input2;
                    return;
                }
            }

            match = string.Empty;
        }

        private static string RemoveDifferingCharacter(string input, string match)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < input.Length; i++)
            {
                if (input[i] == match[i])
                {
                    sb.Append(input[i]);
                }
            }
            return sb.ToString();
        }
    }
}
