using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{

    public class Town
    {
        [Key]
        public int TownId { get; set; }   

        public int CityId { get; set; }
        public string Townname { get; set; }
    }
}
