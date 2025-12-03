using System;
using Microsoft.EntityFrameworkCore;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Serie> Serie { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Serie>().ToTable("serie_info", "dbo");

            base.OnModelCreating(modelBuilder);
        }
    }
}
