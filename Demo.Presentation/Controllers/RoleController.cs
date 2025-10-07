using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Presentation.Controllers
{
    [Authorize]
    public class RoleController(RoleManager<IdentityRole> _roleManager ,UserManager<ApplicationUser> _userManager) : Controller
    {
        #region Create 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(RoleViewModel roleView)
        {
            if (ModelState.IsValid)
            {
                var result = _roleManager.CreateAsync(new IdentityRole() { Name = roleView.Name }).Result;
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

            }
            ModelState.AddModelError(string.Empty, "Role Can Not Be Created");
            return View(roleView);
        }
        #endregion
        /// Get All Role 
        public IActionResult Index()
        {
            var users = _roleManager.Roles.AsQueryable();

            var roleViewModel = users.Select(r => new RoleViewModel()
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
            return View(roleViewModel);
        }
        #region Details Role
        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (id is null) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null) return NotFound();
            // Mapping 
            var roleViewModel = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
            };
            return View(roleViewModel);
        }
        #endregion
        #region Edit  User 
        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if (id is null) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null) return NotFound();
            // Mapping 
            var users = _userManager.Users.ToListAsync().Result;
            var roleViewModel = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Users = users.Select(u => new UserRoleViewModel
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    IsSelected = _userManager.IsInRoleAsync(u, role.Name).Result

                }).ToList()
            };

            return View(roleViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RoleViewModel roleViewModel)
        {
            var message = string.Empty;

            //if (!ModelState.IsValid)
            //    return View(roleViewModel);
            try
            {
                // Get User 
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return NotFound();
                // Mapping UserViewModel To ApplicationUser
                role.Name = roleViewModel.Name;
                // Update Role
                var result = await _roleManager.UpdateAsync(role);

                foreach (var userRole in roleViewModel.Users)
                {
                    var user = await _userManager.FindByIdAsync(userRole.UserId);
                    if(user is not null)
                    {
                        if(userRole.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                        {
                           await _userManager.AddToRoleAsync(user, role.Name);
                        }else if (!userRole.IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(user, role.Name);
                        }
                    }
                }
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "role can  not be Updated ";
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(roleViewModel);
        }
        #endregion
        #region Delete 
        [HttpPost]
        public IActionResult Delete(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            try
            {
                if (role is not null)
                {
                    var result = _roleManager.DeleteAsync(role).Result;
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
