using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVC.Blog.DTO;

namespace MVC.Blog.IBLL
{
    /// <summary>
    /// 文章
    /// </summary>
    public interface IArticleManager
    {
        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="title">编号</param>
        /// <param name="content">内容</param>
        /// <param name="categoryIds">所属栏目</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        Task CreateArticle(string title, string content, Guid[] categoryIds, Guid userId);
        /// <summary>
        /// 创建栏目
        /// </summary>
        /// <param name="name">栏目名</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        Task CreateCategory(string name, Guid userId);
        /// <summary>
        /// 获取所有栏目
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        Task<List<CategoryDto>> GetAllCategories(Guid userId);
        /// <summary>
        /// 获取所有文章
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        Task<List<ArticleDto>> GetAllArticles(Guid userId, int pageIndex, int pageSize);
        /// <summary>
        /// 获取文章数量
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        Task<int> GetDataCount(Guid userId);
        /// <summary>
        /// 根据邮箱获取所有文章
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        Task<List<ArticleDto>> GetAllArticlesByEmail(string email);
        /// <summary>
        /// 根据栏目获取文章
        /// </summary>
        /// <param name="categoryId">栏目编号</param>
        /// <returns></returns>
        Task<List<ArticleDto>> GetAllArticlesByCategoryId(Guid categoryId);
        /// <summary>
        /// 移除栏目
        /// </summary>
        /// <param name="name">栏目名</param>
        /// <returns></returns>
        Task RemoveCategory(string name);
        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="categoryId">栏目编号</param>
        /// <param name="newCategoryName">新栏目名</param>
        /// <returns></returns>
        Task EditCategory(Guid categoryId, string newCategoryName);
        /// <summary>
        /// 移除文章
        /// </summary>
        /// <param name="articleId">文章编号</param>
        /// <returns></returns>
        Task RemoveArticle(Guid articleId);
        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="articleId">文章编号</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="categoryIds">所属栏目</param>
        /// <returns></returns>
        Task EditArticle(Guid articleId, string title, string content, Guid[] categoryIds);
        /// <summary>
        /// 文章是否存在
        /// </summary>
        /// <param name="articleId">文章编号</param>
        /// <returns></returns>
        Task<bool> ExistsArticle(Guid articleId);
        /// <summary>
        /// 根据编号获取文章
        /// </summary>
        /// <param name="articleId">文章编号</param>
        /// <returns></returns>
        Task<ArticleDto> GetArticleById(Guid articleId);
        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Task GoodCount(Guid articleId);
        /// <summary>
        /// 反对
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Task BadCount(Guid articleId);
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="articleId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task CreateComment(Guid userId, Guid articleId, string content);
        /// <summary>
        /// 查看评论
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Task<List<CommentDto>> GetCommentByArticleId(Guid articleId);

    }
}