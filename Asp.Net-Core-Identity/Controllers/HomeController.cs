using Asp.Net_Core_Identity.Models;
using Asp.Net_Core_Identity.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Asp.Net_Core_Identity.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginView)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await userManager.FindByEmailAsync(loginView.Email);
                if (appUser != null)
                {
                    await signInManager.SignOutAsync();

                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await signInManager.PasswordSignInAsync(appUser, loginView.Password, false, true);
                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Member");
                    }                
                }
                else
                {
                    ModelState.AddModelError("", "Mail adresi ya da şifre geçersiz.");
                }

            }
            
            return View();
        }
        public IActionResult SignUp()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupViewModel signup)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new();
                user.UserName = signup.UserName;
                user.Email = signup.Email;
                user.PhoneNumber = signup.PhoneNumber;

                IdentityResult result = await userManager.CreateAsync(user, signup.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Home");
                    
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View(signup);

        }
    }
}
