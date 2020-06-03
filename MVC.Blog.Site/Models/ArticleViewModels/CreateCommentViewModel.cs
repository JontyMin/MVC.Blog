using System;

namespace MVC.Blog.Site.Models.ArticleViewModels
{
    /// <summary>
    /// 创建评论
    /// </summary>
    public class CreateCommentViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}