using System.ComponentModel.DataAnnotations;

namespace MVC.Blog.Site.Models.ArticleViewModels
{
    public class CreateCategoryViewModel
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [Required]
        [Display(Name = "文章类别")]
        [StringLength(200,MinimumLength = 2)]
        public string CategoryName { get; set; }
    }
}