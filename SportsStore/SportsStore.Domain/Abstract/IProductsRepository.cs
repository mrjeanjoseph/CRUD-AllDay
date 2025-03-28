﻿using SportsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsStore.Domain.Abstract {
    public interface IProductRepository {

        IEnumerable<Product> Products { get; }

        void SaveProduct(Product product);

        //void SaveLog(ActionLog actionLog);

        Product DeleteProduct(int productId);

        IEnumerable<ActionLog> GetActionLogs { get; }
    }
}
