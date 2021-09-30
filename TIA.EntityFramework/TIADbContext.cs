using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TIA.Core.AspNetCoreEntities;
using TIA.Core.EfEntities;
using TIA.Core.StoredProcedureModels;

namespace TIA.EntityFramework
{
    public class TIADbContext : IdentityDbContext<User>
    {
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Product> Products { get; set; }

        // Store Procedures
        public virtual DbSet<ProductDataModel> ProductDataModels { get; set; }

        public TIADbContext(DbContextOptions<TIADbContext> options)
          : base(options)
        {
        }
    }
}
