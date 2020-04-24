using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ViewModels
{
    public class AdminDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string CityName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
    }
}
