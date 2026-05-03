using E_CommerceApplication.Usecases.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApp.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        public async Task<IActionResult> Index(int categoryId, decimal minprice, decimal maxprice, string search, int pageNumber = 1, int pageSize = 6)
        {
            if (categoryId != 0)
            {
                var values = await _productServices.GetProductByCategory(categoryId);
                var pageproducts = values.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                int totalProductss = values.Count();
                int totalPagess = (int)Math.Ceiling((double)totalProductss / pageSize);

                // View'e model gönder
                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = totalPagess;

                return View(pageproducts);
            }
            if (maxprice != 0)
            {
                var values = await _productServices.GetProductByPrice(minprice, maxprice);
                var pageproduct1 = values.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                int totalProducts1 = values.Count();
                int totalPages1 = (int)Math.Ceiling((double)totalProducts1 / pageSize);

                // View'e model gönder
                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = totalPages1;
                return View(pageproduct1);

            }
            var value = await _productServices.GetAllProductAsync();
            var pageproduct = value.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            int totalProducts = value.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            // View'e model gönder
            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;
            return View(pageproduct);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string search)
        {
            if (search == null)
            {
                return View();
            }
            var value = await _productServices.GetProductBySearch(search);
            return View(value);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var value = await _productServices.GetByIdProductAsync(id);
            return View(value);
        }
    }
}
