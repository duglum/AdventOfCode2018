using NUnit.Framework;
using System.Collections.Generic;

namespace Day06
{
    [TestFixture]
    public class CoordinatesTests
    {
        private readonly List<string> _input = new List<string>{
            "1, 1",
            "1, 6",
            "8, 3",
            "3, 4",
            "5, 5",
            "8, 9"
        };

        [Test]
        public void Solution()
        {
            var (result1, result2) = Coordinates.Solution(_input);
            Assert.That(result1 == 17);
            Assert.That(result2 == 90);
        }
    }
}