using MVC.Blog.IDAL;
using MVC.Blog.Model;

namespace MVC.Blog.DAL
{
    public class ArticleToCategoryService:BaseService<ArticleToCategory>,IArticleToCategoryService
    {
        public ArticleToCategoryService() : base(new BlogDbContext())
        {
        }
    }
}