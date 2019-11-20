using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopPodaci.Model
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductNumber { get; set; }

        public SubCategory SubCategory { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public ICollection<CartDetails> _CartDetails { get; set; }
    }
}
