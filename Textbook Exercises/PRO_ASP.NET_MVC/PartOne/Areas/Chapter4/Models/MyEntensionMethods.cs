using System;
using System.Collections.Generic;

namespace EssentialFeatures.Models {
    public static class MyEntensionMethods {
        public static decimal TotalPrices(this ShoppingCart cartParam) {
            decimal total = 0;
            foreach (Product prod in cartParam.Products) {
                total += prod.ProductPrice;
            }
            return total;
        }
        public static decimal TotalPrices(this IEnumerable<Product> productEnum) {
            decimal total = 0;
            foreach (Product prod in productEnum) {
                total += prod.ProductPrice;
            }
            return total;
        }
        public static IEnumerable<Product> FilterByCategory(
            this IEnumerable<Product> productEnum,
            string categoryParam) {
            foreach (Product product in productEnum) {
                if (product.Category == categoryParam) {
                    yield return product;
                }
            }
        }
        public static IEnumerable<Product> FilterOverAll(
            this IEnumerable<Product> productEnum, Func<Product, bool> selectorParam) {
            foreach (Product product in productEnum) {
                if (selectorParam(product)) {
                    yield return product;
                }
            }
        }
    }
}