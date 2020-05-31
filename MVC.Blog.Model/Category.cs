using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Blog.Model
{
    /// <summary>
    /// 栏目实体类
    /// </summary>
    public class Category:BaseEntity
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        public String CategoryName { get; set; }
        /// <summary>
        /// 用户外键
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }
    }
}