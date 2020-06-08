using System;
using System.Linq;
using NUnit.Framework;
using Precalc;

namespace PrecalcTest
{
    [TestFixture]
    public class NaturalFunctionTests
    {
        [Test]
        public void Test()
        {
            // arrange
            Func<int, int> func = i => i * i * 2;
            var function = new NaturalFunction<int>(func, 20, -10);
            
            // assert
            Assert.True(Enumerable.Range(-20, 40).All(i => func(i) == function.Calculate(i)));
        }
    }
}