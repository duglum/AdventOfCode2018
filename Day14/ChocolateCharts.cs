using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day14
{
    class ChocolateCharts
    {
        static void Main(string[] args)
        {
            var result = Part1(920831);
            Console.WriteLine($"Result Part1: {result}");

            var result2 = Part2("920831");
            Console.WriteLine($"Result Part2: {result2}");
            Console.ReadKey();
        }

        public static string Part1(int numNewRecipes)
        {
            var (scoreboard, elves) = PrepareScoreboard("37");

            for (var recipeCounter = 0; recipeCounter < numNewRecipes + 10; recipeCounter++)
            {
                AddNewRecipes(scoreboard, elves);
                MoveElves(elves);
            }

            return GetTenRecipes(scoreboard, numNewRecipes);
        }

        public static int Part2(string numNewRecipes)
        {
            var (scoreboard, elves) = PrepareScoreboard("37");
            var searchElement = scoreboard.First;
            while (true)
            {
                var sbString = GetScoreboardAsString(searchElement, numNewRecipes.Length);
                if (sbString.Contains(numNewRecipes))
                {
                    return GetScoreboardAsString(scoreboard, 0).IndexOf(numNewRecipes, StringComparison.CurrentCulture);
                }

                AddNewRecipes(scoreboard, elves);
                MoveElves(elves);
                searchElement = searchElement?.Next;
            }
        }

        private static string GetScoreboardAsString(LinkedList<int> scoreboard, int start)
        {
            var sb = new StringBuilder();
            var recipe = scoreboard.First;
            for (var i = 0; i < start; i++)
            {
                recipe = recipe?.Next;
            }
            for (var i = start; i < scoreboard.Count; i++)
            {
                sb.Append(recipe?.Value);
                recipe = recipe?.Next;
            }
            return sb.ToString();
        }

        private static string GetScoreboardAsString(LinkedListNode<int> searchElement, int numElements)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < numElements; i++)
            {
                sb.Append(searchElement?.Value);
                searchElement = searchElement?.Next;
            }
            return sb.ToString();
        }

        private static string GetTenRecipes(LinkedList<int> scoreboard, int numNewRecipes)
        {
            var sb = new StringBuilder();
            var recipe = scoreboard.First;
            int counter;
            for (counter = 1; counter < numNewRecipes; counter++)
            {
                recipe = recipe.NextOrFirst();
            }

            for (var i = 0; i < 10; i++)
            {
                recipe = recipe.NextOrFirst();
                sb.Append(recipe.Value);
            }

            return sb.ToString();
        }

        private static void MoveElves(IEnumerable<Elf> elves)
        {
            foreach (var elf in elves)
            {
                for (var j = 0; j <= elf.CurrentRecipe; j++)
                {
                    elf.Position = elf.Position.NextOrFirst();
                }

                elf.CurrentRecipe = elf.Position.Value;
            }
        }

        private static void AddNewRecipes(LinkedList<int> scoreboard, IReadOnlyCollection<Elf> elves)
        {
            var sumRecipes = elves.Sum(e => e.CurrentRecipe).ToString();
            for (var i = 0; i < sumRecipes.Length; i++)
            {
                var currentRecipe = sumRecipes.Substring(i, 1);
                scoreboard.AddLast(int.Parse(currentRecipe));
            }
        }

        private static (LinkedList<int> scoreboard, List<Elf> elves) PrepareScoreboard(string input)
        {
            var elves = new List<Elf>();
            var scoreboard = new LinkedList<int>();

            for (var i = 0; i < input.Length; i++)
            {
                scoreboard.AddLast(int.Parse(input.Substring(i, 1)));
            }

            elves.Add(new Elf(scoreboard.First, scoreboard.First.Value));
            elves.Add(new Elf(scoreboard.First.Next, scoreboard.First.NextOrFirst().Value));

            return (scoreboard, elves);
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
}
