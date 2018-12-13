using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    class MineCarts
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile();
            var (map, carts) = ParseMap(input);
            var result = Part1(map, carts);
            Console.WriteLine($"Result Part1: {result}");

            result = Part2(map, carts);
            Console.WriteLine($"Result Part2: {result}");
            Console.ReadKey();
        }

        internal static (int x, int y) Part1(char[,] map, List<Cart> carts)
        {
            while (carts.Any(c => c.Status == CartStatus.Running))
            {
                foreach (var cart in carts)
                {
                    cart.Tick(map, carts);
                    var firstCrashedCart = carts.FirstOrDefault(c => c.Status == CartStatus.Crashed);
                    if (firstCrashedCart != null)
                    {
                        return (firstCrashedCart.PositionX, firstCrashedCart.PositionY);
                    }
                }
                //PrintMap(map, carts);
            }

            return (0,0);
        }

        internal static (int x, int y) Part2(char[,] map, List<Cart> carts)
        {
            while (carts.Count > 1)
            {
                carts = carts.OrderBy(c => c.PositionY).ThenBy(c => c.PositionX).ToList();
                foreach (var cart in carts)
                {
                    cart.Tick(map, carts);
                }
                carts = carts.Where(c => c.Status != CartStatus.Crashed).ToList();
                //PrintMap(map, carts);
            }

            var remainingCart = carts.First();
            remainingCart.Tick(map, carts);
            return (remainingCart.PositionX, remainingCart.PositionY);
        }

        internal static (char[,] map, List<Cart> carts) ParseMap(List<string> input)
        {
            var cartMarkers = new List<char>{Cart.DirectionLeft, Cart.DirectionRight, Cart.DirectionUp, Cart.DirectionDown};

            var map = new char[input[0].Length, input.Count];
            var carts = new List<Cart>();

            for (var i = 0; i < input.Count; i++)
            {
                var line = input[i];
                for (var j = 0; j < line.Length; j++)
                {
                    var element = line[j];
                    if (cartMarkers.Contains(element))
                    {
                        // there is a cart here, replace with appropriate element
                        carts.Add(new Cart(j, i, element));
                        if (element == Cart.DirectionLeft || element == Cart.DirectionRight)
                        {
                            map[j, i] = '-';
                        }
                        else
                        {
                            map[j, i] = '|';
                        }
                    }
                    else
                    {
                        // track, just add it
                        map[j, i] = line[j];
                    }
                }
            }

            return (map, carts);
        }

        private static void PrintMap(char[,] map, IEnumerable<Cart> carts)
        {
            var currentMap = (char[,])map.Clone();
            foreach (var cart in carts)
            {
                currentMap[cart.PositionX, cart.PositionY] = cart.Direction;
            }

            Console.WriteLine();
            var rowLength = currentMap.GetLength(0);
            var colLength = currentMap.GetLength(1);
            for (var i = 0; i < colLength; i++)
            {
                for (var j = 0; j < rowLength; j++)
                {
                    Console.Write(currentMap[j, i]);
                }
                Console.WriteLine();
            }
        }

        private static List<string> ReadInputFile()
        {
            return File.ReadAllLines("Input.txt").ToList();
        }
    }
}
