using System;
using Matrix.Tools;
using NUnit.Framework;

namespace Matrix.Tests
{
    [TestFixture]
    public class float2Tests
    {
        [Test]
        public void Test_Rotation()
        {
            Assert.Equals(new float2(1, 0).Rotated(60).X, 0.5);
            Assert.Equals(new float2(1, 0).Rotated(30).Y, 0.5);
        }
    }
}