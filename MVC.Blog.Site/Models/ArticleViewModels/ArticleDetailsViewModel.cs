using System;

namespace MVC.Blog.Site.Models.ArticleViewModels
{
    /// <summary>
    /// 如果该viewmodel和DTO中的类一样则省略，直接使用DTO
    /// </summary>
    public class ArticleDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public string[] CategoryIds { get; set; }
        public string[] CategoryName { get; set; }
        public int GoodCount { get; set; }
        public int BadCount { get; set; }
    }
}