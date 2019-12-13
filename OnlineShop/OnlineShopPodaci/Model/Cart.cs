using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShopPodaci.Model
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        public decimal TotalPrice { get; set; }

        public Order Order { get; set; }            
        public ICollection<CartDetails> _CartDetails { get; set; }
    }
}
