using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Blog.Site.Views.Article
{
    /// <summary>
    /// 创建文章视图模型
    /// </summary>
    public class CreateArticleViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Display(Name = "文章类别")]
        public Guid[] CategoryIds { get; set; }
    }
}