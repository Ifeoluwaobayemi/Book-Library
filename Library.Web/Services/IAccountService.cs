using Library.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Library.Web.Services
{
    public interface IAccountService
    {
        Task<bool> LoginAsync(AppUser user, string password);
        Task LogoutAsync();
        bool IsLoggedInAsync(ClaimsPrincipal user);
        Task<IdentityResult> RegisterAsync(AppUser user, string password);
    }
}
