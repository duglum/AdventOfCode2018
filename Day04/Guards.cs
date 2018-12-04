using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day04
{
    class Guards
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile();
            var result = Strategy1(input);
            Console.WriteLine($"Strategy 1: {result}");

            result = Strategy2(input);
            Console.WriteLine($"Strategy 2: {result}");
        }

        public static int Strategy1(IEnumerable<string> inputList)
        {
            var (sleepAmount, sleepMinutes) = CalculateSleeping(inputList);

            var sleepiestGuard = sleepAmount.OrderByDescending(x => x.Value).Select(x => x.Key).First();
            var max = sleepMinutes[sleepiestGuard].Max();

            for (var i = 0; i < 60; i++)
            {
                if (sleepMinutes[sleepiestGuard][i] == max)
                    return i * sleepiestGuard;
            }

            return 0;
        }

        public static int Strategy2(IEnumerable<string> inputList)
        {
            var (sleepAmount, sleepMinutes) = CalculateSleeping(inputList);

            var max = sleepMinutes.SelectMany(x => x.Value).Max();

            foreach (var sleepiestGuard in sleepMinutes.Keys)
            {
                for (var i = 0; i < 60; i++)
                {
                    if (sleepMinutes[sleepiestGuard][i] == max)
                        return i * sleepiestGuard;
                }
            }

            return 0;
        }

        internal static (Dictionary<int, int>, Dictionary<int, int[]>) CalculateSleeping(IEnumerable<string> inputList)
        {
            var ordered = inputList.OrderBy(i => i).ToList();
            var sleepAmount = new Dictionary<int, int>();
            var sleepMinutes = new Dictionary<int, int[]>();

            var currentGuard = -1;
            var sleepMinute = 0;
            foreach (var item in ordered)
            {
                var timestamp = item.Substring(1, 16).Split(' ');
                var minute = int.Parse(timestamp[1].Split(':')[1]);
                var text = item.Substring(19).Trim();
                switch (text.Substring(0, 5).ToLower())
                {
                    case "guard":
                        currentGuard = int.Parse(text.Substring(7).Split(' ')[0]);
                        if (!sleepAmount.ContainsKey(currentGuard))
                        {
                            sleepAmount[currentGuard] = 0;
                        }
                        if (!sleepMinutes.ContainsKey(currentGuard))
                        {
                            sleepMinutes[currentGuard] = new int[60];
                        }
                        break;
                    case "falls":
                        sleepMinute = minute;
                        break;
                    case "wakes":
                        for (var i = sleepMinute; i < minute; i++)
                        {
                            sleepAmount[currentGuard]++;
                            sleepMinutes[currentGuard][i]++;
                        }
                        break;
                }
            }

            return (sleepAmount, sleepMinutes);
        }

        private static IEnumerable<string> ReadInputFile()
        {
            return File.ReadAllLines("Input.txt").ToList();
        }
    }
}