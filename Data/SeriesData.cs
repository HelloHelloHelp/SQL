
using Microsoft.EntityFrameworkCore;
using ZAAL_SQL.Models;

namespace ZAAL_SQL.Data
{
    public class SeriesDataContext: DbContext
    {
        public SeriesDataContext(DbContextOptions<SeriesDataContext> options)
            : base(options)
        {
        }

        public DbSet<SerieModel> SerieModel { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SerieModel>().ToTable("LFS", "dbo");

            base.OnModelCreating(modelBuilder);
        }
    }
}
