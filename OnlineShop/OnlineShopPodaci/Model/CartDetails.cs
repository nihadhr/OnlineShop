using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopPodaci.Model
{
    public class CartDetails
    {
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
