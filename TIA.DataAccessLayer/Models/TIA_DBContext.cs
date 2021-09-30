using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TIA.DataAccessLayer.Models
{
    public partial class TIA_DBContext : IdentityDbContext<User>
    {
        public TIA_DBContext()
        {
        }

        public TIA_DBContext(DbContextOptions<TIA_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDataModel> ProductDataModels { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TIA_DB;Integrated Security=True; Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<Catalog>(entity =>
            {
                entity.HasIndex(e => e.ParentCatalogId, "IX_Catalogs_ParentCatalogId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.ParentCatalog)
                    .WithMany(p => p.InverseParentCatalog)
                    .HasForeignKey(d => d.ParentCatalogId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.ParentCatalogId, "IX_Products_ParentCatalogId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.ParentCatalog)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ParentCatalogId);
            });

            modelBuilder.Entity<ProductDataModel>(entity =>
            {
                entity.HasNoKey();
            });

            OnModelCreatingPartial(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
