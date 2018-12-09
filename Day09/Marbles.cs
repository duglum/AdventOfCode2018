using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09
{
    class Marbles
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile().Split(' ');
            var players = int.Parse(input[0]);
            var numMarbles = int.Parse(input[6]);
            var result = Part1(players, numMarbles);
            Console.WriteLine($"Part 1: {result}");

            result = Part2(players, numMarbles, 100);
            Console.WriteLine($"Part 2: {result}");
        }

        public static long Part1(int numPlayers, int numMarbles)
        {
            // Marbles
            var marbles = new LinkedList<long>();
            var currentMarble = marbles.AddFirst(0);

            // Players
            var players = new long[numPlayers];
            for (var i = 0; i < numPlayers; i++)
            {
                players[i] = 0;
            }
            var currentPlayer = 0;

            for (var i = 1; i <= numMarbles; i++)
            {
                if (i % 23 != 0)
                {
                    var placementElement = currentMarble.NextOrFirst();
                    currentMarble = marbles.AddAfter(placementElement, i);
                }
                else
                {
                    for (var j = 0; j < 7; j++)
                    {
                        currentMarble = currentMarble.PreviousOrLast();
                    }
                    players[currentPlayer] += i + (long)currentMarble?.Value;
                    var previousMarble = currentMarble.NextOrFirst();
                    marbles.Remove(currentMarble);
                    currentMarble = previousMarble;
                }

                currentPlayer++;
                if (currentPlayer >= numPlayers)
                    currentPlayer = 0;
            }

            return players.Max();
        }

        public static long Part2(int numPlayers, int numMarbles, int multiplier)
        {
            return Part1(numPlayers, numMarbles * multiplier);
        }

        private static string ReadInputFile()
        {
            return File.ReadAllLines("Input.txt")[0];
        }
    }

    public static class CircularLinkedListExtension
    {
        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
        {
            return current.Next ?? current.List.First;
        }

        public static LinkedListNode<T> PreviousOrLast<T>(this LinkedListNode<T> current)
        {
            return current.Previous ?? current.List.Last;
        }
    }
}
