using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Day13
{
    [TestFixture]
    public class MineCartsTests
    {
        private readonly List<string> _testInput1 = new List<string> {"|", "v", "|", "|", "|", "^", "|"};
        private readonly List<string> _testInput2 = new List<string>
        {
            @"/->-\        ",
            @"|   |  /----\",
            @"| /-+--+-\  |",
            @"| | |  | v  |",
            @"\-+-/  \-+--/",
            @"\------/     "
        };
        private readonly List<string> _testInput3 = new List<string>
        {
            @"/>-<\  ",
            @"|   |  ",
            @"| /<+-\",
            @"| | | v",
            @"\>+</ |",
            @"  |   ^",
            @"  \<->/"
        };

        [Test]
        public void Part1Test1()
        {
            var (map, carts) = MineCarts.ParseMap(_testInput1);
            var (x, y) = MineCarts.Part1(map, carts);
            (x, y).Should().Be((0, 3));
        }

        [Test]
        public void Part1Test2()
        {
            var (map, carts) = MineCarts.ParseMap(_testInput2);
            var (x, y) = MineCarts.Part1(map, carts);
            (x, y).Should().Be((7, 3));
        }

        [Test]
        public void Part2Test1()
        {
            var (map, carts) = MineCarts.ParseMap(_testInput3);
            var (x, y) = MineCarts.Part2(map, carts);
            (x, y).Should().Be((6, 4));
        }
    }
}