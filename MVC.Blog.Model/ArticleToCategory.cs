using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Blog.Model
{
    /// <summary>
    /// 文章栏目实体类
    /// </summary>
    public class ArticleToCategory:BaseEntity
    {
        /// <summary>
        /// 类别id
        /// </summary>
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// 文章id
        /// </summary>
        [ForeignKey(nameof(Article))]
        public Guid ArticleId { get; set; }
        /// <summary>
        /// 文章
        /// </summary>
        public Article Article { get; set; }
        
    }
}