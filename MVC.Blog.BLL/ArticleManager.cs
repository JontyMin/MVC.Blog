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

        public async Task<List<ArticleDto>> GetAllArticles(Guid userId)
        {
            using (var articleService=new ArticleService())
            {
                var list = await articleService.GetAll().Include(x=>x.User).Where(x => x.UserId == userId)
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

        public async Task<bool> ExistsArticle(Guid articleId)
        {
            using (IArticleService articleService=new ArticleService())
            {
                return await articleService.GetAll().AnyAsync(x => x.Id == articleId);
            }
        }

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