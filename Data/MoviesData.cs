using System;
using Microsoft.EntityFrameworkCore;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Data
{
    public class MoviesDataContext: DbContext
    {
        public MoviesDataContext(DbContextOptions<MoviesDataContext> options)
            : base(options)
        {
        }

        public DbSet<MovieModel> MovieModel { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieModel>().ToTable("LFM", "dbo");

            base.OnModelCreating(modelBuilder);
        }
    }
}
