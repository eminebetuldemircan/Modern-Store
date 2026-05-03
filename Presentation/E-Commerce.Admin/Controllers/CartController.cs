using E_CommerceApplication.Usecases.CartServices;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Admin.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var value = await _cartService.GetAllAdminCartAsync();
            return View(value);
        }
    }
}
