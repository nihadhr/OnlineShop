﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.ViewModels;
using OnlineShopPodaci;
using OnlineShopPodaci.Model;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;


namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private OnlineShopContext _db;
        private readonly RoleManager<Role> roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, OnlineShopContext db, 
            RoleManager<Role>roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _db = db;
            this.roleManager = roleManager;
        }

        [HttpGet]       
        public IActionResult Register()
        {
            var model = new RegisterVM { gradovi = _db.city.Select(c => new SelectListItem {Value=c.CityID.ToString(),Text=c.CityName })
                .ToList(),
            genders = _db.gender.Select(c => new SelectListItem { Value = c.GenderID.ToString(), Text = c._Gender})
                .ToList()
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if(ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Name=model.FirstName,
                    Surname=model.LastName,
                    BirthDate=model.BirthDate,
                    Adress=model.Adress,
                    CityID=model.GradID,
                    PhoneNumber=model.PhoneNumber,
                    GenderID=model.GenderID

                };

                var result = await userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    var role = await roleManager.FindByIdAsync(1.ToString());
                    await userManager.AddToRoleAsync(user, role.Name);

                    return RedirectToAction("Index", "Home");
                }
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email,model.Password,model.RememberMe,false);
                if (result.Succeeded)
                    if (!string.IsNullOrEmpty(returnUrl))
                        return LocalRedirect(returnUrl);
                    else 
                    {
                        var user = await userManager.FindByNameAsync(model.Email);
                        if (await userManager.IsInRoleAsync(user, "Customer"))
                            return RedirectToAction("Index", "Home");
                        else
                            return RedirectToAction("Index", "Administration");
                    }

                ModelState.AddModelError("", "Neuspješan pokušaj prijave!");
            }
            return View(model);
        }
        public IActionResult Contact()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Ahmed", "ahmed.terzic99@gmail.com"));
            message.To.Add(new MailboxAddress("Ahmed", "ahmedterzic@hotmail.com"));
            message.Subject = "OnlineShop Service Notification";
            message.Body = new TextPart("plain")
            {
                Text="Obavijest od Online Shop servisa! Vaša uloga administratora na web aplikaciji je uklonjena kao i pristup svim administratorskim funkcionalnostima!"
            };
            using(var client=new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("ahmed.terzic99@gmail.com", "1Kz0481!");
                client.Send(message);
                client.Disconnect(true);

            }

            return View();
        }
    }

}