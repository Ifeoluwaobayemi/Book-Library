using Microsoft.AspNetCore.Identity;

namespace Library.Web.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
