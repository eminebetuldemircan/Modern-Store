using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Interfaces.IOrderRepository
{
    public interface IOrderRepository
    {
        Task<List<City>> GetCity();
        Task<List<Town>> GetTownByCityId(int cityid);

    }
}
