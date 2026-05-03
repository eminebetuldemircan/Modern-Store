using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Context;
using E_CommerceApplication.Dtos.FavoritesDtos;
using E_CommerceApplication.Interfaces.IFavoritesRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories.FavoritesRepository
{
    public class FavoritesRepository : IFavoritesRepository
    {
        private readonly AppDbContext _context;

        public FavoritesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AdminFavoritesDto>> GetFavoritesGroupUserId()
        {
            var value = await _context.Favorites.GroupBy(x => x.UserId).Select(y => new AdminFavoritesDto
            {
                UserId = y.Key,
                NameSurname = _context.Customers.Where(b => b.UserId == y.Key).Select(c => c.FirstName + " " + c.LastName).FirstOrDefault(),
                FavoritesDetails = y.Select(z => new AdminFavoritesDetailDto
                {
                    CreatedDate = z.CreatedDate,
                    ProductId = z.ProductId,
                    Product = _context.Products.Where(x => x.ProductId == z.ProductId).Select(a => new Product
                    {
                        ProductName = a.ProductName,
                    }).FirstOrDefault()
                }).ToList()
            }).ToListAsync();
            return value;
        }
    }
}
