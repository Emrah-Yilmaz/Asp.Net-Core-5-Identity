using Microsoft.AspNetCore.Mvc;

namespace Asp.Net_Core_Identity.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
