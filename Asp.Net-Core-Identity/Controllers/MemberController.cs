using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Asp.Net_Core_Identity.Models;
using Asp.Net_Core_Identity.ViewModel;

namespace Asp.Net_Core_Identity.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public MemberController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            UserViewModel userView = user.Adapt<UserViewModel>();
            return View(userView);
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(PasswordChangeViewModel passwordChange)
        {
            if (ModelState.IsValid)
            {
                AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                
                    bool exist = _userManager.CheckPasswordAsync(user, passwordChange.PasswordOld).Result;
                    if (exist)
                    {
                        IdentityResult result = _userManager.ChangePasswordAsync(user, passwordChange.PasswordOld, passwordChange.PasswordNew).Result;
                        if (result.Succeeded)
                        {
                        _userManager.UpdateSecurityStampAsync(user);
                        _signInManager.SignOutAsync();
                        _signInManager.PasswordSignInAsync(user, passwordChange.PasswordNew, true, false);
                            ViewBag.success = true;

                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Eski şifreniz yanlış");
                    }
                
            }
            return View(passwordChange);
        }
    }
}
