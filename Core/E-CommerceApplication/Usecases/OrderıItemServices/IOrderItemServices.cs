using E_CommerceApplication.Dtos.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Usecases.OrderItemServices
{   
    public interface IOrderItemServices
    {
        Task<List<ResultOrderItemDto>> GetAllOrderItemAsync();
        Task<GetByIdOrderItemDto> GetByIdOrderItemAsync(int id);
        Task CreateOrderItemAsync(CreateOrderItemDto model);
        Task UpdateOrderItemAsync(UpdateOrderItemDto model);
        Task DeleteOrderItemAsync(int id);
    }
}
