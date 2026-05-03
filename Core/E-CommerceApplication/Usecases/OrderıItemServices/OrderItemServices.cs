using E_Commerce.Domain.Entities;
using E_CommerceApplication.Dtos.OrderItemDtos;
using E_CommerceApplication.Interfaces;
using E_CommerceApplication.Usecases.OrderItemServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Usecases.OrderItemServices
{

    public class OrderItemServices : IOrderItemServices
    {
        private readonly IRepository<OrderItem> _repository;
        public OrderItemServices(IRepository<OrderItem> repository)
        {
            _repository = repository;
        }

        public async Task CreateOrderItemAsync(CreateOrderItemDto model)
        {
            await _repository.CreateAsync(new OrderItem
            {
                //OrderId = model.OrderId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                TotalPrice = model.TotalPrice
            });
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var values =await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(values);
        }

        public async Task<List<ResultOrderItemDto>> GetAllOrderItemAsync()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new ResultOrderItemDto
            {
                OrderItemId = x.OrderItemId,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                TotalPrice = x.TotalPrice
            }).ToList();
        }

        public async Task<GetByIdOrderItemDto> GetByIdOrderItemAsync(int id)
        {
            var values = await  _repository.GetByIdAsync(id);
            var result = new GetByIdOrderItemDto
            {
                OrderItemId = values.OrderItemId,
                OrderId = values.OrderId,
                ProductId = values.ProductId,
                Quantity = values.Quantity,
                TotalPrice = values.TotalPrice
            };
            return result;
        }

        public async Task UpdateOrderItemAsync(UpdateOrderItemDto model)
        {
            var values = await _repository.GetByIdAsync(model.OrderItemId);
            //values.OrderId = model.OrderId;
            values.ProductId = model.ProductId;
            values.Quantity = model.Quantity;
            values.TotalPrice = model.TotalPrice;
            await _repository.UpdateAsync(values);
        }
    }
}
