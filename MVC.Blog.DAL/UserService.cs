using MVC.Blog.IDAL;
using MVC.Blog.Model;

namespace MVC.Blog.DAL
{
    public class UserService:BaseService<User>,IUserService
    {
        public UserService() : base(new BlogDbContext())
        {
        }
    }
}