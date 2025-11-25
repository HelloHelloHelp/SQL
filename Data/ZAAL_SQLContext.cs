using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Data
{
    public class ZAAL_SQLContext : DbContext
    {
        public ZAAL_SQLContext (DbContextOptions<ZAAL_SQLContext> options)
            : base(options)
        {
        }

        public DbSet<ZAAL_SQL.Models.Movie> Movie { get; set; } = default!;
    }
}
