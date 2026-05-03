using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Context.Identity
{
    public class AppIdentityUser : IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
    }
}
