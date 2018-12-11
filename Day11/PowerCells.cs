using System;

namespace Day11
{
    class PowerCells
    {
        static void Main(string[] args)
        {
            var (x, y, power) = Part1(6392);
            Console.WriteLine($"Coordinate: {x}/{y} with power {power}");

            var (x2, y2, power2, squareSize) = Part2(6392);
            Console.WriteLine($"Coordinate: {x2}/{y2} with power {power2}, squareSize {squareSize}");
        }

        public static (int x, int y, int power) Part1(int input)
        {
            var field = CreatePowerField(input);
            return CalculatePowerForSquare(6392, field, 3);
        }

        public static (int x, int y, int power, int squareSize) Part2(int input)
        {
            var field = CreatePowerField(input);
            var largestPower = 0;
            var largestCoordinate = (x: 0, y: 0);
            var squareSize = 0;
            for (var i = 1; i <= 300; i++)
            {
                Console.WriteLine($"Calculating for {i}");
                var (x, y, power) = CalculatePowerForSquare(input, field, i);
                if (power > largestPower)
                {
                    Console.WriteLine($"New largest power: {power}, at coordinate ({largestCoordinate.x}, {largestCoordinate.y})");
                    largestPower = power;
                    largestCoordinate.x = x;
                    largestCoordinate.y = y;
                    squareSize = i;
                }
            }

            return (largestCoordinate.x, largestCoordinate.y, largestPower, squareSize);
        }

        public static (int x, int y, int power) CalculatePowerForSquare(int input, int[,] field, int squareSize)
        {
            // find largest 3x3 square
            var largestCombinedPower = 0;
            var largestPowerCoordinate = (x: 1, y: 1);
            for (var i = 1; i <= 300 - squareSize; i++)
            {
                for (var j = 1; j <= 300 - squareSize; j++)
                {
                    var sum = 0;
                    for (var k = 0; k < squareSize; k++)
                    {
                        for (var l = 0; l < squareSize; l++)
                        {
                            sum += field[k + i, l + j];
                        }
                    }

                    if (sum > largestCombinedPower)
                    {
                        largestCombinedPower = sum;
                        largestPowerCoordinate = (i, j);
                    }
                }
            }

            return (largestPowerCoordinate.x, largestPowerCoordinate.y, largestCombinedPower);
        }

        internal static int[,] CreatePowerField(int input)
        {
            // fill field with power levels
            var field = new int[301, 301];
            for (var i = 1; i <= 300; i++)
            {
                for (var j = 1; j <= 300; j++)
                {
                    field[i, j] = GetPowerLevel(i, j, input);
                }
            }

            return field;
        }

        internal static int GetPowerLevel(int i, int j, int input)
        {
            var rackId = i + 10;
            var powerLevelStr = ((rackId * j + input) * rackId).ToString();
            var powerLevel = int.Parse(powerLevelStr.Substring(powerLevelStr.Length - 3, 1)) - 5;
            return powerLevel;
        }
    }
}
