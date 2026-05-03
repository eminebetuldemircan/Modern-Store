using E_CommerceApplication.Dtos.CartItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Interfaces.ICartItemsRepository
{
    public interface ICartItemsRepository
    {
        Task UpdateQuantity(int cartId, int productId, int quantity);
        Task UpdateQuantityOnCartAsync(UpdateCartItemDto dto);
        Task<bool> CheckCartItemAsync(int cartId, int productId);
    }
}
