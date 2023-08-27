using System.ComponentModel.DataAnnotations;

namespace Library.Web.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
