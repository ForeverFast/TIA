using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TIA.Core.EfEntities;
using TIA.EntityFramework;

namespace TIA.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (TIADbContext context = new TIADbContextFactory().CreateDbContext(null))
            {
                Catalog catalog = context.Catalogs.ToArray()[0];

                Product product = new Product
                {
                    Title = "product1",
                    ParentCatalogId = catalog.Id
                };

                Product product2 = new Product
                {
                    Title = "product2",
                    ParentCatalogId = catalog.Id
                };

                Product product3 = new Product
                {
                    Title = "product2",
                    ParentCatalogId = catalog.Id
                };

                context.Products.AddRange(product, product2, product3);

                catalog = context.Catalogs.ToArray()[1];

                Product product4 = new Product
                {
                    Title = "product4",
                    ParentCatalogId = catalog.Id
                };

                Product product5 = new Product
                {
                    Title = "product5",
                    ParentCatalogId = catalog.Id
                };

                context.Products.AddRange(product4, product5);

                context.SaveChanges();
            }
        }
    }
}


/*
 Catalog catalog = new Catalog { Title = "test1" };
                Catalog catalog2 = new Catalog { Title = "test2" };
                Catalog catalog3 = new Catalog { Title = "test3" };

                var dbC = context.Catalogs.Add(catalog).Entity;
                var dbC2 = context.Catalogs.Add(catalog2).Entity;
                var dbC3 = context.Catalogs.Add(catalog3).Entity;
                context.SaveChanges();

                Catalog catalog11 = new Catalog { Title = "test11", ParentCatalogId = dbC.Id };
                Catalog catalog12 = new Catalog { Title = "test12", ParentCatalogId = dbC.Id };

                var dbC11 = context.Catalogs.Add(catalog11).Entity;
                var dbC12 = context.Catalogs.Add(catalog12).Entity;
                context.SaveChanges();

                Catalog catalog21 = new Catalog { Title = "test21", ParentCatalogId = dbC2.Id };
                Catalog catalog22 = new Catalog { Title = "test22", ParentCatalogId = dbC2.Id };

                context.AddRange(catalog21, catalog22);
                context.SaveChanges();

                Catalog catalog111 = new Catalog { Title = "test111", ParentCatalogId = dbC11.Id };
                Catalog catalog112 = new Catalog { Title = "test112", ParentCatalogId = dbC11.Id };

                context.AddRange(catalog111, catalog112);
                context.SaveChanges();
 */