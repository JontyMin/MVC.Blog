using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVC.Blog.DTO;

namespace MVC.Blog.IBLL
{
    public interface IArticleManager
    {
        Task CreateArticle(string title, string content, Guid[] categoryIds, Guid userId);
        Task CreateCategory(string name, Guid userId);
        Task<List<CategoryDto>> GetAllCategories(Guid userId);
        Task<List<ArticleDto>> GetAllArticles(Guid userId);
        Task<List<ArticleDto>> GetAllArticlesByEmail(string email);
        Task<List<ArticleDto>> GetAllArticlesByCategoryId(Guid categoryId);
        Task RemoveCategory(string name);
        Task EditCategory(Guid categoryId, string newCategoryName);
        Task RemoveArticle(Guid articleId);
        Task EditArticle(Guid articleId, string title, string content, Guid[] categoryIds);

        Task<bool> ExistsArticle(Guid articleId);
        Task<ArticleDto> GetArticleById(Guid articleId);
    }
}