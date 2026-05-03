using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Usecases.DashboardDtos
{
    public class SalesTrendDto
    {
        public string Months { get; set; }
        public string SalesCount { get; set; }
        public string TotalAmount { get; set; }
    }
}
