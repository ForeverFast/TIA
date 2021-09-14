using Microsoft.EntityFrameworkCore;
using TIA.Core.EfEntities;

namespace TIA.EntityFramework
{
    public class TIADbContext : DbContext
    {
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public TIADbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
