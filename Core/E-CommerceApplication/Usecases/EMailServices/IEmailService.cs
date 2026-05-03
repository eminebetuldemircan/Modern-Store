using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceApplication.Dtos.EMailDtos;


namespace E_CommerceApplication.Usecases.EMailServices
{
    public interface IEmailService
    {
        bool SendEmailAsync(SendEmailDto dto);
    }
}
