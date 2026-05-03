using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApp.Controllers
{
    public class FaqsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
