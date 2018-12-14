using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Day14
{
    class ChocolateCharts
    {
        static void Main(string[] args)
        {
            var result = Part1("37", 9);
            Console.WriteLine($"Result Part1: {result}");
        }

        public static string Part1(string input, int numNewRecipes)
        {
            var elves = new List<Elf>();
            var scoreboard = new LinkedList<int>();

            for (var i = 0; i < input.Length; i++)
            {
                var value = int.Parse(input.Substring(i, 1));
                LinkedListNode<int> node;
                if (scoreboard.Count == 0)
                {
                    node = scoreboard.AddFirst(value);
                }
                else
                {
                    node = scoreboard.AddLast(value);
                }
                elves.Add(new Elf(node, value));
            }
            
            for (var recipeCounter = 0; recipeCounter < numNewRecipes + 10; recipeCounter++)
            {
                var sumRecipes = elves.Sum(e => e.CurrentRecipe).ToString().PadLeft(elves.Count, '0');
                for (var i = 0; i < elves.Count; i++)
                {
                    scoreboard.AddLast(int.Parse(sumRecipes.Substring(i, 1)));
                }

                foreach (var elf in elves)
                {
                    for (var j = 0; j <= elf.CurrentRecipe; j++)
                    {
                        elf.Position.NextOrFirst();
                    }

                    elf.CurrentRecipe = elf.Position.Value;
                }

                PrintBoard(scoreboard);
            }

            var sb = new StringBuilder();
            var recipe = scoreboard.First;
            int counter;
            for (counter = 1; counter < numNewRecipes; counter++)
            {
                recipe = recipe.NextOrFirst();
            }

            for (; counter < 10; counter++)
            {
                recipe = recipe.NextOrFirst();
                sb.Append(recipe.Value);
            }

            return sb.ToString();
        }

        private static void PrintBoard(LinkedList<int> scoreboard)
        {
            var element = scoreboard.First;
            Console.Write($"{element?.Value} ");
            for (var i = 1; i < scoreboard.Count; i++)
            {
                element = element?.Next;
                Console.Write($"{element?.Value} ");
            }
            Console.WriteLine();
        }
    }

    public class Elf
    {
        public LinkedListNode<int> Position { get; set; }
        public int CurrentRecipe { get; set; }

        public Elf(LinkedListNode<int> position, int currentRecipe)
        {
            Position = position;
            CurrentRecipe = currentRecipe;
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

    [TestFixture]
    public class ChocolateChartsTests
    {
        [Test]
        public void Part1Test()
        {
            var result = ChocolateCharts.Part1("37", 9);
            result.Should().Be("5158916779");
        }
    }
}
