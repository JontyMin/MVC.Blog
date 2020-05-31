using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Blog.Model
{
    /// <summary>
    /// 评论实体类
    /// </summary>
    public class Comment:BaseEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        [Required,StringLength(800)]
        public String Content { get; set; }
        /// <summary>
        /// 文章id
        /// </summary>
        [ForeignKey(nameof(Article))]
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }

    }
}