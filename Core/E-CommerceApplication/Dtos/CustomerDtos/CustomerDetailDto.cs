using E_CommerceApplication.Dtos.CartDtos;
using E_CommerceApplication.Dtos.ContactDtos;
using E_CommerceApplication.Dtos.FavoritesDtos;
using E_CommerceApplication.Dtos.HelpDtos;
using E_CommerceApplication.Dtos.OrderDtos;
using E_CommerceApplication.Dtos.SubscriberDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Dtos.CustomerDtos
{
    public class CustomerDetailDto
    {
        public int CustomerId { get; set; }
        public GetByIdCustomerDto Customer { get; set; }
        public GetByIdCartDto Cart { get; set; }
        public List<ResultContactDto> Contacts { get; set; }
        public List<ResultFavoritesDto> Favorites { get; set; }
        public List<ResultHelpDto> Helps { get; set; }
        public List<ResultOrderDto> Orders { get; set; }
        public List<ResultSubscriberDto> Subscribe { get; set; }
    }
}
