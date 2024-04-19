using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace PartOne.Tests {

    [TestClass]
    public class LinqValueCalc {

        private readonly Merchandise[] merch = {
            new Merchandise {Name = "Bouret", Category = "Biznis Nasyonal", Price = 1050},
            new Merchandise {Name = "Barik Pistash", Category = "Biznis Lokal", Price = 3800},
            new Merchandise {Name = "Pye Palmis", Category = "Biznis Lokal", Price = 2000},
            new Merchandise {Name = "Konbit pou Rekolt", Category = "Biznis Lokal", Price = 500},
            new Merchandise {Name = "Sevis Transpo", Category = "Biznis Nasyonal", Price = 250},
        };

        [TestMethod]
        public void SumProductCorrectly() {
            //arrange
            Mock<IDiscountCalculator> mock = new Mock<IDiscountCalculator>();
            mock.Setup(t => t.ApplyDiscount(It.IsAny<decimal>()))
                .Returns<decimal>(total => total);
            var target = new LinqValueCalculator(mock.Object);
            
            //var discounter = new MinimumDiscountHelper();
            //var target = new LinqValueCalculator(discounter);
            //var goalTotal = merch.Sum(m => m.Price);

            //act
            var result = target.MerchandiseValue(merch);

            //assert
            Assert.AreEqual(merch.Sum(m => m.Price), result);
            //Assert.AreEqual(goalTotal, result);
        }

        private Merchandise[] CreateProduct(decimal value) {
            return new[] { new Merchandise { Price = value } };
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PassThroughVariableDiscount() {
            //arrange
            Mock<IDiscountCalculator> mock = new Mock<IDiscountCalculator>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                .Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0)))
                .Throws<ArgumentOutOfRangeException>();
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100)))
                .Returns<decimal>(total => (total * 0.9M));
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10,100, Range.Inclusive)))
                .Returns<decimal>(total => total - 5);
            var target = new LinqValueCalculator(mock.Object);

            //act
            decimal FiveDollarDiscount = target.MerchandiseValue(CreateProduct(5));
            decimal TenDollarDiscount = target.MerchandiseValue(CreateProduct(10));
            decimal FiftyDollarDiscount = target.MerchandiseValue(CreateProduct(50));
            decimal HundDollarDiscount = target.MerchandiseValue(CreateProduct(100));
            decimal FiveHundDollarDiscount = target.MerchandiseValue(CreateProduct(500));

            //assert
            Assert.AreEqual(5, FiveDollarDiscount, "$5 Fail");
            Assert.AreEqual(5, TenDollarDiscount, "$10 Fail");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 Fail");
            Assert.AreEqual(95, HundDollarDiscount, "$100 Fail");
            Assert.AreEqual(450, FiveHundDollarDiscount, "$500 Fail");
            target.MerchandiseValue(CreateProduct(0));
        }
    }
}
