using Microsoft.AspNetCore.Mvc;

namespace PhotoGram.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
