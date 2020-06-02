using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.BLL;
using MVC.Blog.IBLL;
using MVC.Blog.Site.Fliters;
using MVC.Blog.Site.Models.ArticleViewModels;
using MVC.Blog.Site.Views.Article;

namespace MVC.Blog.Site.Controllers
{
    [BlogAuth]
    public class ArticleController : Controller
    {
        // GET: Article
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                IArticleManager articleManager = new ArticleManager();
                articleManager.CreateCategory(model.CategoryName, Guid.Parse(Session["userId"].ToString()));
                return RedirectToAction(nameof(CategoryList));
            }
            ModelState.AddModelError("","您输入的信息有误");
            return View(model);

        }
        [HttpGet]
        public async Task<ActionResult> CategoryList()
        {
            IArticleManager articleManager = new ArticleManager();
            return View(await articleManager.GetAllCategories(Guid.Parse(Session["userId"].ToString())));
        }
        [HttpGet]
        public async Task<ActionResult> CreateArticle()
        {
            var userId = Guid.Parse(Session["userId"].ToString());
            ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userId);
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateArticle(CreateArticleViewModel model)
        {
            var userId = Guid.Parse(Session["userId"].ToString());
            if (ModelState.IsValid)
            {
                
                await new ArticleManager().CreateArticle(model.Title, model.Content, model.CategoryIds, userId);
                return RedirectToAction(nameof(ArticleList));
            }
            ModelState.AddModelError("","添加失败");
            ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userId);
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> ArticleList()
        {
            var userId = Guid.Parse(Session["userId"].ToString());
            var articles = await new ArticleManager().GetAllArticles(userId);
            return View(articles);
        }

        [HttpGet]
        public async Task<ActionResult> ArticleEdit(Guid? id)
        {
            var articleManager = new ArticleManager();
            if (id==null || !await articleManager.ExistsArticle(id.Value)) return RedirectToAction(nameof(ArticleList));

            return View(await articleManager.GetArticleById(id.Value));

        }

    }
}