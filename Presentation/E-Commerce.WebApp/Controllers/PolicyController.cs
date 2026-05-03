using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApp.Controllers
{
    public class PolicyController : Controller
    {
        public IActionResult DistanceSales() => View();
        public IActionResult PreInformation() => View();
        public IActionResult Privacy() => View();
        public IActionResult Kvkk() => View();
        public IActionResult Cookies() => View();
        public IActionResult Return() => View();
    }
}
