using System;
using System.Linq;
using System.Collections.Generic;
using TIA.DataAccessLayer.Converters;
using TIA.DataAccessLayer.DTOClasses;
using TIA.DataAccessLayer.Models;
using Z.EntityFramework.Plus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace TIA.DataAccessLayer.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly TIA_DBContextFactory _contextFactory;

        public CatalogDTO GetById(Guid id)
        {
            using (TIA_DBContext context = _contextFactory.CreateDbContext(null))
            {
                Catalog result = context.Catalogs
                    .IncludeFilter(c => c.InverseParentCatalog.Where(c => c.IsActive == true))
                    .FirstOrDefault(c => c.Id == id && c.IsActive == true);

                return result.ConvertCatalog();
            }
        }

        public List<ProductDTO> GetCatalogProductsWithFilters(Guid id, string title = "", DateTime? minDate = null, DateTime? maxDate = null, uint? minPrice = null, uint? maxPrice = null)
        {
            using (TIA_DBContext context = _contextFactory.CreateDbContext(null))
            {
                IQueryable<Product> result = context.Products
                    .Where(p => p.ParentCatalogId == id)
                    .Where(p => p.IsActive == true);

                if (!string.IsNullOrEmpty(title))
                    result = result.Where(p => p.Title.Contains(title));

                if (minDate != null)
                    result = result.Where(p => p.SomeDate > minDate);

                if (maxDate != null)
                    result = result.Where(p => p.SomeDate < maxDate);

                if (minPrice != null)
                    result = result.Where(p => p.Price > minPrice);

                if (maxPrice != null)
                    result = result.Where(p => p.Price < maxPrice);

                return result.Select(p => p.ConvertProduct())
                    .ToList();
            }
        }

        public List<CatalogDTO> GetCatalogsTree()
        {
            using (TIA_DBContext context = _contextFactory.CreateDbContext(null))
            {
                List<CatalogDTO> result = context.Catalogs
                    .Where(c => c.IsActive == true)
                    .IncludeFilter(c => c.InverseParentCatalog.Where(c => c.IsActive == true))
                    .Include(c => c.ParentCatalog)
                    .AsEnumerable()
                    .Where(c => c.ParentCatalogId == null)
                    .Select(c => c.ConvertCatalog())
                    .ToList();

                return result;
            }
        }

        public List<CatalogDTO> GetCatalogsLineCollection()
        {
            using (TIA_DBContext context = _contextFactory.CreateDbContext(null))
            {
                List<CatalogDTO> result = context.Catalogs
                    .Where(c => c.IsActive == true)
                    .Include(c => c.ParentCatalog)
                    .Select(c => c.ConvertCatalog())
                    .ToList();

                return result;

            }
        }

        public CatalogDTO Add(CatalogDTO entity)
        {
            using (TIA_DBContext context = _contextFactory.CreateDbContext(null))
            {
                Catalog catalog = entity.ConvertCatalogDTO();

                catalog.IsActive = true;
                if (context.Catalogs.FirstOrDefault(c => c.Id == catalog.Id) != null || catalog.Id == Guid.Empty)
                    catalog.Id = Guid.NewGuid();

                EntityEntry<Catalog> createdResult = context.Catalogs.Add(catalog);
                context.SaveChanges();

                return createdResult.Entity.ConvertCatalog();
            }
        }

        public CatalogDTO Update(CatalogDTO entity, Guid id)
        {
            using (TIA_DBContext context = _contextFactory.CreateDbContext(null))
            {
                Catalog catalog = entity.ConvertCatalogDTO();

                var original = context.Catalogs.FirstOrDefault(e => e.Id == id);

                foreach (PropertyInfo propertyInfo in original.GetType().GetProperties())
                {
                    if (propertyInfo.GetValue(catalog, null) == null)
                        propertyInfo.SetValue(catalog, propertyInfo.GetValue(original, null), null);
                }
                context.Entry(original).CurrentValues.SetValues(catalog);
                context.SaveChanges();

                return catalog.ConvertCatalog();
            }
        }

        public bool Delete(Guid id)
        {
            using (TIA_DBContext context = _contextFactory.CreateDbContext(null))
            {
                Catalog catalog = context.Catalogs
                   .FirstOrDefault(x => x.Id == id);

                catalog.IsActive = false;
                context.SaveChanges();

                return true;
            }
        }

        public CatalogRepository()
        {
            this._contextFactory = new TIA_DBContextFactory();
        }

        public CatalogRepository(TIA_DBContextFactory context)
        {
            this._contextFactory = context;
        }

    }
}
