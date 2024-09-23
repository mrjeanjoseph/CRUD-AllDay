using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain.Concrete {
    public class EFProductRepository : IProductRepository {

        private EFDbContext _context = new EFDbContext();

        public IEnumerable<Product> Products {
            get { return _context.Products; }
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

        //public void SaveLog(ActionLog actionLog) {

        //    ActionLog dbEntry = _context.ActionLogs.Find(actionLog.LogId);

        //    if(dbEntry != null) {

        //        dbEntry.Controller = actionLog.Controller;
        //        dbEntry.Action = actionLog.Action;
        //        dbEntry.HttpMethod = actionLog.HttpMethod;
        //        dbEntry.URL = actionLog.URL;
        //        dbEntry.ActionDate = actionLog.ActionDate;
        //    }
        //}
    }
}
