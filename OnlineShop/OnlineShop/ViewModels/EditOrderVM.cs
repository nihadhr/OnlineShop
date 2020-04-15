using OnlineShopPodaci.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ViewModels
{
    public class EditOrderVM
    {
        public int OrderID { get; set; }
        public int UserId { get; set; }
        public string UserInfo { get; set; }
        public bool IsShipped{get;set;}
        public string OrderDate { get; set; }
        public List<Rows> items { get; set; }
        public class Rows
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public bool Flag { get; set; }
            
        }
    }
}
