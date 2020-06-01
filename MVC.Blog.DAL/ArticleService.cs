using MVC.Blog.IDAL;
using MVC.Blog.Model;

namespace MVC.Blog.DAL
{
    public class ArticleService:BaseService<Article>,IArticleService
    {
        public ArticleService() : base(new BlogDbContext())
        {
        }
    }
}