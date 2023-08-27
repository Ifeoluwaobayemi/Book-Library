using Library.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Library.Web.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public AccountService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) 
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> LoginAsync(AppUser user, string password)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (loginResult.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public bool IsLoggedInAsync(ClaimsPrincipal user)
        {
            if (_signInManager.IsSignedIn(user))
            return true;
            return false;
        }

        public async Task<IdentityResult> RegisterAsync(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

    }
}
