using Asp.Net_Core_Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Asp.Net_Core_Identity.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager { get; }

        public AdminController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }
    }
}
