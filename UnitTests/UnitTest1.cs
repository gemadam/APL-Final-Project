using APL_Final_Project;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class NormalisationTests
    {
        [TestMethod]
        [DataRow("7, 3, 21")]
        [DataRow("0.9490196, 1, 2.74978519E+30, -7.95935E+29, -5.2852965E+31")]
        public void ShouldNormalizeValuesToRange_0_1(string values)
        {
            float[] arrayToNormalize = values
                .Split(',')
                .Select(x => (float)Convert.ToDouble(x))
                .ToArray();

            var result = BmpUtil.Normalize(arrayToNormalize, arrayToNormalize.Min(), arrayToNormalize.Max());

            Assert.IsFalse(result.Any(x => x > 1), $"Max val is: {result.Max()}");
            Assert.IsFalse(result.Any(x => x < 0), $"Min val is: {result.Min()}");
        }
    }
}