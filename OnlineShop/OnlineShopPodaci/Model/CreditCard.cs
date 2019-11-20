using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopPodaci.Model
{
    public class CreditCard
    {
        public int CreditCardID { get; set; }
        public CardType CardType { get; set; }
        public int CreditCardNumber { get; set; }
        public int CSC { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
    }
}
