using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    class Combat
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile();
            var result = Part1(input);
            Console.WriteLine($"Result Part 1: {result}");
        }

        public static int Part1(List<string> input)
        {
            var (map, combatUnits) = ParseMap(input);

            while (true)
            {
                var aliveUnits = combatUnits
                    .Where(u => u.IsAlive())
                    .OrderBy(u => u.PositionY)
                    .ThenBy(u => u.PositionX)
                    .ToList();
                foreach (var unit in aliveUnits)
                {
                    unit.DoTurn(map, aliveUnits);
                }
            }

            return 0;
        }

        private static (char[,], List<CombatUnit>) ParseMap(List<string> input)
        {
            var map = new char[input.Count, input[0].Length];
            var combatUnits = new List<CombatUnit>();

            for (var i = 0; i < input.Count; i++)
            {
                var line = input[i];
                for (var j = 0; j < line.Length; j++)
                {
                    switch (line[j])
                    {
                        case '#':
                        case '.':
                            map[i, j] = line[j];
                            break;
                        case 'G':
                            map[i, j] = '.';
                            combatUnits.Add(new Goblin(i, j));
                            break;
                    }
                }
            }

            return (map, combatUnits);
        }

        private static List<string> ReadInputFile()
        {
            return File.ReadAllLines("Input.txt").ToList();
        }
    }

    public class CombatUnit
    {
        public int HitPoints { get; set; } = 200;
        public int AttackPower { get; set; } = 3;
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public CombatUnit(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        public bool IsAlive()
        {
            return HitPoints > 0;
        }

        public void DoTurn(char[,] map, List<CombatUnit> combatUnits)
        {
            if (!HasTargets(combatUnits))
                return;

            var (anyInRange, inRangeFields) = IdentifyInRangeFields(map, combatUnits);
            if (anyInRange)
            {
                var nearestTarget = combatUnits
                    .Where(IsAdjacent)
                    .OrderBy(u => u.HitPoints)
                    .ThenBy(u => u.PositionY)
                    .ThenBy(u => u.PositionX)
                    .FirstOrDefault();

                if (nearestTarget == null)
                    return;

                Attack(nearestTarget, combatUnits);
            }
            else
            {
                if (inRangeFields.Count == 0)
                    return;

                Move(map, combatUnits, inRangeFields);
            }
        }

        public bool HasTargets(IEnumerable<CombatUnit> units)
        {
            return units.Select(x => x != this && x.GetType() != this.GetType()).Any();
        }

        private (bool anyInRange, List<(int x, int y)> inRangeFields) IdentifyInRangeFields(char[,] map, List<CombatUnit> combatUnits)
        {
            var inRangeFields = new List<(int x, int y)>();
            foreach (var unit in combatUnits)
            {
                inRangeFields.AddRange(unit.GetInRangeFields(map, combatUnits));
            }

            var anyInRange = combatUnits.Any(IsAdjacent);

            return (anyInRange, inRangeFields);
        }

        public bool IsAdjacent(CombatUnit target)
        {
            return Math.Abs(PositionX - target.PositionX) + Math.Abs(PositionY - target.PositionY) == 1;
        }

        private void Attack(CombatUnit target, List<CombatUnit> combatUnits)
        {
            target.HitPoints -= AttackPower;
            if (target.HitPoints > 0)
                return;

            if (target.GetType() != typeof(Goblin))
                return;

            combatUnits.RemoveAt(combatUnits.IndexOf(target));
        }

        private void Move(char[,] map, List<CombatUnit> combatUnits, List<(int x, int y)> inRangeFields)
        {
            var queue = new Queue<(int x, int y)>();
            queue.Enqueue((PositionX, PositionY));

            var bla = new Dictionary<(int x, int y), (int px, int py)>();
            bla.Add((PositionX, PositionY), (-1, -1));

            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();
                foreach (var (ax, ay) in Adjacent)
                {
                    (int x, int y) field = (x + ax, y + ay);
                    if (bla.ContainsKey(field) || !FieldIsFree(map, combatUnits, field.x, field.y))
                        continue;

                    queue.Enqueue(field);
                    bla.Add(field, (x, y));
                }
            }

            var paths = inRangeFields
                .Select(t => (t.x, t.y, path: GetPath(bla, t.x, t.y)))
                .Where(t => t.path != null)
                .OrderBy(t => t.path.Count)
                .ThenBy(t => t.y)
                .ThenBy(t => t.x)
                .ToList();

            var shortestPath = paths.FirstOrDefault().path;
            if (shortestPath != null)
            {
                PositionX = shortestPath[0].x;
                PositionY = shortestPath[0].y;
            }
        }

        private List<(int x, int y)> GetPath(Dictionary<(int x, int y), (int px, int py)> bla, int destinationX, int destinationY)
        {
            if (!bla.ContainsKey((destinationX, destinationY)))
                return null;

            var path = new List<(int x, int y)>();
            (int x, int y) = (destinationX, destinationY);
            while (x != PositionX || y != PositionY)
            {
                path.Add((x, y));
                (x, y) = bla[(x, y)];
            }

            path.Reverse();
            return path;
        }

        // in reading order
        private static readonly (int x, int y)[] Adjacent =
        {
            ( 0, -1),
            (-1,  0),
            ( 1,  0),
            ( 0,  1)
        };

        private List<(int x, int y)> GetInRangeFields(char[,] map, List<CombatUnit> combatUnits)
        {
            var list = new List<(int x, int y)>();

            if (FieldIsFree(map, combatUnits, PositionX + 0, PositionY - 1))
                list.Add((PositionX + 0, PositionY - 1));

            if (FieldIsFree(map, combatUnits, PositionX - 1, PositionY + 0))
                list.Add((PositionX - 1, PositionY + 0));

            if (FieldIsFree(map, combatUnits, PositionX + 0, PositionY + 1))
                list.Add((PositionX + 0, PositionY + 1));

            if (FieldIsFree(map, combatUnits, PositionX + 1, PositionY + 0))
                list.Add((PositionX + 1, PositionY + 0));

            return list;
        }

        private bool FieldIsFree(char[,] map, List<CombatUnit> combatUnits, int x, int y)
        {
            if (map[x, y] != '.')
                return false;

            if (combatUnits.Any(u => u.PositionX == x && u.PositionY == y))
                return false;

            return true;
        }
    }

    public class Elf : CombatUnit
    {
        public Elf(int positionX, int positionY) : base(positionX, positionY)
        { }
    }

    public class Goblin : CombatUnit
    {
        public Goblin(int positionX, int positionY) : base (positionX, positionY)
        {}
    }

    [TestFixture]
    public class ChocolateChartsTests
    {
        [Test]
        public void Part1Test()
        {
            
        }

        //[Test]
        //public void Part2Tests(int result, string search)
        //{
        //    Combat.Part2(search).Should().Be(result);
        //}
    }
}
