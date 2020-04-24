using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopPodaci;
using OnlineShopPodaci.Model;
using OnlineShop.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineShop.Controllers
{
    public class CustomerController : Controller
    {
        private OnlineShopContext _database;
        private ICustomer _customer;
        
        public CustomerController(ICustomer customer,OnlineShopContext db)
        {
            _database = db;
            _customer = customer;
        }

        [Authorize(Roles = "Customer")]
        public IActionResult ChangeInfo()
        {
            int userid = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _database.Users.Find(userid);
            var model = new ChangeUserInfoVM
            {
                name=user.Name,
                surname=user.Surname,
                phonenumber=user.PhoneNumber,
                choosencity=user.CityID,
                cities=_database.city.Select(c=>new SelectListItem {Text=c.CityName,Value=c.CityID.ToString() }).ToList(),
                adress=user.Adress,
            };
            return View(model);
        }

        public IActionResult SaveUserInfo(ChangeUserInfoVM model)
        {
            int userid = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _database.Users.Find(userid);

            user.Name = model.name;
            user.Surname = model.surname;
            user.Adress = model.adress;
            user.CityID = model.choosencity;
            user.PhoneNumber = model.phonenumber;

            _database.SaveChanges();
            return Redirect("/Customer/Panel");
        }
        public IActionResult GetNotifications()
        {
            int id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var lista = _customer.GetNotifications(id);


            return PartialView();
        }
        public IActionResult Panel()
        {
            return View();
        }

        

    }
}