using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Buffers;
using System.Threading.Tasks;

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
        #region Details User
        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(userViewModel);

        }
        #endregion
        #region  Edit User 
        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            // Mapping 
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(userViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
                return View(userViewModel);

            var user = await _userManager.FindByIdAsync(userViewModel.Id);
            if (user == null) return NotFound();

            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Email = userViewModel.Email;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            return View(userViewModel);
        }

        #endregion
        #region Delete User 
        [HttpPost]
        public IActionResult Delete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            try
            {
                if (user is not null)
                {
                    var result = _userManager.DeleteAsync(user).Result;
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
                    else
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(nameof(Index));
        }
        #endregion
    }
}
