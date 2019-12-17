using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopPodaci;
using OnlineShopPodaci.Model;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
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

        public IActionResult AddProduct(int ProductID)
        {
            OnlineShopContext _database = new OnlineShopContext();

            if (ProductID != 0)
            {
                Product old = _database.product.Find(ProductID);
                ViewData["data"] = old;
            }
            else
            {
                Product newProduct = new Product();
                ViewData["data"] = newProduct;
            }
            ViewData["key2"] = _database.subcategory.ToList();
            ViewData["key3"] = _database.manufacturer.ToList();

            return View("AddProduct");
        }

        public IActionResult SaveProduct(int ProductID, string productNumber, int subCategoryID, int manufacturerID, string productName, string imageURL, string description, decimal unitPrice)
        {
            OnlineShopContext _database = new OnlineShopContext();

            Product neki;

            if (ProductID == 0)
            {
                neki = new Product();
                _database.product.Add(neki);
            }
            else
            {
                neki = _database.product.Find(ProductID);
            }

            neki.ProductNumber = productNumber;
            neki.SubCategoryID = subCategoryID;
            neki.ManufacturerID = manufacturerID;
            neki.ProductName = productName;
            neki.ImageUrl = imageURL;
            neki.Description = description;
            neki.UnitPrice = unitPrice;

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
    }
}