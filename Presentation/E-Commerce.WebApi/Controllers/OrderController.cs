using E_CommerceApplication.Dtos.OrderDtos;
using E_CommerceApplication.Usecases.OrderServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _services;

        public OrderController(IOrderServices services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var values = await _services.GetAllOrderAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrder(int id)
        {
            var values = await _services.GetByIdOrderAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            await _services.CreateOrderAsync(dto);
            return Ok("Siparişiniz başarılı bir şekilde oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            await _services.UpdateOrderAsync(dto);
            return Ok("Siparişiniz başarılı bir şekilde güncellendi.");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _services.DeleteOrderAsync(id);
            return Ok("Siparişiniz başarılı bir şekilde silindi.");
        }
    }
}
