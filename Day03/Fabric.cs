using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03
{
    class Fabric
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile();
            MultipleClaims(input, out var multipleClaims, out var wholeId);
            Console.WriteLine($"Multiple claims: {multipleClaims}");
            Console.WriteLine($"Whole ID: {wholeId}");
        }

        public static void MultipleClaims(IEnumerable<string> inputList, out int multiple, out int wholeId)
        {
            var array = FillFabric(inputList);
            multiple = GetMultipleClaims(array);
            wholeId = GetIdWithoutOverlap(array, inputList);
        }

        internal static int[,] FillFabric(IEnumerable<string> inputList)
        {
            var array = new int[1000, 1000];
            foreach (var input in inputList)
            {
                ParseInput(input, out var id, out var left, out var top, out var width, out var height);
                for (var i = left; i < left + width; i++)
                {
                    for (var j = top; j < top + height; j++)
                    {
                        array[i, j]++;
                    }
                }
            }

            return array;
        }

        internal static int GetMultipleClaims(int[,] fabric)
        {
            var multiple = 0;
            for (var i = 0; i < 1000; i++)
            {
                for (var j = 0; j < 1000; j++)
                {
                    if (fabric[i, j] > 1)
                    {
                        multiple++;
                    }
                }
            }
            return multiple;
        }

        internal static int GetIdWithoutOverlap(int[,] fabric, IEnumerable<string> inputList)
        {
            var wholeId = -1;
            foreach (var input in inputList)
            {
                var whole = true;
                ParseInput(input, out var id, out var left, out var top, out var width, out var height);
                for (var i = left; i < left + width; i++)
                {
                    for (var j = top; j < top + height; j++)
                    {
                        if (fabric[i, j] != 1)
                        {
                            whole = false;
                        }
                    }
                }

                if (whole)
                {
                    wholeId = id;
                }
            }

            return wholeId;
        }

        internal static void ParseInput(string input, out int id, out int left, out int top, out int width, out int height)
        {
            id = int.Parse(input.Split('@')[0].Trim().Substring(1));
            var coordinates = input.Split('@')[1].Split(':')[0].Trim().Split(',');
            left = int.Parse(coordinates[0]);
            top = int.Parse(coordinates[1]);
            var rectangle = input.Split('@')[1].Split(':')[1].Trim().Split('x');
            width = int.Parse(rectangle[0]);
            height = int.Parse(rectangle[1]);
        }

        private static IEnumerable<string> ReadInputFile()
        {
            return File.ReadAllLines("Input.txt").ToList();
        }
    }
}
