using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using MVC.Blog.BLL;
using MVC.Blog.DTO;
using MVC.Blog.IBLL;
using MVC.Blog.Site.Fliters;
using MVC.Blog.Site.Models.ArticleViewModels;
using MVC.Blog.Site.Views.Article;
using Webdiyer.WebControls.Mvc;

namespace MVC.Blog.Site.Controllers
{
    /// <summary>
    /// 文章
    /// </summary>
    [BlogAuth]
    public class ArticleController : Controller
    {
        // GET: Article
        /// <summary>
        /// 创建栏目
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 获取栏目列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CategoryList()
        {
            IArticleManager articleManager = new ArticleManager();
            return View(await articleManager.GetAllCategories(Guid.Parse(Session["userId"].ToString())));
        }

        /// <summary>
        /// 创建文章
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CreateArticle()
        {
            var userId = Guid.Parse(Session["userId"].ToString());
            ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userId);
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> CreateArticle(CreateArticleViewModel model)
        {
            var userId = Guid.Parse(Session["userId"].ToString());//获取用户id
            if (ModelState.IsValid)
            {
                
                await new ArticleManager().CreateArticle(model.Title, model.Content, model.CategoryIds, userId);
                return RedirectToAction(nameof(ArticleList));
            }
            ModelState.AddModelError("","添加失败");
            ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userId);
            return View(model);//添加失败把数据返回
        }

        /// <summary>
        /// 文章列表分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ArticleList(int pageIndex =1,int pageSize=2)
        {
            //总页码，当前页码，可显示页码数
            var articleManager = new ArticleManager();
            var userId = Guid.Parse(Session["userId"].ToString());//获取用户id
            var articles = await articleManager.GetAllArticles(userId, pageIndex - 1,pageSize);//获取分页数据
            var dataCount = await articleManager.GetDataCount(userId);//获取数据条数
            
            return View(new PagedList<ArticleDto>(articles, pageIndex,pageSize,dataCount));//返回分页list
        }
        /// <summary>
        /// 文章查看
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]  
        public async Task<ActionResult> ArticleDetails(Guid? id)
        {
            var articleManager = new ArticleManager();
            if (id==null || !await articleManager.ExistsArticle(id.Value)) return RedirectToAction(nameof(ArticleList));
            var comments = await articleManager.GetCommentByArticleId(id.Value);
            ViewBag.Comments =comments.AsQueryable();
            return View(await articleManager.GetArticleById(id.Value));

        }
        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> EditArticle(Guid id)
        {
           
            var articleManager = new ArticleManager();
            var data = await articleManager.GetArticleById(id);
            var userId = Guid.Parse(Session["userId"].ToString());
            ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userId);
            return View(new EditArticleViewModel()
            {
                ArticleId = data.ArticleId,
                Title = data.Title,
                Content = data.Content,
                CategoryIds = data.CategoryIds
            });

        }
        [HttpPost]
        public async Task<ActionResult> EditArticle(EditArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IArticleManager articleManager = new ArticleManager();
                await articleManager.EditArticle(model.ArticleId, model.Title, model.Content, model.CategoryIds);
                return RedirectToAction(nameof(ArticleList));
            }
            var userId = Guid.Parse(Session["userId"].ToString());
            ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userId);
            return View(model);
        }
        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> GoodCount(Guid id)
        {
            IArticleManager articleManager = new ArticleManager();
            await articleManager.GoodCount(id);
            return Json(new {resule = "OK"});
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> BadCount(Guid id)
        {
            IArticleManager articleManager = new ArticleManager();
            await articleManager.BadCount(id);
            return Json(new { resule = "OK" });
        }

        public async Task<ActionResult> AddComment(CreateCommentViewModel model)
        {
            var userId = Guid.Parse(Session["userId"].ToString());
            IArticleManager articleManager = new ArticleManager();
            await articleManager.CreateComment(userId, model.Id, model.Content);
            return Json(new {resule = "OK"});
        }
    }
}