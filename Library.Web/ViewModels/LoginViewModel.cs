using System.ComponentModel.DataAnnotations;

namespace Library.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required(ErrorMessage="Password is required!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
