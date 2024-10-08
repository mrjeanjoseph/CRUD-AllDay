﻿using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsStore.Domain.Concrete {
    public class EFProductRepository : IProductRepository {

        private EFDbContext _context = new EFDbContext();

        public IEnumerable<Product> Products {
            get { return _context.Products; }
        }

        public IEnumerable<ActionLog> GetActionLogs {
            get { return _context.ActionLogs; }
        }

        public void SaveProduct(Product product) {

            if (product.ProductID == 0) {
                _context.Products.Add(product);
            } else {
                Product dbEntry = _context.Products.Find(product.ProductID);
                if (dbEntry != null) {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            _context.SaveChanges();
        }

        public Product DeleteProduct(int productId) {

            Product dbEntry = _context.Products.Find(productId);
            if (dbEntry != null) {
                _context.Products.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
