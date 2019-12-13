using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShopPodaci.Model
{
    public class CartDetails
    {
        [ForeignKey("Cart")]
        public int CartID { get; set; }
        public Cart Cart { get; set; }
        
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product Product { get; set; }
        
        public int Quantity { get; set; }
    }
}
