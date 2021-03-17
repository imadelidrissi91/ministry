using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcExercise.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExercise.Controllers
{
    [Authorize(Roles="SuperAdmin,admin")]
    public class AdministrationController:Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this._userManager = userManager;
        }
        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateRoleAsync(AdministrationCreateRoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole { Name = model.Name };
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Employee");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();
        }
        public ActionResult ListRole()
        {
            var roles = this.roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public async Task<ActionResult> UpdateRole(string id)
        {
            IdentityRole role = await this.roleManager.FindByIdAsync(id);
            if (role is null)
            {

            }
            AdministrationEditRoleViewModel model = new AdministrationEditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name,
                Users = new List<string>()
            };

            foreach (var item in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(item, role.Name))
                {
                    model.Users.Add(item.Email);
                }
            }
            return View(model);


        }
        [HttpPost]
        public async Task<ActionResult> UpdateRole(AdministrationEditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = await this.roleManager.FindByIdAsync(model.Id);
                if (role is null)
                {

                }
                else
                {
                    role.Name = model.RoleName;
                    IdentityResult result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(ListRole));
                    }
                }
            }
            
            
            return View(model);


        }

        [HttpGet]
        public async Task<ActionResult> EditUsersRole(string IdRole)
        {
            IdentityRole role = await this.roleManager.FindByIdAsync(IdRole);
            if (role is null)
            {

            }
                List<AdministrationEditUsersRoleViewModel> selectedusers = new List<AdministrationEditUsersRoleViewModel>();
                foreach (IdentityUser user in _userManager.Users)
                {
                    AdministrationEditUsersRoleViewModel m = new AdministrationEditUsersRoleViewModel()
                    {
                        UserID = user.Id,
                        NameUser = user.UserName
                    };
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        m.IsSelected = true;
                        
                    }
                selectedusers.Add(m);
            }

            
                ViewBag.roleid = IdRole;
                return View(selectedusers);
        }
        [HttpPost]
        public async Task<ActionResult> EditUsersRole(List<AdministrationEditUsersRoleViewModel> model,string idRole)
        {
            if (string.IsNullOrEmpty(idRole))
            {
            }
            IdentityResult result = null;
            var role = await roleManager.FindByIdAsync(idRole);
            for (int i=0;i< model.Count;i++)
            {
                IdentityUser user = await _userManager.FindByIdAsync(model[i].UserID);
                
                if (await _userManager.IsInRoleAsync(user, role.Name) && !(model[i].IsSelected))
                {
                    result=await _userManager.RemoveFromRoleAsync(user, role.Name);
                }else if (!(await _userManager.IsInRoleAsync(user, role.Name)) && model[i].IsSelected == true)
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
            }
            if (!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return RedirectToAction(nameof(ListRole));
        }

    }
}
 