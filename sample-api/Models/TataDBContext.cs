using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_api.Models
{
    //  Data Context =  Database
    //  Data Context =  ConnectionString as Parameter.
    // Microsoft.EFCore
    // Microsoft.EF.SqlServer
    public class TataDBContext : DbContext
    {
        // Data Context class takes connectionstring as parameter
        public TataDBContext(DbContextOptions<TataDBContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
