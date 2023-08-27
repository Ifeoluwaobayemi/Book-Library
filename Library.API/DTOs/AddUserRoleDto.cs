using System.ComponentModel.DataAnnotations;

namespace Library.API.DTOs
{
    public class AddUserRoleDto
    {
      [Required]
       public string UserId { get; set; } = "";

       [Required]
        public string Role { get; set; } = "";
        
    }
}
