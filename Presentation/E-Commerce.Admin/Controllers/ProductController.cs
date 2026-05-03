using E_CommerceApplication.Dtos.ProductDtos;
using E_CommerceApplication.Usecases.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productService;

        public ProductController(IProductServices productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductAsync();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto model)
        {
            await _productService.CreateProductAsync(model);
            return RedirectToAction("Index");
        }
        //update methods
        public async Task<IActionResult> Edit(int productId)
        {
            var product = await _productService.GetByIdProductAsync(productId);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(UpdateProductDto model)
        {
            await _productService.UpdateProductAsync(model);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int productId)
        {
            await _productService.DeleteProductAsync(productId);
            return RedirectToAction("Index");
        }
    }
}
