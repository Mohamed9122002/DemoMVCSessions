using Demo.DataAccess.Models.DepartmentModels;
using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.Utilities;
using Demo.Presentation.ViewModels.AuthModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager ,SignInManager<ApplicationUser> _signInManager) : Controller
    {
        // Register 
        #region Register  
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = new ApplicationUser()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
            };
            var result = _userManager.CreateAsync(user, viewModel.Password).Result;

            if (result.Succeeded)
            {
                return RedirectToAction("Login");

            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(viewModel);
            }
        }
        #endregion
        #region Login 
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
            if (user is not null)
            {
                var flag = _userManager.CheckPasswordAsync(user, viewModel.Password).Result;
                if (flag)
                {
                    var result = _signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, false).Result;
                    if (result.Succeeded)
                      return  RedirectToAction(nameof(HomeController.Index), "Home");
                }


            } 
                return View(viewModel);
        }
        #endregion
        #region SignOut
        [HttpGet]
        public new  IActionResult SignOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        #endregion
        #region Forget Password 
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost] 
        public IActionResult ForgetPassword(ForgetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
            if(user is not null)
            {
                var Token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                /// Create Url  
                var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email =viewModel.Email,Token},Request.Scheme);
                var email = new Email()
                {
                    To = viewModel.Email,
                    Subject = "ResetPassword",
                    Body = ResetPasswordLink
                };
                // Send Email 
                EmailSettings.SendEmail(email);
                return RedirectToAction(nameof(CheckYourInBox));

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Opeartion");
            }
            return View(viewModel);
        }
        #endregion
        [HttpGet]
        public IActionResult CheckYourInBox()
        {
            return View();
        }
    }
}
