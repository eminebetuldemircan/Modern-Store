using E_CommerceApplication.Dtos.CityDtos;
using E_CommerceApplication.Dtos.OrderDtos;
using E_CommerceApplication.Dtos.TownDtos;
using E_CommerceApplication.Usecases.DashboardDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Usecases.OrderServices
{
    public interface IOrderServices
    {
        Task<List<ResultOrderDto>> GetAllOrderAsync();
        Task<GetByIdOrderDto> GetByIdOrderAsync(int id);
        Task CreateOrderAsync(CreateOrderDto model);
        Task UpdateOrderAsync(UpdateOrderDto model);
        Task DeleteOrderAsync(int id);
        Task<List<ResultCityDto>> GetAllCity();
        Task<List<ResultTownDto>> GetTownByCityId(int cityId);

        Task<List<ResultOrderDto>> GetOrderByUserId(string userId);
        Task UpdateOrderStatus(int orderId, string orderstatus);
        Task<List<SalesWithCategory>> GetOrderByKategori();
        Task<List<DashboardSoledProductDto>> GetSoledProducts();
        Task<List<DashboardOrderStatusDto>> GetOrderStatusGrafiks();
        Task<List<SalesTrendDto>> GetSalesTrends();
        Task<DashboardCardsDto> GetDashboardCards();
    }
}
