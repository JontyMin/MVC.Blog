using System.Web.Mvc;
using System.Web.Routing;

namespace MVC.Blog.Site.Fliters
{
    public class BlogAuthAttribute:AuthorizeAttribute
    {
        //public bool IsSkip { get; set; } = false;

        /// <summary>
        /// 过滤器方法 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //当用户存储在cookie中且session为空，数据同步
            var value = filterContext.HttpContext.Request.Cookies["userId"]?.Value;
            if (filterContext.HttpContext.Response.Cookies["loginName"] != null&&
                filterContext.HttpContext.Session["loginName"] == null)
            {
                filterContext.HttpContext.Session["loginName"] =
                    filterContext.HttpContext.Request.Cookies["loginName"];
                if (value != null)
                    filterContext.HttpContext.Session["userId"] =
                        value;
            }
            //base.OnAuthorization(filterContext);
            if (!(filterContext.HttpContext.Session["loginName"]!=null || 
                filterContext.HttpContext.Response.Cookies["loginName"]!=null))
            {
                filterContext.Result=new RedirectToRouteResult(new RouteValueDictionary()
                {
                    {"controller","Home"},
                    {"action","Login"}
                });
            }
        }
    }
}