using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVC.Blog.Site.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name="电子邮箱")]
        public string LoginName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50,MinimumLength = 6)]
        [Display(Name = "密码")]
        public string LoginPwd { get; set; }
        [Display(Name = "记住我")]
        public bool RememberMe { get; set; }
    }
}