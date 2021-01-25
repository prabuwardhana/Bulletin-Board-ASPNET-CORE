using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<User> userMrg)
        {
            _roleManager = roleMgr;
            _userManager = userMrg;
        }

        [HttpGet]
        public IActionResult Index() => View(_roleManager.Roles);

        [HttpGet]
        public IActionResult Create() => View();

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            return View(name);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            List<User> members = new List<User>();

            List<User> nonMembers = new List<User>();

            foreach (User user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;

                list.Add(user);
            }

            return View(new RoleEditViewModels
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });

        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleModificationViewModel model)
        {
            IdentityResult result;

            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    User user = await _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);

                        if (model.RoleName == Roles.Administrator)
                        {
                            user.IsAdministrator = true;                            
                        }                                                

                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }

                        await _userManager.UpdateAsync(user);
                    }
                }

                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    User user = await _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);

                        if (model.RoleName == Roles.Administrator)
                        {
                            user.IsAdministrator = false;
                        }

                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }

                        await _userManager.UpdateAsync(user);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return await Edit(model.RoleId);
            }
        }
    }
}