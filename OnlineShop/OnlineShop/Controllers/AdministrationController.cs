using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.ViewModels;
using OnlineShopPodaci;
using OnlineShopPodaci.Model;
using X.PagedList;

namespace OnlineShop.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdministrationController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private IOrder _order;
        private OnlineShopContext _database;
       

        public AdministrationController(UserManager<User> userManager,RoleManager<Role>roleManager,IOrder order,
            OnlineShopContext _database)
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
            return View();
        }

       
        public IActionResult GetOrders()
        {
            var model = _database.order.Include(i=>i.OrderStatus).Include(a => a.User).Select(s => new ShowOrdersVM
            {
                OrderID = s.OrderID,
                OrderDate = s.OrderDate,
                ShipTime = s.ShipDate,
                OrderStatusID=s.OrderStatusID,
                Status=s.OrderStatus.Status,
                UserID = s.UserID,
                UserInfo = s.User.Name + " " + s.User.Surname + " | " + s.User.Adress + " | " + s.User.PhoneNumber,
                TotalPrice = s.TotalPrice

            }).ToList();

            return PartialView(model);
        }
        public IActionResult EditOrder(int id)  
        {
            //ovdje ide neki VM
            var order = _database.order.FirstOrDefault(s=>s.OrderID==id);
            var User = _database.user.Find(order.UserID);
            var model = new EditOrderVM {
                OrderID = id,
                UserId = order.UserID,
                //IsShipped = order.IsShipped,
                OrderDate = order.OrderDate.ToString(),
                UserInfo = User.Name + " " + User.Surname + ", " + User.Adress + "| " + User.PhoneNumber,
                items = _database.orderdetails.Include(i => i.Product).Where(w => w.OrderID == id).Select(o => new EditOrderVM.Rows
                {
                    ProductID = o.ProductID,
                    ProductName = o.Product.ProductName,
                    Quantity = o.Quantity,
                   
                }).ToList(),

            };
            return PartialView(model);
        }
        
     
        
        //public IActionResult SaveOrderChanges(EditOrderVM model)
        //{
        //    var order = _database.order.Include(u => u.User).FirstOrDefault(s => s.OrderID == model.Id);
        //    order.IsShipped = true;   
        //    order.ShipDate = DateTime.Now;
        //    _database.SaveChanges();
        //    return Redirect("GetOrders");
        //}




        public async Task<IActionResult> ListOfCustomers(int? page)
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
            IPagedList<ListOfCustomersVM> lista = model.ToPagedList(page ?? 1, 5);
            return View(lista);
        }
        public async Task<IActionResult> SetForAdmin(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            await userManager.RemoveFromRoleAsync(user, "Customer");
            await userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("ListOfCustomers", "Administration");

        }
        public IActionResult UserDetails(int id)
        {

            var u= _database.user.Find(id);
            UserDetailsVM model = new UserDetailsVM
            {
                Id = id,
                Name = u.Name,
                Surname = u.Surname,
                BirthDate = u.BirthDate,
                Adress = u.Adress,
                PhoneNumber = u.PhoneNumber,
                //CityName = u.City.CityName,
                //Gender = u.Gender._Gender,
                Email=u.Email
            };

            return View(model);

        }

    }
}