using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PartOne.Tests {
    [TestClass]
    public class EssentialTools {

        private IDiscountCalculator GetTestOject() {
            return new MinimumDiscountHelper();
        }

        [TestMethod]
        public void DiscountAbove100() {
            //Arrange
            IDiscountCalculator target = GetTestOject();
            decimal total = 200;

            //act
            var discountedTotal = target.ApplyDiscount(total);

            //assert
            Assert.AreEqual(total * 0.9M, discountedTotal);
        }

        [TestMethod]
        public void DiscountBetweenTenAnd100() {
            //Arrange
            IDiscountCalculator target = GetTestOject();
            decimal[] total = { 10, 100, 50 };

            //act
            var TenDollarDiscount = target.ApplyDiscount(total[0]);
            var OneHundDollarDiscount = target.ApplyDiscount(total[1]);
            var FiftyDollarDiscount = target.ApplyDiscount(total[2]);

            //assert
            Assert.AreEqual(5, TenDollarDiscount, "$10 discount is wrong");
            Assert.AreEqual(95, OneHundDollarDiscount, "$100 discount is wrong");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 discount is wrong");
        }

        [TestMethod]
        public void DiscountLessThanTen() {
            //Arrange
            IDiscountCalculator target = GetTestOject();
            decimal[] total = { 9, 5, 0 };

            //act
            decimal NineDiscount = target.ApplyDiscount(total[0]);
            decimal OneDiscount = target.ApplyDiscount(total[1]);
            decimal ZeroDiscount = target.ApplyDiscount(total[2]);

            //assert
            Assert.AreEqual(9, NineDiscount);
            Assert.AreEqual(5, OneDiscount);
            Assert.AreEqual(0, ZeroDiscount);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DiscountNegativeTotal() {
            //Arrange
            IDiscountCalculator target = GetTestOject();

            //act
            target.ApplyDiscount(-1);
        }
    }
}
