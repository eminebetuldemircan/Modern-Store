using E_Commerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_CommerceApplication.Interfaces.IProductRepository
{
    public interface IProductsRepository 
    {
        Task<List<Product>> GetProductByCategory(int categoryId);
        Task<List<Product>> GetProductByPriceFilter(decimal minprice, decimal maxprice);
        Task<List<Product>> GetProductBySearch(string search);
    }
}
