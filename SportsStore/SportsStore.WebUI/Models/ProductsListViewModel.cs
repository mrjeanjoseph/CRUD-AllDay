﻿using SportsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsStore.WebUI.Models {
    public class ProductsListViewModel {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}