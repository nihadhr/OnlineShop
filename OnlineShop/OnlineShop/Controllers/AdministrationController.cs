using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.ViewModels;
using OnlineShopPodaci;
using OnlineShopPodaci.Model;

namespace OnlineShop.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdministrationController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private IOrder _order;
        private OnlineShopContext _database;
        public AdministrationController(UserManager<User> userManager,RoleManager<Role>roleManager,IOrder order,OnlineShopContext _database)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _order = order;
            this._database = _database;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowOrders()
        {
            var model = _database.order.Include(a => a.User).Select(s => new ShowOrdersVM
            {
                OrderID = s.OrderID,
                OrderDate = s.OrderDate,
                ShipTime = s.ShipDate,
                IsShipped = s.IsShipped,
                UserID = s.UserID,
                UserInfo = s.User.Name + " " + s.User.Surname + " | " + s.User.Adress + " | " + s.User.PhoneNumber,
                TotalPrice = s.TotalPrice,
                //Items = _order.GetAllCartItemsByUser(s.UserID).Select(p => new ShowOrdersVM.Rows
                //{
                //    ProductName = p.Product.ProductName,
                //    Quantity = p.Quantity
                //}).ToList()

                //Items = _database.orderdetails.Where(g => g.OrderID == s.OrderID).Include(p => p.Product).ToList().Select(o => new ShowOrdersVM.Rows
                //{
                //    ProductName = o.Product.ProductName,
                //    Quantity = o.Quantity
                //}).ToList()
            }).ToList();
            foreach(var x in model)  //ovaj dio optimizovat !! !!!
            {
                x.Items = _database.orderdetails.Include(p => p.Product).Where(g => g.OrderID == x.OrderID).Select(o => new ShowOrdersVM.Rows
                {
                    ProductName = o.Product.ProductName,
                    Quantity = o.Quantity
                }).ToList();
            }
            return View(model);
        }
        public IActionResult ShipOrder(int orderid)  //da,ne clickable
        {
            return View();
        }

        public async Task<IActionResult> ListOfCustomers()
        {
            List<ListOfCustomersVM> model = new List<ListOfCustomersVM>();
            foreach(var user in userManager.Users)
            {
                if(await userManager.IsInRoleAsync(user,"Customer"))
                {
                    model.Add(new ListOfCustomersVM
                    {
                        Id=user.Id,
                        Email=user.Email,
                        Firstname=user.Name,
                        LastName=user.Surname,
                        PhoneNumber=user.PhoneNumber
                    });
                }

            }
            return View(model);
        }
        public async Task<IActionResult> SetForAdmin(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            await userManager.RemoveFromRoleAsync(user, "Customer");
            await userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("Index", "Home");

        }

    }
}