using Asp.Net_Core_Identity.Models;
using Asp.Net_Core_Identity.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> Login(LoginViewModel loginView, string returnurl)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await userManager.FindByEmailAsync(loginView.Email);
                if (appUser != null)
                {
                    if (await userManager.IsLockedOutAsync(appUser))
                    {
                        ModelState.AddModelError("", "Hesabınız bir süreliğine kilitlenmiştir. Lütfen daha sonra tekrar deneyiniz.");
                    }

                    await signInManager.SignOutAsync();

                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await signInManager.PasswordSignInAsync(appUser, loginView.Password, loginView.RememberMe, false);
                    if (signInResult.Succeeded)
                    {
                        await userManager.ResetAccessFailedCountAsync(appUser);
                        if (!string.IsNullOrEmpty(returnurl))
                        {
                            return LocalRedirect(returnurl);
                        }
                        return RedirectToAction("Index", "Member");
                        
                    }
                    else
                    {
                        await userManager.AccessFailedAsync(appUser);
                        int fail = await userManager.GetAccessFailedCountAsync(appUser);
                        if (fail==3)
                        {
                            await userManager.SetLockoutEndDateAsync(appUser, new System.DateTimeOffset(DateTime.Now.AddMinutes(15)));
                            ModelState.AddModelError("", "Hesabınız bir çok kez başarısız giriş denemesinden dolayı kilitlenmiştir. Lütfen daha sonra deneyiniz.");

                        }
                        else
                        {
                            //Bu scope alanında şifre geçersizdir. Ancak kullanıcıya direkt olarak kullanıcı adının doğru şifre bilgsinin yanlış olduğunu
                            //belirtmek istemdiğimden bu şekilde bir açıklama eklemeyi tercih ettim.
                            ModelState.AddModelError("", "Mail adresi ya da şifre geçersiz.");
                        }

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bu email adresine kayıtlı kullanıcı bulunamamıştır.");
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
