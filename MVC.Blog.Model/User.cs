using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace MVC.Blog.Model
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class User:BaseEntity
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        [Required,StringLength(40),Column(TypeName = "varchar")]
        public String Email { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required,StringLength(30),Column(TypeName = "varchar")]
        public string Password { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [Required,StringLength(300),Column(TypeName = "varchar")]
        public string ImagePath { get; set; }
        /// <summary>
        /// 粉丝数量
        /// </summary>
        public int FansCount { get; set; }
        /// <summary>
        /// 关注数
        /// </summary>
        public int FocusCount { get; set; }
        /// <summary>
        /// 网站名称
        /// </summary>
        public string SiteName { get; set; }
    }
}