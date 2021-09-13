﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TIA.EntityFramework;

namespace TIA.EntityFramework.Migrations
{
    [DbContext(typeof(TIADbContext))]
    partial class TIADbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TIA.Core.EfEntities.Catalog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ParentCatalogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentCatalogId");

                    b.ToTable("Catalogs");
                });

            modelBuilder.Entity("TIA.Core.EfEntities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ParentCatalogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentCatalogId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TIA.Core.EfEntities.Catalog", b =>
                {
                    b.HasOne("TIA.Core.EfEntities.Catalog", "ParentCatalog")
                        .WithMany("Catalogs")
                        .HasForeignKey("ParentCatalogId");

                    b.Navigation("ParentCatalog");
                });

            modelBuilder.Entity("TIA.Core.EfEntities.Product", b =>
                {
                    b.HasOne("TIA.Core.EfEntities.Catalog", "ParentCatalog")
                        .WithMany("Products")
                        .HasForeignKey("ParentCatalogId");

                    b.Navigation("ParentCatalog");
                });

            modelBuilder.Entity("TIA.Core.EfEntities.Catalog", b =>
                {
                    b.Navigation("Catalogs");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
