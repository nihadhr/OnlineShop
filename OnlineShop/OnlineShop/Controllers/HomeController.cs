using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Models;
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


       
     

        //public IActionResult AddToCart(int ID, int qID)
        //{
        //    OnlineShopContext novi = new OnlineShopContext();
        //    List<Cart> cart = novi.cart.ToList();  //uzimam sve korpe, radi provjere da li već postoji neka i da li da pravim novu ili dodajem na postojeću CartID
        //    if (cart.Count() == 0)
        //    {
        //        Cart newCart = new Cart();  //nema nijedne, kreiram novu Korpu  //bool aktivna korpa ? ?!?
        //        novi.Add(newCart);
        //        novi.SaveChanges();
        //    }

        //    int IDCart = novi.cart.ToList().Last().CartID;

        //    if (novi.cartdetails.Find(IDCart, ID) != null)
        //    {
        //        novi.cartdetails.Find(IDCart, ID).Quantity += qID;  //ako već postoji samo povećaj količinu za datu
        //    }
        //    else
        //    {
        //        CartDetails newCartDetail = new CartDetails();
        //        newCartDetail.CartID = IDCart;
        //        newCartDetail.ProductID = ID;
        //        newCartDetail.Quantity = qID;
        //        novi.Add(newCartDetail);

        //    }
        //    //funkcija za kalkulaciju polja TotalPrice u tabeli Cart

        //    novi.SaveChanges();
        //    CalculateTotalPrice(IDCart);
        //    novi.Dispose();
        //    return View("ItemAdded");
        //}

        //public void CalculateTotalPrice(int cartid)
        //{
        //    OnlineShopContext novi = new OnlineShopContext();
        //    var SetTotalPrice = novi.cart.Find(cartid);
        //    decimal sum = 0;
        //    List<CartDetails> lista = novi.cartdetails.Include(s => s.Product).Include(c => c.Cart).ToList();
        //    foreach (CartDetails cd in lista)
        //    {
        //        if (cd.CartID == cartid)
        //            sum += cd.Quantity * cd.Product.UnitPrice;
        //    }
        //    SetTotalPrice.TotalPrice = sum;

        //    novi.SaveChanges();
        //    novi.Dispose();
        //}
        //public IActionResult LookInCart()
        //{
        //    OnlineShopContext novi = new OnlineShopContext();
        //    var cd = novi.cartdetails.Include(cd => cd.Product).Include(cd => cd.Cart).ToList();
        //    decimal sum = 0;
        //    foreach (CartDetails CD in cd)
        //        sum += CD.Product.UnitPrice * CD.Quantity;

        //    ViewData["suma"] = sum;
        //    ViewData["cartdetails"] = cd;
        //    return View();
        //}
        //public IActionResult RemoveFromCart(int productid, int cartid)
        //{

        //    OnlineShopContext novi = new OnlineShopContext();
        //    novi.cartdetails.Remove(novi.cartdetails.Find(cartid, productid));
        //    novi.SaveChanges();  //nakon sto se obrise zapis gdje je productID i cartID odgovarajuci, ide snimanje izmjena 

        //    CalculateTotalPrice(cartid);//rekalkulacija cijene u košarici.
        //    novi.Dispose();
        //    return View();
        //}
        //public IActionResult DeleteCart()  //naknadno ce se primati userid da se brise korpa iskljucivo tog korisnika, kao i za sve prethodne funkcionalnosti koje ce biti vezane i za userid
        //{
        //    OnlineShopContext novi = new OnlineShopContext();
        //    List<Cart> lista = novi.cart.ToList();
        //    var ID = lista.Last().CartID;  //pomocu .Find uz UserID ce se naci zapis poslije kad bude vezana korpa za userid
        //    //List<CartDetails> cartdetails = novi.cartdetails.Include(p=>p.Product).Include(c=>c.Cart).ToList();
        //    novi.cartdetails.Include(p => p.Product).Include(c => c.Cart).ToList().RemoveAll(c => c.CartID == ID); //obrisani svi zapisi iz CartDetails vezani za tu Korpu
        //    novi.SaveChanges();
        //    novi.cart.Remove(lista.SingleOrDefault(c => c.CartID == ID));
        //    novi.SaveChanges();
        //    novi.Dispose();
        //    return View();
        //}



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
