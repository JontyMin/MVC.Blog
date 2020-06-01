using System;
using MVC.Blog.IDAL;
using MVC.Blog.Model;

namespace MVC.Blog.DAL
{
    public class CategoryService:BaseService<Category>,ICategoryService
    {
        public CategoryService() : base(new BlogDbContext())
        {
        }
    }
}