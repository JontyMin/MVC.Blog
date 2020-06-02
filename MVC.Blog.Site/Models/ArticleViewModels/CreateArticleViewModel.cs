using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Blog.Site.Views.Article
{
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