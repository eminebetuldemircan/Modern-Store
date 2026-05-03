using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Dtos.FavoritesDtos
{
    public class CreateFavoritesDto
    {
        public DateTime CreatedDate { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int? CustomerId { get; set; }
    }
}
