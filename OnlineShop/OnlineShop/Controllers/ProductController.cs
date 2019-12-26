using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewModels;
using OnlineShopPodaci;
using OnlineShopPodaci.Model;
using X.PagedList;
using X.PagedList.Mvc.Core;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private IProduct product;
        private OnlineShopContext _database;
        private readonly IHostingEnvironment hosting;


        public ProductController(IProduct p, OnlineShopContext b, IHostingEnvironment hostingEnvironment)
        {
            product = p;
            _database = b;
            hosting = hostingEnvironment;
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

        private string SaveFile(IFormFile file)
        {
            string uploadFileName = null;
            string filePath = null;
            if (file != null)
            {
                string uploadFoloder = Path.Combine(hosting.WebRootPath, "images");
                uploadFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                filePath = Path.Combine(uploadFoloder, uploadFileName);
                file.CopyTo(new FileStream(filePath, FileMode.Create));
                return "~/images/" + uploadFileName;
            }
            return null;
        }

        public IActionResult AddProduct(int ProductID)
        {
            Product temp;
            if (ProductID != 0)
                temp= _database.product.Find(ProductID);
            else
                temp= new Product();
            var data = new AddOrUpdateProductVM
            {
                ProductID=temp.ProductID,
                ProductNumber=temp.ProductNumber,
                SubCategoryID=temp.SubCategoryID,
                Subcategories=_database.subcategory.Select(s=> new SelectListItem { Value = s.SubCategoryID.ToString(), Text = s.SubCategoryName }).ToList(),
                ManufacturerID = temp.ManufacturerID,
                Manufacturers = _database.manufacturer.Select(s => new SelectListItem { Value = s.ManufacturerID.ToString(), Text = s.ManufacturerName }).ToList(),
                ProductName=temp.ProductName,
                //Image = temp.ImageUrl,
                Description =temp.Description,
                UnitPrice=temp.UnitPrice
            };
            return View(data);
        }

        public IActionResult SaveProduct(AddOrUpdateProductVM model)
        {
            if (ModelState.IsValid)
            {
                string uniquefileName = null;
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(hosting.WebRootPath, "images");
                    uniquefileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniquefileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Product neki;
                if (model.ProductID == 0)
                {
                    neki = new Product();
                    _database.product.Add(neki);
                }
                else
                    neki = _database.product.Find(model.ProductID);

                neki.ProductNumber = model.ProductNumber;
                neki.SubCategoryID = model.SubCategoryID;
                neki.ManufacturerID = model.ManufacturerID;
                neki.ProductName = model.ProductName;
                neki.ImageUrl = uniquefileName;                  
                neki.Description = model.Description;
                neki.UnitPrice = model.UnitPrice;

                product.AddProduct(neki);
            }
            _database.SaveChanges();
            return Redirect("/Product/Show");
        }

        public IActionResult AddManufacturer(int ProductID)
        {
            ViewData["idP"] =ProductID;
            return View("AddManufacturer");
        }

        public IActionResult SaveManufacturer(string manufacturerName, string logoURL,int ProductID)
        {
            Manufacturer manufacturer = new Manufacturer
            {
                ManufacturerName = manufacturerName,
                LogoUrl = logoURL
            };

            _database.manufacturer.Add(manufacturer);
            _database.SaveChanges();
            return Redirect($"/Product/AddProduct?={ProductID}");

        }
        public IActionResult Show2()       
        {
           List<ShowCategoriesVM >data = _database.category.Select(c => new ShowCategoriesVM { CategoryID = c.CategoryID, CategoryName = c.CategoryName }).ToList();
            
            return View(data);
        }

        public IActionResult ShowSubcategories(int id,string search,int? page)          // ID kategorije
        {
            var c = _database.subcategory.Where(s => s.CategoryID == id).
                Select(s => new ShowSubCategoriesVM
                {
                 ID=id,
                    CategoryName = s.Category.CategoryName,
                    SubCategoryID = s.SubCategoryID,
                    SubCategoryName = s.SubCategoryName
                }).Where(e => e.SubCategoryName.StartsWith(search) || search == null);

            IPagedList<ShowSubCategoriesVM> lista = c.ToPagedList(page ?? 1, 6);

            return View(lista);
        }

        public IActionResult ShowProducts(int ID)       // ID podkategorije 
        {

            List<ShowProductsVM> products = _database.product.Where(s => s.SubCategoryID == ID).
                Select(p => new ShowProductsVM
            {
                ProductID=p.ProductID,
                ProductName=p.ProductName,
                ManufacturerName=p.Manufacturer.ManufacturerName,
                UnitPrice=p.UnitPrice,
                UnitsInStock=p.UnitsInStock

            }).ToList();

            return View(products);
        }

        public IActionResult ProductDetails(int ID)     // ID proizvoda
        {
            ProductDetailsVM model = _database.product.Where(s=>s.ProductID==ID).Select(a => new ProductDetailsVM
            {
                ProductID =a.ProductID,
                ProductName=a.ProductName,
                ProductNumber =a.ProductNumber,
                SubCategoryName=a.SubCategory.SubCategoryName,
                ManufacturerName=a.Manufacturer.ManufacturerName,
                ImageUrl=a.ImageUrl,
                Description=a.Description,
                UnitPrice=a.UnitPrice,
                UnitsInStock=a.UnitsInStock         // uvijek je 0 tamo
            }).SingleOrDefault();

            return View(model);
        }
    }
}