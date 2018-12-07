using NUnit.Framework;
using System.Collections.Generic;

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
            Assert.That(result == "CABDFE");
        }

        [Test]
        public void Part2()
        {
            var result = Order.Part2(_testInput);
            Assert.That(result == 15);
        }
    }
}