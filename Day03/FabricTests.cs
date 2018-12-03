using NUnit.Framework;
using System.Collections.Generic;

namespace Day03
{
    [TestFixture]
    public class FabricTests
    {
        private static readonly List<string> inputList = new List<string>
        {
            "#1 @ 1,3: 4x4",
            "#2 @ 3,1: 4x4",
            "#3 @ 5,5: 2x2"
        };

        [Test]
        public void Parse()
        {
            Fabric.ParseInput(inputList[0], out var id, out var left, out var top, out var width, out var height);
            Assert.That(id == 1);
            Assert.That(left == 1);
            Assert.That(top == 3);
            Assert.That(width == 4);
            Assert.That(height == 4);
        }

        [Test]
        public void FillTest()
        {
            var fabric = Fabric.FillFabric(inputList);
            // spot check
            Assert.That(fabric[1,4] == 1);
            Assert.That(fabric[4,1] == 1);
            Assert.That(fabric[4,4] == 2);
        }

        [Test]
        public void MultipleTest()
        {
            var fabric = Fabric.FillFabric(inputList);
            var multiple = Fabric.GetMultipleClaims(fabric);
            Assert.That(fabric[4,4] >= 1);
            Assert.That(fabric[4,5] >= 1);
            Assert.That(fabric[5,4] >= 1);
            Assert.That(fabric[5,5] >= 1);
            Assert.That(multiple == 4);
        }

        [Test]
        public void OverlapTest()
        {
            var fabric = Fabric.FillFabric(inputList);
            var idNoOverlap = Fabric.GetIdWithoutOverlap(fabric, inputList);
            Assert.That(idNoOverlap == 3);
        }
    }
}
