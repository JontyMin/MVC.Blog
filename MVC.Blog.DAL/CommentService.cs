using MVC.Blog.IDAL;
using MVC.Blog.Model;

namespace MVC.Blog.DAL
{
    public class CommentService:BaseService<Comment>,ICommentService
    {
        public CommentService() : base(new BlogDbContext())
        {
        }
    }
}