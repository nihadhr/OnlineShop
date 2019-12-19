using OnlineShopPodaci.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ViewModels
{
    public class ShowSubCategoriesVM
    {
        public string CategoryName { get; set; }
        public List<SubCategory> Subcategorylist { get; set; }



    }
}
