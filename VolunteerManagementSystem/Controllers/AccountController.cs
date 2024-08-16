using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolunteerManagementSystem.Models;
using VolunteerManagementSystem.Models.ViewModels;

namespace VolunteerManagementSystem.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userM, SignInManager<AppUser> signInM)
        {
            userManager = userM;
            signInManager = signInM;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user,
                        model.Password, isPersistent: false, false)).Succeeded)
                    {
                        return Redirect("/Admin/Index");
                    }
                }

            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(model);
        }// end Login method

        public async Task<RedirectResult> Logout()
        {
            await signInManager.SignOutAsync();
            // When logged out, return to the login page (only admins are authorized for other pages)
            return Redirect("/Account/Login");
        }

        [AllowAnonymous]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.UserName = model.UserName;
                IdentityResult result = await userManager.CreateAsync(user,model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }// end create method

    }// end class
}// end namespace
