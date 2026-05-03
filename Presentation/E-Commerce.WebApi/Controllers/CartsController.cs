using E_CommerceApplication.Dtos.CartDtos;
using E_CommerceApplication.Usecases.CartServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _services;

        public CartsController(ICartService services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCart()
        {
            var values = await _services.GetAllCartAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCart(int id)
        {
            var value = await _services.GetByIdCartAsync(id);
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCart(CreateCartDto dto)
        {
            await _services.CreateCartAsync(dto);
            return Ok("Cart ınız başarılı bir şekilde oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCart(UpdateCartDto dto)
        {
            await _services.UpdateCartAsync(dto);
            return Ok("Cartınız başarılı bir şekilde güncellendi.");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCart(int id)
        {
            await _services.DeleteCartAsync(id);
            return Ok("Cartınız başarılı bir şekilde silindi.");
        }
    }
}
