﻿namespace DataProtection.Web.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public decimal? Price { get; set; }
        public string Color { get; set; }

        public int ProductCategoryId { get; set; }
    }
}
