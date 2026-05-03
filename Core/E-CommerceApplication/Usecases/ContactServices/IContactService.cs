using E_CommerceApplication.Dtos.ContactDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Usecases.ContactServices
{
    public interface IContactService
    {

        Task<List<ResultContactDto>> GetAllContactAsync();
        Task<GetByIdContactDto> GetByIdContactAsync(int id);
        Task CreateContactAsync(CreateContactDto model);
        Task UpdateContactAsync(UpdateContactDto model);
        Task DeleteContactAsync(int id);
        Task<List<ResultContactDto>> GetAllContactsByEmailAsync(string email);
    }
}
