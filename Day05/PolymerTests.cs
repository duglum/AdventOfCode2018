using NUnit.Framework;

namespace Day05
{
    [TestFixture]
    public class PolymerTests
    {
        private const string TestInput = "dabAcCaCBAcCcaDA";
        
        [Test]
        public void Part1()
        {
            var result = Polymer.Part1(TestInput);
            Assert.That(result == "dabCBAcaDA".Length);
        }

        [Test]
        public void Part2()
        {
            var result = Polymer.Part2(TestInput);
            Assert.That(result == 4);
        }
    }
}