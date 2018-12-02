using System.Collections.Generic;
using NUnit.Framework;

namespace Day02
{
    [TestFixture]
    public class MultipleLettersTests
    {
        private readonly IEnumerable<string> _part1Input = new List<string>
        {
            "abcdef",
            "bababc",
            "abbcde",
            "abcccd",
            "aabcdd",
            "abcdee",
            "ababab"
        };

        private readonly List<string> _part2Input = new List<string>
        {
            "abcde",
            "fghij",
            "klmno",
            "pqrst",
            "fguij",
            "axcye",
            "wvxyz"
        };

        [Test]
        public void SummedUp()
        {
            var checksum = MultipleLetters.Checksum(_part1Input);
            Assert.That(checksum == 12);
        }

        [Test]
        public void Part2()
        {
            var result = MultipleLetters.Part2(_part2Input);
            Assert.That(result.Equals("fgij"));
        }
    }
}