using System.ComponentModel.DataAnnotations;

namespace MVC.Blog.Site.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50,MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword  { get; set; }
    }
}