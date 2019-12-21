using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Models;
using OnlineShop.ViewModels;
using OnlineShopPodaci;
using OnlineShopPodaci.Model;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0,
            Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        
        public IActionResult AddToCart(int productid,int userid=6,int q=1)
        {
            OnlineShopContext novi = new OnlineShopContext();
            Cart singlerecord = novi.cart.SingleOrDefault(u => u.UserID == userid && u.ProductID == productid);
            Product product = novi.product.Find(productid);
            if (singlerecord != null) {
                singlerecord.Quantity += q;
                singlerecord.TotalPrice = (singlerecord.Quantity + q) * product.UnitPrice;
            }
            else
            {
                Cart newrecord = new Cart {
                    UserID = userid,
                    ProductID = productid,
                    Quantity = q,
                    TotalPrice = product.UnitPrice * q
                };
                novi.Add(newrecord);

            }
            novi.SaveChanges();
            novi.Dispose();
            return View("ItemAdded");
        }

        
        public IActionResult LookInCart(int userid=6)
        {
            OnlineShopContext novi = new OnlineShopContext();
            List<Cart> listacart = novi.cart.Where(u => u.UserID == userid).ToList();
            List<LookInCartVM> listavm = listacart
            .Select(s => new LookInCartVM {
                ProductID = s.ProductID,
                ProductNumber = novi.product.Find(s.ProductID).ProductNumber,
                ProductName = novi.product.Find(s.ProductID).ProductName,
                SubCategoryName = novi.subcategory.Find(novi.product.Find(s.ProductID).SubCategoryID).SubCategoryName,
                UnitPrice = novi.product.Find(s.ProductID).UnitPrice,
                Quantity=s.Quantity
            }
            ).ToList();
            novi.Dispose();
            return View(listavm);
        }
        public IActionResult RemoveFromCart(int productid,int userid)
        {
            OnlineShopContext novi = new OnlineShopContext();
            novi.cart.Remove(novi.cart.SingleOrDefault(p => p.ProductID == productid && p.UserID == userid));
            novi.SaveChanges();
            novi.Dispose();
            return Redirect("LookInCart");
        }
        public IActionResult DeleteCart(int userid)  
        {
            OnlineShopContext novi = new OnlineShopContext();
            novi.cart.RemoveRange(novi.cart.Where(p =>p.UserID == userid));
            novi.SaveChanges();
            novi.Dispose();
            return Redirect("LookInCart");
        }


        // LOGIN 

        public IActionResult LogInForm()
        {
            return View();
        }
        public IActionResult CheckLogIn(string UserMail, string UserPass)
        {
            OnlineShopContext baza = new OnlineShopContext();
            var user = baza.user.Where(u => u.Email == UserMail && u.Password == UserPass).FirstOrDefault();
            if (user == null)
                return Redirect("LogInForm");
            else
                return Redirect("Index");
        }
        public IActionResult Registration()
        {
            OnlineShopContext _database = new OnlineShopContext();
            ViewData["gradovi"] = _database.city.ToList();
            ViewData["spol"] = _database.gender.ToList();

            return View();
        }
        public IActionResult SaveRegistration(string name,string surname,DateTime birthdate,int cityID,string adresa,string email,string password,int genderID)
        {
            OnlineShopContext _database = new OnlineShopContext();

            User user = new User
            {
                Name = name,
                Surname = surname,
                BirthDate = birthdate,
                CityID = cityID,
                Adress = adresa,
                Email = email,
                Password = password,
                GenderID = genderID,
            };


            _database.user.Add(user);
            _database.SaveChanges();
            _database.Dispose();
            return Redirect("RegistrationSuccessful");           
        }
        public IActionResult RegistrationSuccessful()
        {
            return View();
        }
    }
}
