using NUnit.Framework;

namespace Day08
{
    [TestFixture]
    public class MemoryTests
    {
        private const string Input = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";

        [Test]
        public void Part1Test()
        {
            var result = Memory.Part1(Input);
            Assert.That(result == 138);
        }

        [Test]
        public void Part2Test()
        {
            var result = Memory.Part2(Input);
            Assert.That(result == 66);
        }
    }
}