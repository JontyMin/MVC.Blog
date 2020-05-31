using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Blog.Model
{
    /// <summary>
    /// 基类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }=DateTime.Now;
        /// <summary>
        /// 是否被删除，伪删除
        /// </summary>
        public bool IsRemove { get; set; }
    }
}
