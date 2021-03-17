using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcExercise.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExercise.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> UserManager,
            SignInManager<IdentityUser> SignInManager)
        {
            userManager = UserManager;
            signInManager = SignInManager;
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Employee");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel model,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result=await signInManager.PasswordSignInAsync(model.Email,model.Password,model.Remember,false);
                if (result.Succeeded)
                {  
                    if (string.IsNullOrEmpty(ReturnUrl) )
                    {
                        return RedirectToAction("Index", "Employee");
                    }
                    else
                    {
                        return LocalRedirect(ReturnUrl);

                    }
                    
                }
                
            }
            ModelState.AddModelError(string.Empty, "invalid login try");
            return View(model);
        }
        

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fullname = model.FirstName + "_" + model.LastName;
                IdentityUser user = new IdentityUser {UserName=model.Email,Email=model.Email };
               
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "Employee");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();
        }
    }
}
