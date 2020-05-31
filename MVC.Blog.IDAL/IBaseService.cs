using System;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Blog.IDAL
{
    using MVC.Blog.Model;
    public interface IBaseService<T>:IDisposable where T:BaseEntity
    {
        /// <summary>
        /// 创建资源
        /// </summary>
        /// <param name="model"></param>
        /// <param name="saved"></param>
        /// <returns></returns>
        Task CreateAsync(T model, bool saved=true);

        /// <summary>
        /// 修改资源
        /// </summary>
        /// <param name="model"></param>
        /// <param name="saved"></param>
        /// <returns></returns>
        Task EditAsync(T model, bool saved = true);

        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="model"></param>
        /// <param name="saved"></param>
        /// <returns></returns>
        Task RemoveAsync(T model, bool saved = true);

        /// <summary>
        /// 根据id删除资源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saved"></param>
        /// <returns></returns>
        Task RemoveAsync(Guid id, bool saved = true);
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        Task Save();
        /// <summary>
        /// 根据id获取单个资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetOneByIdAsync(Guid id);
        /// <summary>
        /// 获取所有资源
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        IQueryable<T> GetAllByPage(int pageSize = 10,int pageIndex=0);
        /// <summary>
        /// 排序查询
        /// </summary>
        /// <param name="asc"></param>
        /// <returns></returns>
        IQueryable<T> GetAllOrder(bool asc = false);
        /// <summary>
        /// 分页并排序
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        IQueryable<T> GetAllByPageOrder(int pageSize = 10, int pageIndex = 0, bool asc = false);

    }
}