using Contracts.Extensions;
using Contracts.Map;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCleaner.Tests
{
    [TestFixture]
    public class MatrixConvertionTest
    {

        [Test]
        public void MatrixConvertion_ShouldReturnFalse()
        {
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "Dull", "S", "S"} };
            var result = map.ToPlaceStatuses();
            Assert.AreEqual(false, result.IsSucceed);
        }

        [Test]
        public void MatrixConvertion_ShouldReturnTrue()
        {
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "null", "S", "S"} };
            var result = map.ToPlaceStatuses();
            Assert.AreEqual(true, result.IsSucceed);
        }

        [Test]
        public void MatrixConvertion_TestValues_ShouldReturnTrue()
        {
            var map = new string[2, 3] { { "S", "C", "Null" }, { "SS", "C", "Null" } };
            var result = map.ToPlaceStatuses();            
            Assert.AreEqual(PlaceStatus.S, result.Matrix[0, 0]);
            Assert.AreEqual(PlaceStatus.C, result.Matrix[0, 1]);
            Assert.AreEqual(PlaceStatus.Null, result.Matrix[0, 2]);
            Assert.AreEqual(PlaceStatus.NotDefined, result.Matrix[1, 0]);
            Assert.AreEqual(PlaceStatus.NotDefined, result.Matrix[1, 1]);
            Assert.AreEqual(PlaceStatus.NotDefined, result.Matrix[1, 2]);
        }
    }
}
