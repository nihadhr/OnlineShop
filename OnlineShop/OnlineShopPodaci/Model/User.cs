using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopPodaci.Model
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public int Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public City City { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
