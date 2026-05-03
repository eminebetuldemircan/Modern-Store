using E_Commerce.Domain.Entities;
using E_CommerceApplication.Dtos.CartItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Dtos.CartDtos
{
    public class UpdateCartDto
    {
        public int CartId { get; set; }
        //public decimal TotalAmount { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public int CustomerId { get; set; }
        //public Customer? Customer { get; set; }
        public ICollection<UpdateCartItemDto> CartItems { get; set; }
    }
}
