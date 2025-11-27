using System;
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

        public DbSet<Movie> Movie { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("movie_info", "dbo");

            base.OnModelCreating(modelBuilder);
        }
    }
}
