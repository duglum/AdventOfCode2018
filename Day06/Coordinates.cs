using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
    class Coordinates
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile();
            var (result1, result2) = Solution(input);

            Console.WriteLine($"Part 1: {result1}");
            Console.WriteLine($"Part 2: {result2}");
        }

        // Solves Part 1 and Part 2, since they are very similar
        public static (int, int) Solution(IEnumerable<string> input)
        {
            var coordinates = ParseCoordinates(input);

            var maximumX = coordinates.Max(c => c.x);
            var maximumY = coordinates.Max(c => c.y);

            var field = new int[maximumX + 2, maximumY + 2];
            var safeCount = 0;

            CalculateDistances(ref field, coordinates, maximumX, maximumY, ref safeCount);
            ExcludeEdges(field, coordinates, maximumX, maximumY, out var excluded, out var counts);
            GetLargestArea(counts, excluded, out var largestArea);

            return (largestArea, safeCount);
        }

        // Split strings into usable coordinate tuples
        private static (int x, int y)[] ParseCoordinates(IEnumerable<string> input)
        {
            var coordinates = input.Select(s => s.Split(new[] {", "}, StringSplitOptions.None))
                .Select(s => s.Select(int.Parse).ToArray())
                .Select(s => (x: s[0], y: s[1]))
                .ToArray();
            return coordinates;
        }

        // Create a matrix that includes the distance between all coordinates
        private static void CalculateDistances(ref int[,] field, (int x, int y)[] coordinates, int maximumX, int maximumY, ref int safeCount)
        {
            for (var x = 0; x <= maximumX; x++)
            {
                for (var y = 0; y <= maximumY; y++)
                {
                    var distances = coordinates.Select((c, i) => (i: i, dist: Math.Abs(c.x - x) + Math.Abs(c.y - y)))
                        .OrderBy(c => c.dist)
                        .ToArray();

                    field[x, y] = distances[1].dist != distances[0].dist
                        ? distances[0].i
                        : -1;

                    if (distances.Sum(c => c.dist) < 10000)
                        safeCount++;
                }
            }
        }

        // Exclude all parts that are on the outer edges of the field, since they represent infinite areas
        private static void ExcludeEdges(int[,] field, IReadOnlyCollection<(int x, int y)> coordinates, int maximumX, int maximumY, out List<int> excluded, out Dictionary<int, int> counts)
        {
            excluded = new List<int>();
            counts = Enumerable.Range(-1, coordinates.Count + 1)
                .ToDictionary(i => i, _ => 0);

            for (var x = 0; x <= maximumX + 1; x++)
            {
                for (var y = 0; y <= maximumY + 1; y++)
                {
                    if (x == 0 || y == 0 || x == maximumX + 1 || y == maximumY + 1)
                    {
                        excluded.Add(field[x, y]);
                    }

                    counts[field[x, y]] += 1;
                }
            }

            excluded = excluded.Distinct().ToList();
        }

        // Calculate the size of all areas and select the largest
        private static void GetLargestArea(Dictionary<int, int> counts, ICollection<int> excluded, out int largestArea)
        {
            var area = counts.Where(c => !excluded.Contains(c.Key))
                .OrderByDescending(c => c.Value);
            largestArea = area.Max(c => c.Value);
        }

        private static IEnumerable<string> ReadInputFile()
        {
            return File.ReadAllLines("Input.txt").ToList();
        }
    }
}
