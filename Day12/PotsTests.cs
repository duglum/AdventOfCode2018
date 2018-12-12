using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Day12
{
    [TestFixture]
    public class PotsTests
    {
        private readonly List<string> _input = new List<string>
        {
            "initial state: #..#.#..##......###...###",
            "",
            "...## => #",
            "..#.. => #",
            ".#... => #",
            ".#.#. => #",
            ".#.## => #",
            ".##.. => #",
            ".#### => #",
            "#.#.# => #",
            "#.### => #",
            "##.#. => #",
            "##.## => #",
            "###.. => #",
            "###.# => #",
            "####. => #"
        };

        [Test]
        public void Generation1Test()
        {
            var (initialState, changes) = Pots.ParseInput(_input);
            var pots = ".." + initialState + "..";
            var (result, zero) = Pots.NextGeneration(pots, changes, 2L);
            result.Should().Be("....#...#....#.....#..#..#..#..");
        }
    }
}