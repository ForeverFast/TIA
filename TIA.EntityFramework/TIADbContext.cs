using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TIA.Core.AspNetCoreEntities;
using TIA.Core.EfEntities;

namespace TIA.EntityFramework
{
    public class TIADbContext : IdentityDbContext<User>
    {
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Product> Products { get; set; }

        public TIADbContext(DbContextOptions<TIADbContext> options)
          : base(options)
        {
        }
    }
}
