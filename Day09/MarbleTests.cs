using FluentAssertions;
using NUnit.Framework;

namespace Day09
{
    [TestFixture]
    public class MarbleTests
    {
        [Test]
        [TestCase(9, 25, 32)]
        [TestCase(10, 1618, 8317)]
        [TestCase(13, 7999, 146373)]
        [TestCase(17, 1104, 2764)]
        [TestCase(21, 6111, 54718)]
        [TestCase(30, 5807, 37305)]
        public void Part1Test(int players, int numMarbles, int result)
        {
            var part1 = Marbles.Part1(players, numMarbles);
            part1.Should().Be(result);
        }
    }
}