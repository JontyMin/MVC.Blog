using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MVC.Blog.DAL;
using MVC.Blog.DTO;
using MVC.Blog.IBLL;
using MVC.Blog.IDAL;
using MVC.Blog.Model;

namespace MVC.Blog.BLL
{
    public class ArticleManager:IArticleManager
    {
        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="categoryIds">所属栏目</param>
        /// <param name="userId">创建用户</param>
        /// <returns></returns>
        public async Task CreateArticle(string title, string content, Guid[] categoryIds, Guid userId)
        {
            using (var articleService = new ArticleService())
            {
                var article = new Article()
                {
                    Title = title,
                    Content = content,
                    UserId = userId
                };
                await articleService.CreateAsync(article);

                Guid articleId = article.Id;

                using (var articleToCategoryService = new ArticleToCategoryService())
                {
                    //遍历栏目
                    foreach (var categoryId in categoryIds)
                    {
                        await articleToCategoryService.CreateAsync(new ArticleToCategory()
                        {
                            ArticleId = articleId,
                            CategoryId = categoryId
                        },false);
                    }

                    await articleToCategoryService.Save();
                }
            }
        }
        /// <summary>
        /// 创建栏目
        /// </summary>
        /// <param name="name">栏目名</param>
        /// <param name="userId">用户</param>
        /// <returns></returns>
        public async Task CreateCategory(string name, Guid userId)
        {
            using (var categoryService = new CategoryService())
            {
                await categoryService.CreateAsync(new Category()
                {
                    CategoryName = name,
                    UserId = userId
                });
            }
        }
        /// <summary>
        /// 根据用户获取所有栏目
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public async Task<List<CategoryDto>> GetAllCategories(Guid userId)
        {
            using (ICategoryService categoryService = new CategoryService())
            {
                return await categoryService.GetAll().Where(x=>x.UserId==userId).Select(x=>new CategoryDto()
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName
                }).ToListAsync();
            }
        }
        /// <summary>
        /// 根据用户获取所有文章并分页
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public async Task<List<ArticleDto>> GetAllArticles(Guid userId,int pageIndex,int pageSize)
        {
            using (var articleService=new ArticleService())
            {
                var list = await articleService.GetAllByPageOrder(pageSize, pageIndex, asc:false).Include(x=>x.User).Where(x => x.UserId == userId)
                    .Select(x=>new ArticleDto()
                {
                    Title = x.Title,
                    BadCount = x.BadCount,
                    GoodCount = x.GoodCount,
                    Email = x.User.Email,
                    Content = x.Content,
                    CreateTime = x.CreateTime,
                    ArticleId = x.Id,
                    ImagePath = x.User.ImagePath
                }).ToListAsync();

                using (IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                    foreach (var articleDto in list)
                    {
                        var cates = await articleToCategoryService.GetAll().Include(x=>x.Category).Where(x => x.ArticleId == articleDto.ArticleId).ToListAsync();
                        articleDto.CategoryIds = cates.Select(x => x.CategoryId).ToArray();
                        articleDto.CategoryNames = cates.Select(x => x.Category.CategoryName).ToArray();
                    }

                    return list;
                }
            }
        }

        /// <summary>
        /// 获取用户文章数量
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public async Task<int> GetDataCount(Guid userId)
        {
            using (IArticleService articleService = new ArticleService())
            {
                return await articleService.GetAll().Where(x => x.UserId == userId).CountAsync();
            }
        }
        /// <summary>
        /// 根据邮箱获取文章
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public Task<List<ArticleDto>> GetAllArticlesByEmail(string email)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据栏目获取文章
        /// </summary>
        /// <param name="categoryId">栏目编号</param>
        /// <returns></returns>
        public Task<List<ArticleDto>> GetAllArticlesByCategoryId(Guid categoryId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 移除文章
        /// </summary>
        /// <param name="name">栏目</param>
        /// <returns></returns>
        public Task RemoveCategory(string name)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="categoryId">栏目编号</param>
        /// <param name="newCategoryName">新的栏目名</param>
        /// <returns></returns>
        public Task EditCategory(Guid categoryId, string newCategoryName)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 移除文章
        /// </summary>
        /// <param name="articleId">文章编号</param>
        /// <returns></returns>
        public Task RemoveArticle(Guid articleId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="articleId">文章编号</param>
        /// <param name="title">标题</param>
        /// <param name="content">文章内容</param>
        /// <param name="categoryIds">所属栏目</param>
        /// <returns></returns>
        public Task EditArticle(Guid articleId, string title, string content, Guid[] categoryIds)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据编号查找文章是否存在
        /// </summary>
        /// <param name="articleId">文章编号</param>
        /// <returns></returns>
        public async Task<bool> ExistsArticle(Guid articleId)
        {
            using (IArticleService articleService=new ArticleService())
            {
                return await articleService.GetAll().AnyAsync(x => x.Id == articleId);
            }
        }
        /// <summary>
        /// 根据编号获取文章
        /// </summary>
        /// <param name="articleId">文章编号</param>
        /// <returns></returns>
        public async Task<ArticleDto> GetArticleById(Guid articleId)
        {
            using (IArticleService articleService = new ArticleService())
            {
                var data= await articleService.GetAll().Where(x => x.Id == articleId).Select(x=>new ArticleDto()
                {
                    ArticleId = x.Id,
                    BadCount = x.BadCount,
                    Title = x.Title,
                    Content = x.Content,
                    CreateTime = x.CreateTime,
                    Email = x.User.Email,
                    GoodCount = x.GoodCount,
                    ImagePath = x.User.ImagePath

                }).FirstAsync();

                using (IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                    var cates = await articleToCategoryService.GetAll().Include(x => x.Category)
                        .Where(x => x.ArticleId == data.ArticleId).ToListAsync();
                    data.CategoryIds = cates.Select(x => x.CategoryId).ToArray();
                    data.CategoryNames = cates.Select(x => x.Category.CategoryName).ToArray();

                    return data;
                }
            }
        }
    }
}