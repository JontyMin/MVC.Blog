using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public Task<List<ArticleDto>> GetAllArticles(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ArticleDto>> GetAllArticlesByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<List<ArticleDto>> GetAllArticlesByCategoryId(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCategory(string name)
        {
            throw new NotImplementedException();
        }

        public Task EditCategory(Guid categoryId, string newCategoryName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveArticle(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public Task EditArticle(Guid articleId, string title, string content, Guid[] categoryIds)
        {
            throw new NotImplementedException();
        }
    }
}