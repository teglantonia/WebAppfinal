﻿namespace WebApp.Models
{
    public class ProductDta
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }

    }
}
