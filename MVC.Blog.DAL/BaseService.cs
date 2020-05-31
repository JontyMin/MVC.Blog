using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MVC.Blog.IDAL;
using MVC.Blog.Model;

namespace MVC.Blog.DAL
{
    public class BaseService<T>:IBaseService<T> where T:BaseEntity,new()
    {
        private readonly BlogDbContext _dbContext;

        public BaseService(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 创建资源
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="saved">是否保存</param>
        /// <returns></returns>
        public async Task CreateAsync(T model, bool saved = true)
        {
            _dbContext.Set<T>().Add(model);
            if (saved) await _dbContext.SaveChangesAsync();

        }
        /// <summary>
        /// 更新资源
        /// </summary>
        /// <param name="model"></param>
        /// <param name="saved"></param>
        /// <returns></returns>
        public async Task EditAsync(T model, bool saved = true)
        {
            _dbContext.Configuration.ValidateOnSaveEnabled = false;
            _dbContext.Entry(model).State = EntityState.Modified;
            if (saved)
            {
                await _dbContext.SaveChangesAsync();
                _dbContext.Configuration.ValidateOnSaveEnabled = false;
            }

        }

        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="model"></param>
        /// <param name="saved"></param>
        /// <returns></returns>
        public async Task RemoveAsync(T model, bool saved = true)
        {
            await RemoveAsync(model.Id, saved);
        }
        /// <summary>
        /// 根据id删除资源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saved"></param>
        /// <returns></returns>
        public async Task RemoveAsync(Guid id, bool saved = true)
        {
            _dbContext.Configuration.ValidateOnSaveEnabled = false;
            var t = new T() { Id =id };
            _dbContext.Entry(t).State = EntityState.Unchanged;
            t.IsRemove = true;
            if (saved)
            {
                await _dbContext.SaveChangesAsync();
                _dbContext.Configuration.ValidateOnSaveEnabled = true;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
            _dbContext.Configuration.ValidateOnSaveEnabled = true;
        }

        /// <summary>
        /// 获取单个资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetOneByIdAsync(Guid id)
        {
           return await GetAll().FirstAsync(m => m.Id == id);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().Where(m => !m.IsRemove).AsNoTracking();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllByPage(int pageSize = 10, int pageIndex = 0)
        {
            return GetAll().Skip(pageSize * pageIndex)
                .Take(pageSize);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="asc"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllOrder(bool asc = false)
        {
            var data = GetAll();
            data = asc ? data.OrderBy(m => m.CreateTime) : data.OrderByDescending(m => m.CreateTime);
            return data;
        }

        /// <summary>
        /// 分页并排序
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllByPageOrder(int pageSize = 10, int pageIndex = 0, bool asc = false)
        {
            return GetAllOrder(asc).Skip(pageSize * pageIndex).Take(pageSize);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}