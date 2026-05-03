using E_CommerceApplication.Dtos.HelpDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Usecases.HelpServices
{
    public interface IHelpService
    {
        Task<List<ResultHelpDto>> GetAllHelpAsync();
        Task<GetByIdHelpDto> GetByIdHelpAsync(int id);
        Task CreateHelpAsync(CreateHelpDto model);
        Task UpdateHelpAsync(UpdateHelpDto model);
        Task DeleteHelpAsync(int id);
        Task<List<ResultHelpDto>> GetByEmailHelpAsync(string email);
    }
}
