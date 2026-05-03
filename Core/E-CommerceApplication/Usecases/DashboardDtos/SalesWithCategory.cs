using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Usecases.DashboardDtos
{
    public class SalesWithCategory
    {
        public string Categoryname { get; set; }
        public decimal Totalsales { get; set; }
    }
}
