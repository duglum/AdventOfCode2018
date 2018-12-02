using System.Collections.Generic;
using NUnit.Framework;

namespace Day01
{
    [TestFixture]
    public class FrequenciesTests
    {
        private readonly List<TestInput> _testData = new List<TestInput>
        {
            new TestInput { Input = new List<string> { "+1", "+1", "+1" }, Result = 3 },
            new TestInput { Input = new List<string> { "+1", "+1", "-2" }, Result = 0 },
            new TestInput { Input = new List<string> { "-1", "-2", "-3" }, Result = -6}
        };

        private readonly List<TestInput> _testDataReachedTwice = new List<TestInput>
        {
            new TestInput {Input = new List<string> { "+1", "-1" }, Result = 1},
            new TestInput {Input = new List<string> { "+3", "+3", "+4", "-2", "-4" }, Result = 10},
            new TestInput {Input = new List<string> { "-6", "+3", "+8", "+5", "-6" }, Result = 5},
            new TestInput {Input = new List<string> { "+7", "+7", "-2", "-7", "-4" }, Result = 14}
        };

        private class TestInput
        {
            public List<string> Input;
            public int Result;
        }

        [Test]
        public void InputMatchesResult()
        {
            foreach (var input in _testData)
            {
                var sum = Frequencies.AddUp(input.Input);
                Assert.That(sum == input.Result);
            }
        }

        [Test]
        public void ReachedTwice()
        {
            foreach (var input in _testDataReachedTwice)
            {
                Assert.That(Frequencies.ReachedTwice(input.Input) == input.Result);
            }
        }
    }
}