using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;

namespace Demo.Presentation.Controllers
{
    public class UserController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region GetAll User 
        [HttpGet] 
        public IActionResult Index()
        {
            var users = _userManager.Users.AsQueryable();
        
            var userViewModel = users.Select(u => new UserViewModel()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
            }).ToList();
            foreach (var user in userViewModel)
            {
                user.Roles = _userManager.GetRolesAsync(_userManager.FindByIdAsync(user.Id).Result).Result;
            }
            return View(userViewModel);
        }
        #endregion
    }
}
