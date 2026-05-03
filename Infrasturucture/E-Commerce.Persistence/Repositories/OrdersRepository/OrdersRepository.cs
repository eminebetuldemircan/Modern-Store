using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Context;
using E_CommerceApplication.Dtos.CityDtos;
using E_CommerceApplication.Dtos.TownDtos;
using E_CommerceApplication.Interfaces.IOrderRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories.OrdersRepository
{
    public class OrdersRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrdersRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<City>> GetCity()
        {
            var cities = await _context.City.ToListAsync();
            return cities;
        }

        public async Task<List<Town>> GetTownByCityId(int cityid)
        {
            var town = await _context.Town.Where(x => x.CityId == cityid).ToListAsync();
            return town;
        }
    }
}
