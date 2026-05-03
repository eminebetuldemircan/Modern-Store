using E_Commerce.Domain.Entities;
using E_CommerceApplication.Dtos.CartItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Dtos.CartDtos
{
    public class CreateCartDto
    {

        //public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CustomerId { get; set; }
        //public Customer? Customer { get; set; }
        public ICollection<CreateCartItemDto> CartItems { get; set; }
        public string? UserId { get; set; }

    }
}
