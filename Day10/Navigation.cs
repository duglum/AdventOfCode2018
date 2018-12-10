using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class Navigation
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile();
            Part1(input);
        }

        public static void Part1(IEnumerable<string> input)
        {
            var points = input.Select(Point.Parse).ToList();
            var step = 0;

            var boundingBox = (x: 100, y: 50);

            while (true)
            {
                foreach (var point in points)
                {
                    point.Step();
                }

                var maxX = points.Max(p => p.X);
                var minX = points.Min(p => p.X);
                var width = Math.Abs(maxX - minX);
                var maxY = points.Max(p => p.Y);
                var minY = points.Min(p => p.Y);
                var height = Math.Abs(maxY - minY);
                step++;

                if (width < boundingBox.x && height < boundingBox.y)
                {
                    Console.WriteLine($"Step {step}");
                    for (var i = minY; i <= maxY; i++)
                    {
                        for (var j = minX; j <= maxX; j++)
                        {
                            Console.Write(points.Any(p => p.Y == i && p.X == j) ? "#" : ".");
                        }

                        Console.WriteLine();
                    }

                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Enter)
                        break;
                }
                
                if (step % 1000 == 0)
                    Console.WriteLine($"Step {step}");
            }
        }

        private static IEnumerable<string> ReadInputFile()
        {
            return File.ReadAllLines("Input.txt");
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        private int VX { get; set; }
        private int VY { get; set; }
        
        public static Point Parse(string line)
        {
            var input = line.Split(',');
            var p = new Point
            {
                X = int.Parse(input[0].Substring(10)),
                Y = int.Parse(input[1].Split('>')[0]),
                VX = int.Parse(input[1].Split('<')[1]),
                VY = int.Parse(input[2].Split('>')[0])
            };
            return p;
        }

        public void Step()
        {
            X += VX;
            Y += VY;
        }
    }
}
