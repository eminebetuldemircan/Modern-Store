using E_CommerceApplication.Usecases.FavoritesServices;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Admin.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoritesService _favoritesService;

        public FavoriteController(IFavoritesService favoritesService)
        {
            _favoritesService = favoritesService;
        }

        public async Task<IActionResult> Index()
        {
            var value = await _favoritesService.GetAdminFavoritesList();
            return View(value);
        }
    }
}
