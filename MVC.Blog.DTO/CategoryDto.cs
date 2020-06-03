using System;

namespace MVC.Blog.DTO
{
    /// <summary>
    /// 栏目
    /// </summary>
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
    }
}