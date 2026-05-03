using E_CommerceApplication.Dtos.FavoritesDtos;
using E_CommerceApplication.Usecases.AccountService;
using E_CommerceApplication.Usecases.FavoritesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApp.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavoritesService _favoritesService;
        private readonly IAccountService _accountService;

        public FavoritesController(IFavoritesService favoritesService, IAccountService accountService)
        {
            _favoritesService = favoritesService;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var userid = await _accountService.GetUserIdAsync(User);
            var values = await _favoritesService.GetFavoritesByUserId(userid);
            return View(values);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productid)
        {
            var userid = await _accountService.GetUserIdAsync(User);

            if (string.IsNullOrEmpty(userid))
                return Json(new { success = false, error = "Giriş yapmalısınız" });

            var check = await _favoritesService
                .CheckFavoritesByUseridAndProductId(userid, productid);

            if (!check)
                return Json(new { success = false, error = "Ürün zaten favorilerde" });

            await _favoritesService.CreateFavoritesAsync(new CreateFavoritesDto
            {
                ProductId = productid,
                UserId = userid,
                CreatedDate = DateTime.Now
            });

            return Json(new { success = true });
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _favoritesService.DeleteFavoritesAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
