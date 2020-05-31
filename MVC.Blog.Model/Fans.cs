using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Blog.Model
{
    /// <summary>
    /// 粉丝实体类
    /// </summary>
    public class Fans:BaseEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// 关注用户Id
        /// </summary>
        [ForeignKey(nameof(FocusUser))]
        public Guid FocusUserId { get; set; }
        public User  FocusUser { get; set; }

    }
}