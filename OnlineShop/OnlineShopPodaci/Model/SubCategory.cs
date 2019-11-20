using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopPodaci.Model
{
    public class SubCategory
    {
        public int SubCategoryID { get; set; }
        public int CategoryID { get; set; }     // DODANO
        public Category Category { get; set; }

        public string SubCategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
