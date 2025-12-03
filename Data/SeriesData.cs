using System;
using Microsoft.EntityFrameworkCore;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Data
{
    public class SeriesData : DbContext
    {
        public SeriesData(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<SeriesData> seriesData { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Serie>().ToTable("LFS", "dbo");

            base.OnModelCreating(modelBuilder);
        }
    }
}
