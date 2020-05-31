﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.BLL;
using MVC.Blog.IBLL;
using MVC.Blog.Site.Fliters;
using MVC.Blog.Site.Models;
namespace MVC.Blog.Site.Controllers
{
    public class HomeController : Controller
    {
        [BlogAuth]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)return View();
            IUserManager userManager = new UserManager();
            await userManager.Register(model.Email, model.Password);
            return Content("注册成功");
            

            
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IUserManager userManager = new UserManager();
                if (await userManager.Login(model.LoginName, model.LoginPwd))
                {
                    if (model.RememberMe)
                    {
                        Response.Cookies.Add(new HttpCookie("loginName")
                        {
                            Value = model.LoginName,
                            Expires = DateTime.Now.AddDays(7)
                        });
                    }
                    else
                    {
                        Session["loginName"] = model.LoginName;
                    }

                    return RedirectToAction(nameof(Index));
                    //return Redirect("/Home/Index");
                }

               
            }
            return View(model);
        }
    }
}