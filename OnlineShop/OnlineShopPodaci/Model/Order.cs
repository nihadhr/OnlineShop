using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShopPodaci.Model
{
    public class Order
    {
        [ForeignKey("Cart")]
        public int OrderID { get; set; }
        public Cart Cart { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
