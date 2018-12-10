using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class NavigationNotWorking
    {
        static void MainNotWorking(string[] args)
        {
            var input = ReadInputFile();
            Part1(input);
        }

        public static void Part1(IEnumerable<string> input)
        {
            var points = input.Select(PointNotWorking.Parse).ToList();
            var maxX = points.Max(p => p.Coordinate.x);
            var maxY = points.Max(p => p.Coordinate.y);
            var minX = points.Min(p => p.Coordinate.x);
            var minY = points.Min(p => p.Coordinate.y);
            var sizeX = Math.Abs(maxX - minX);
            var sizeY = Math.Abs(maxY - minY);
            var fieldSize = Math.Max(sizeX, sizeY);
            var center = fieldSize / 2;
            var step = 0;

            while (true)
            {
                var field = new char[fieldSize, fieldSize];
                foreach (var point in points)
                {
                    var coordinateX = center + point.Coordinate.x + (step * point.Velocity.x);
                    var coordinateY = center + point.Coordinate.y + (step * point.Velocity.y);
                    field[coordinateY, coordinateX] = '#';
                }

                Console.WriteLine($"Step {step}");
                for (var i = 0; i < fieldSize; i++)
                {
                    for (var j = 0; j < fieldSize; j++)
                    {
                        Console.Write($"{field[i, j]} ");
                    }

                    Console.WriteLine();
                }

                step++;

                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                    break;
            }
        }

        private static IEnumerable<string> ReadInputFile()
        {
            return File.ReadAllLines("Input.txt");
        }
    }

    public class PointNotWorking
    {
        public (int x, int y) Coordinate { get; set; }
        public (int x, int y) Velocity { get; set; }

        public static PointNotWorking Parse(string line)
        {
            var input = line.Split(',');
            var cx = int.Parse(input[0].Substring(10));
            var cy = int.Parse(input[1].Split('>')[0]);
            var vx = int.Parse(input[1].Split('<')[1]);
            var vy = int.Parse(input[2].Split('>')[0]);
            var p = new PointNotWorking
            {
                Coordinate = (cx, cy),
                Velocity = (vx, vy)
            };
            return p;
        }
    }
}
