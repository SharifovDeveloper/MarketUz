﻿using MarketUz.Domain.Common;

namespace MarketUz.Domain.Entities
{
    public class ProductCategory : EntityBase
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
  
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
