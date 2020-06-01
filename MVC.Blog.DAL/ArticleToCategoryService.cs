using MVC.Blog.IDAL;
using MVC.Blog.Model;

namespace MVC.Blog.DAL
{
    public class ArticleToCategoryService:BaseService<ArticleToCategory>,IArticleToCategory
    {
        public ArticleToCategoryService() : base(new BlogDbContext())
        {
        }
    }
}