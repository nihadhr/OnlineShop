using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewModels;
using OnlineShopPodaci;
using OnlineShopPodaci.Model;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private IProduct product;
        private OnlineShopContext _database;

        public ProductController(IProduct p,OnlineShopContext b)
        {
            product = p;
            _database = b;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Show()
        {
            var proizvodi = product.GetAllProducts();
            var productForView = proizvodi.Select(p => new ShowProductForManage
            {
                ProductID = p.ProductID,
                ProductNumber = p.ProductNumber,
                SubCategoryName = p.SubCategory.SubCategoryName,
                ManufacturerName = p.Manufacturer.ManufacturerName,
                ProductName = p.ProductName,
                Description = p.Description,
                UnitPrice = p.UnitPrice
            });
            var data = new SohwProductForManageLIST { ListOfProducts = productForView };
                
            

            return View(data);
        }


        public IActionResult Delete(int ID)
        {
            product.RemoveProduct(ID);
            return Redirect("/Product/Show");

        }

        public IActionResult AddProduct(int ProductID)
        {
            Product temp;
            if (ProductID != 0)
                temp= _database.product.Find(ProductID);
            else
                temp= new Product();
            var data = new AddOrUpdateProductVM {
                ProductID=temp.ProductID,
                ProductNumber=temp.ProductNumber,
                SubCategoryID=temp.SubCategoryID,
                Subcategories=_database.subcategory.Select(s=> new SelectListItem { Value = s.SubCategoryID.ToString(), Text = s.SubCategoryName }).ToList(),
                ManufacturerID = temp.SubCategoryID,
                Manufacturers = _database.manufacturer.Select(s => new SelectListItem { Value = s.ManufacturerID.ToString(), Text = s.ManufacturerName }).ToList(),
                ProductName=temp.ProductName,
                ImageURL=temp.ImageUrl,
                Description=temp.Description,
                UnitPrice=temp.UnitPrice
            };
            return View(data);
        }

        public IActionResult SaveProduct(AddOrUpdateProductVM model)
        {
            Product neki;

            if ( model.ProductID== 0)
            {
                neki = new Product();
                _database.product.Add(neki);
            }
            else
                neki = _database.product.Find(model.ProductID);

            _database.Update(neki);
            neki.ProductNumber = model.ProductNumber;
            neki.SubCategoryID = model.SubCategoryID;
            neki.ManufacturerID = model.ManufacturerID;
            neki.ProductName = model.ProductName;
            neki.ImageUrl = model.ImageURL;
            neki.Description = model.Description;
            neki.UnitPrice = model.UnitPrice;

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

            var categories = novi.category.ToList();
            ViewData["test"] = categories;

            novi.Dispose();
            return View(categories);
        }

        public IActionResult ShowSubcategories(int ID)
        {
            OnlineShopContext _database = new OnlineShopContext();

            ShowSubCategoriesVM lista = new ShowSubCategoriesVM { CategoryName = _database.category.Find(ID).CategoryName, 
                Subcategorylist = _database.subcategory.Where(s => s.CategoryID == ID).ToList() };
            return View("ShowSubcategories",lista);
        }

        //public IActionResult ShowProducts(int ID)
        //{
        //    OnlineShopContext baza = new OnlineShopContext();

        //    ShowProductsVM products = new ShowProductsVM { products = baza.product.Where(p => p.SubCategoryID == ID).Include(a=>a.Manufacturer).ToList() };

        //    return View(products);
        //}





    }
}