using FluentAssertions;
using NUnit.Framework;

namespace Day11
{
    [TestFixture]
    public class PowerCellsTests
    {
        [Test]
        [TestCase(3, 5, 8, 4)]
        [TestCase(122, 79, 57, -5)]
        [TestCase(217, 196, 39, 0)]
        [TestCase(101, 153, 71, 4)]
        public void GetPowerLevelTest(int i, int j, int input, int result)
        {
            PowerCells.GetPowerLevel(i, j, input).Should().Be(result);
        }

        [Test]
        [TestCase(18, 90, 269, 16, 113)]
        [TestCase(42, 232, 251, 12, 119)]
        public void GridTest(int input, int x, int y, int squareSize, int power)
        {
            var field = PowerCells.CreatePowerField(input);
            var (resultX, resultY, resultPower) = PowerCells.CalculatePowerForSquare(input, field, squareSize);
            resultX.Should().Be(x);
            resultY.Should().Be(y);
            resultPower.Should().Be(power);
        }
    }
}