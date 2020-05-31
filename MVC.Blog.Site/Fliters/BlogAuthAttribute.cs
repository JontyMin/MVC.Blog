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