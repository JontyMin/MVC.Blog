using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Blog.Site.Models.ArticleViewModels
{
    public class EditArticleViewModel
    {
        public Guid ArticleId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public Guid[] CategoryIds { get; set; }
    }
}