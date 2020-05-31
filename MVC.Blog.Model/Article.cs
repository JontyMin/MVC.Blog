using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Blog.Model
{
    public class Article:BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        public String Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Column(TypeName = "ntext"),Required]
        public String Content { get; set; }
        /// <summary>
        /// 用户外键
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        public int GoodCount { get; set; }
        /// <summary>
        /// 反对数
        /// </summary>
        public int BadCount { get; set; }
    }
}