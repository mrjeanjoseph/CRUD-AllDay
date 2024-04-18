using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var discounter = new MinimumDiscountHelper();
            var target = new LinqValueCalculator(discounter);
            var goalTotal = merch.Sum(m => m.Price);

            //act
            var result = target.MerchandiseValue(merch);

            //assert
            Assert.AreEqual(goalTotal, result);
        }
    }
}
