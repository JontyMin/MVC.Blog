using System;
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
    /// <summary>
    /// 主页
    /// </summary>
    public class HomeController : Controller
    {
        
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IUserManager userManager = new UserManager();
                Guid userId;
                if ( userManager.Login(model.LoginName, model.LoginPwd,out userId))
                {
                    if (model.RememberMe)//记住我
                    {
                        //cookie
                        Response.Cookies.Add(new HttpCookie("loginName")
                        {
                            Value = model.LoginName,
                            Expires = DateTime.Now.AddDays(7)
                        });
                        Response.Cookies.Add(new HttpCookie("userId")
                        {
                            Value = userId.ToString(),
                            Expires = DateTime.Now.AddDays(7)
                        });
                    }
                    else
                    {
                        //session
                        Session["loginName"] = model.LoginName;
                        Session["userId"] = userId;
                    }

                    return RedirectToAction(nameof(Index));
                    //return Redirect("/Home/Index");
                }

               
            }
            return View(model);
        }
    }
}