using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopPodaci.Model
{
    public class Cart
    {
        public int CartID { get; set; }
        public decimal TotalPrice { get; set; }
        public Order Order { get; set; }
        public ICollection<CartDetails> _CartDetails { get; set; }
    }
}
