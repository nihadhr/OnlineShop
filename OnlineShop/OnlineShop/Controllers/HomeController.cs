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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


       
        public IActionResult Show()
        {
            OnlineShopContext _database = new OnlineShopContext();

            List<Product> products = _database.product.Include(s => s.SubCategory).Include(f => f.Manufacturer).ToList();

            ViewData["key1"] = products;
            return View("Show");
        }


        public IActionResult Delete(int ID)
        {
            OnlineShopContext _database = new OnlineShopContext();

            Product x = _database.product.Find(ID);
            _database.Remove(x);
            _database.SaveChanges();
            _database.Dispose();
            return View("DeleteView");
        }

        public IActionResult AddProduct()
        {
            OnlineShopContext _database = new OnlineShopContext();

            ViewData["key2"] = _database.subcategory.ToList();
            ViewData["key3"] = _database.manufacturer.ToList();

            return View("AddProduct");
        }

        public IActionResult SaveProduct(string productNumber, int subCategoryID, int manufacturerID, string productName, string imageURL, string description, decimal unitPrice)
        {
            OnlineShopContext _database = new OnlineShopContext();

            Product newProduct = new Product
            {
                ProductNumber = productNumber,
                SubCategory = _database.subcategory.Where(s => s.SubCategoryID == subCategoryID).FirstOrDefault(),
                Manufacturer = _database.manufacturer.Where(f => f.ManufacturerID == manufacturerID).FirstOrDefault(),
                ProductName = productName,
                ImageUrl = imageURL,
                Description = description,
                UnitPrice = unitPrice
            };

            _database.product.Add(newProduct);
            _database.SaveChanges();
            return View("ProductAddMessage");
        }

        public IActionResult AddManufacturer()
        {
            return View("AddManufacturer");
        }

        public IActionResult SaveManufacture(string manufacturerName, string logoURL)
        {
            OnlineShopContext _database = new OnlineShopContext();

            Manufacturer manufacturer = new Manufacturer
            {
                ManufacturerName = manufacturerName,
                LogoUrl = logoURL
            };


            _database.manufacturer.Add(manufacturer);
            _database.SaveChanges();
            return View("ManufactureAddMessage");
        }
        public IActionResult Show2()
        {

            OnlineShopContext novi = new OnlineShopContext();
            List<Product> lista = novi.product.Include(s => s.SubCategory).Include(f => f.Manufacturer).ToList();
            ViewData["kljuc"] = lista;

            novi.Dispose();
            return View();
        }

        public IActionResult AddToCart(int ID, int quantity)
        {

            OnlineShopContext novi = new OnlineShopContext();
            List<Cart> cart = novi.cart.ToList();  //uzimam sve korpe, radi provjere da li već postoji neka i da li da pravim novu ili dodajem na postojeću CartID
            if (cart.Count() == 0)
            {
                Cart newCart = new Cart();  //nema nijedne, kreiram novu Korpu  //bool aktivna korpa ? ?!?
                novi.Add(newCart);
                novi.SaveChanges();
            }

            int IDCart = novi.cart.ToList().Last().CartID;
            // uzimamo ID od korpe, aktivne ili novonastale svejedno jer vraća last zapis

            if (novi.cartdetails.Find(IDCart, ID) != null)
            {
                novi.cartdetails.Find(IDCart, ID).Quantity += quantity;  //ako već postoji samo povećaj količinu za datu
            }
            else
            {
                CartDetails newCartDetail = new CartDetails();
                newCartDetail.CartID = IDCart;
                newCartDetail.ProductID = ID;
                newCartDetail.Quantity = quantity;
                novi.Add(newCartDetail);

            }
            //funkcija za kalkulaciju polja TotalPrice u tabeli Cart

            novi.SaveChanges();
            CalculateTotalPrice(IDCart);
            novi.Dispose();
            return View("ItemAdded");
        }

        public void CalculateTotalPrice(int cartid)
        {
            OnlineShopContext novi = new OnlineShopContext();
            var SetTotalPrice = novi.cart.Find(cartid);
            decimal sum = 0;
            List<CartDetails> lista = novi.cartdetails.Include(s => s.Product).Include(c => c.Cart).ToList();
            foreach (CartDetails cd in lista)
            {
                if (cd.CartID == cartid)
                    sum += cd.Quantity * (novi.product.Find(cd.ProductID).UnitPrice);
            }
            SetTotalPrice.TotalPrice = sum;

            novi.SaveChanges();
            novi.Dispose();
        }
        public IActionResult LookInCart()
        {
            OnlineShopContext novi = new OnlineShopContext();
            ViewData["cartdetails"] = novi.cartdetails.Include(s => s.Product).Include(a => a.Cart).ToList();
            return View();
        }
        public IActionResult RemoveFromCart(int productid,int cartid){

            OnlineShopContext novi = new OnlineShopContext();
            novi.cartdetails.Remove(novi.cartdetails.Find(cartid, productid));
            novi.SaveChanges();  //nakon sto se obrise zapis gdje je productID i cartID odgovarajuci, ide snimanje izmjena 

            CalculateTotalPrice(cartid);//rekalkulacija cijene u košarici.
            novi.Dispose(); 
            return View();
        }

    }
}
