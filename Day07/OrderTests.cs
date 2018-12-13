using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;

namespace Day07
{
    [TestFixture]
    public class OrderTests
    {
        private readonly List<string> _testInput = new List<string>
        {
            "Step C must be finished before step A can begin.",
            "Step C must be finished before step F can begin.",
            "Step A must be finished before step B can begin.",
            "Step A must be finished before step D can begin.",
            "Step B must be finished before step E can begin.",
            "Step D must be finished before step E can begin.",
            "Step F must be finished before step E can begin."
        };

        [Test]
        public void Part1()
        {
            var result = Order.Part1(_testInput);
            result.Should().Be("CABDFE");
        }

        [Test]
        public void Part2()
        {
            // adjusted to 5 workers, with 2 workers it would be 15
            var result = Order.Part2(_testInput);
            result.Should().Be(253);
        }
    }
}