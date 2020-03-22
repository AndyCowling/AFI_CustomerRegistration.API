using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace AFI_CustomerRegistration.API.Models
{
    public class CustomerDetailContext : DbContext
    {


        public DbSet<CustomerDetail> CustomerDetails { get; set; }
    }
}
