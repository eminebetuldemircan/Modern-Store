using E_Commerce.Persistence.Context;
using E_CommerceApplication.Dtos.CartItemDtos;
using E_CommerceApplication.Interfaces.ICartItemsRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static E_Commerce.Persistence.Repositories.CartItemsRepository.CartItemsRepository;


namespace E_Commerce.Persistence.Repositories.CartItemsRepository
{
    public class CartItemsRepository : ICartItemsRepository
    {
        private readonly AppDbContext _context;

        public CartItemsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckCartItemAsync(int cartId, int productId)
        {
            var items = await _context.CartItems.Where(x => x.CartId == cartId && x.ProductId == productId).SingleOrDefaultAsync();
            if (items == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task UpdateQuantity(int cartId, int productId, int quantity)
        {
            var cart = await _context.CartItems.Where(x => x.CartId == cartId && x.ProductId == productId).SingleOrDefaultAsync();
            if (cart != null)
            {
                var tempprice = cart.TotalPrice / cart.Quantity;
                cart.Quantity += quantity;
                cart.TotalPrice = tempprice * cart.Quantity;

                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateQuantityOnCartAsync(UpdateCartItemDto dto)
        {
            var cart = await _context.CartItems.Where(x => x.CartId == dto.CartId && x.ProductId == dto.ProductId).SingleOrDefaultAsync();
            if (cart != null)
            {
                var tempprice = cart.TotalPrice / cart.Quantity;
                cart.Quantity = dto.Quantity;
                cart.TotalPrice = tempprice * cart.Quantity;

                await _context.SaveChangesAsync();
            }
        }
    }
}

