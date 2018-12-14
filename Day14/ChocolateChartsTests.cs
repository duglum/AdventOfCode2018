using FluentAssertions;
using NUnit.Framework;

namespace Day14
{
    [TestFixture]
    public class ChocolateChartsTests
    {
        [Test]
        [TestCase(9, "5158916779")]
        [TestCase(5, "0124515891")]
        [TestCase(18, "9251071085")]
        [TestCase(2018, "5941429882")]
        public void Part1Test(int numNewRecipes, string result)
        {
            ChocolateCharts.Part1(numNewRecipes).Should().Be(result);
        }

        [Test]
        [TestCase(9, "51589")]
        [TestCase(5, "01245")]
        [TestCase(18, "92510")]
        [TestCase(2018, "59414")]
        public void Part2Tests(int result, string search)
        {
            ChocolateCharts.Part2(search).Should().Be(result);
        }
    }
}