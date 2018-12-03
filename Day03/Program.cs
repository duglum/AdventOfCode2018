using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile();
            var multipleClaims = MultipleClaims(input);
            Console.WriteLine($"Multiple claims: {multipleClaims}");



        }

        public static int MultipleClaims(IEnumerable<string> inputList)
        {
            var array = new int[2000, 2000];
            foreach (var input in inputList)
            {
                ParseInput(input, out var id, out var left, out var top, out var width, out var height);
                Console.WriteLine($"{left}, {top}, {width}, {height}");
                for (var i = left; i < left + width; i++)
                {
                    for (var j = top; j < top + height; j++)
                    {
                        array[i, j]++;
                    }
                }
            }
            
            var multiple = 0;
            for (var i = 0; i < 2000; i++)
            {
                for (var j = 0; j < 2000; j++)
                {
                    if (array[i, j] > 1)
                    {
                        multiple++;
                    }
                }
            }

            return multiple;
        }

        private static void ParseInput(string input, out int id, out int left, out int top, out int width, out int height)
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
