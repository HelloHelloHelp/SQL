using System;
using Microsoft.EntityFrameworkCore;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Data
{
    public class MoviesData : DbContext
    {
        public MoviesData(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<MoviesData> moviesData { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Serie>().ToTable("LFM", "dbo");

            base.OnModelCreating(modelBuilder);
        }
    }
}
