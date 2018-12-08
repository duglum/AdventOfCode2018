using System;
using System.IO;
using System.Linq;

namespace Day08
{
    class Memory
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile();
            var result = Part1(input);
            Console.WriteLine($"Part 1: {result}");

            result = Part2(input);
            Console.WriteLine($"Part 1: {result}");
        }

        public static int Part1(string input)
        {
            var info = input.Split(' ').ToList();
            var node = new Node(info);
            return node.GetSumMetaDataEntries();
        }

        public static int Part2(string input)
        {
            var info = input.Split(' ').ToList();
            var node = new Node(info);
            return node.GetValue();
        }

        private static string ReadInputFile()
        {
            return File.ReadAllLines("Input.txt")[0];
        }
    }
}