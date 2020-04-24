using System;
using System.Collections.Generic;
using System.Text;
using OnlineShopPodaci.Model;
using OnlineShopPodaci;
namespace OnlineShopServices
{
    public class CustomerServices: ICustomer
    {
        private OnlineShopContext _database;

        public CustomerServices(OnlineShopContext db)
        {
            _database = db;
        }

        
    }
}
