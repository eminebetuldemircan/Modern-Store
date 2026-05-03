using E_CommerceApplication.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Usecases.ProductServices
{
    public interface IProductServices
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task<GetByIdProductDto> GetByIdProductAsync(int id);
        Task CreateProductAsync(CreateProductDto model);
        Task UpdateProductAsync(UpdateProductDto model);
        Task DeleteProductAsync(int id);
        Task<List<ResultProductDto>> GetProductTake(int sayi);
        Task<List<ResultProductDto>> GetProductByCategory(int categoryId);
        Task<List<ResultProductDto>> GetProductByPrice(decimal minprice, decimal maxprice);
        Task<List<ResultProductDto>> GetProductBySearch(string search);
    }
}
