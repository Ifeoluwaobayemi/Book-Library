using Library.Web.Data.Entities;
using Library.Web.Services;
using Library.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        public AccountController(IAccountService accountService, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _accountService = accountService;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");

            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (await _accountService.LoginAsync(user, model.Password))
                        if(string.IsNullOrEmpty(ReturnUrl))
                        return RedirectToAction("Index", "Home");
                    else
                        return LocalRedirect(ReturnUrl);
                }

                //ModelState.AddModelError("Login failed", "Invalid credentials");
                ViewBag.Err = "Invalid credential";

            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // You can optionally sign the user in after registration
                    // await _accountService.LoginAsync(user, model.Password);

                    return RedirectToAction("Login", "Account");
                }

                ViewBag.Err("Registration failed\", \"Invalid credentials");
            }

            return View(model);

        }
        [HttpGet]
        public IActionResult ResetPassword(string Email, string token)
        {
            var viewModel = new ResetPasswordViewModel();
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.ErrToken = "Token is required";
            }

            if (string.IsNullOrEmpty(Email))
            {
                ViewBag.ErrEmail = "Email is required";
            }


            viewModel.Token = token;
            viewModel.Email = Email;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);
                    }
                }
                ViewBag.ErrEmail = "Email is invalid";
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var link = Url.Action("ResetPassword", "Account", new {model.Email, token }, Request.Scheme);

                    if (!string.IsNullOrEmpty(link))
                    {
                        if (await _emailService.SendAsync(model.Email, "Reset Password Link", link))
                        {
                            ViewBag.Err = "Reset Password link was sent to the email provided. Please visit your email and click on the link to continue";
                        }
                        else
                        {
                            ViewBag.Err = "Failed to send a reset password link. Please try again later.";
                        }
                    }
                   
                }
                
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
