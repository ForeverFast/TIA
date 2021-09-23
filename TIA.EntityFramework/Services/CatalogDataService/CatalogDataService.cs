using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TIA.Core.Converters;
using TIA.Core.DTOClasses;
using TIA.Core.EfEntities;
using Z.EntityFramework.Plus;

namespace TIA.EntityFramework.Services
{
    public class CatalogDataService : ICatalogDataService
    {
        protected readonly TIADbContextFactory _contextFactory;

        public virtual async Task<CatalogDTO> GetById(Guid guid)
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                Catalog result = await context.Catalogs
                    .IncludeFilter(c => c.Catalogs.Where(c => c.IsActive == true))
                    .IncludeFilter(c => c.Products.Where(c => c.IsActive == true))
                    .FirstOrDefaultAsync(c => c.Id == guid && c.IsActive == true);

                return result.ConvertCatalog();
            }
        }

        public virtual async Task<List<CatalogDTO>> GetCatalogsTree()
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                List<Catalog> result = await Task.FromResult(context.Catalogs
                    .Where(c => c.IsActive == true)
                    .IncludeFilter(c => c.Products.Where(c => c.IsActive == true))
                    .IncludeFilter(c => c.Catalogs.Where(c => c.IsActive == true))
                    .Include(c => c.ParentCatalog)
                    .ToList());
                
                return result.Where(c => c.ParentCatalogId == null)
                    .Select(c => c.ConvertCatalog())
                    .ToList();
            }
        }

        public virtual async Task<List<CatalogDTO>> GetCatalogsLineCollection()
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                List<Catalog> result = await Task.FromResult(context.Catalogs
                    .Where(c => c.IsActive == true)
                    .IncludeFilter(c => c.Catalogs.Where(c => c.IsActive == true))
                    .IncludeFilter(c => c.Products.Where(c => c.IsActive == true))
                    .Include(c => c.ParentCatalog)
                    .ToList());
               
                return result.Select(c => c.ConvertCatalog())
                    .ToList();
            }
        }

        public virtual async Task<CatalogDTO> Add(CatalogDTO entity)
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                Catalog catalog = entity.ConvertCatalogDTO();

                catalog.IsActive = true;
                EntityEntry<Catalog> createdResult = await context.Catalogs.AddAsync(catalog);
                await context.SaveChangesAsync();

                return createdResult.Entity.ConvertCatalog();
            }
        }

        public virtual async Task<CatalogDTO> Update(CatalogDTO entity, Guid guid)
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                Catalog catalog = entity.ConvertCatalogDTO();

                var original = await context.Catalogs.FirstOrDefaultAsync(e => e.Id == guid);

                foreach (PropertyInfo propertyInfo in original.GetType().GetProperties())
                {
                    if (propertyInfo.GetValue(catalog, null) == null)
                        propertyInfo.SetValue(catalog, propertyInfo.GetValue(original, null), null);
                }
                context.Entry(original).CurrentValues.SetValues(catalog);
                await context.SaveChangesAsync();

                return catalog.ConvertCatalog();
            }
        }

        public virtual async Task<bool> Delete(Guid guid)
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                Catalog catalog = await context.Catalogs
                    .FirstOrDefaultAsync(x => x.Id == guid);

                catalog.IsActive = false;
                await context.SaveChangesAsync();

                return true;
            }
        }

        #region Конструкторы

        public CatalogDataService()
        {
            _contextFactory = new TIADbContextFactory();
        }

        public CatalogDataService(TIADbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #endregion
    }
}

//#region Вспомогательные методы

///// <summary>
///// Рекурсивно помечает видимость всех данных "False" 
///// </summary>
///// <param name="catalog"> Скрываемый каталог </param>
///// <param name="context"> Контекст БД</param>
//private void HideAllData(Catalog catalog, TIADbContext context)
//{
//    catalog = context.Catalogs
//        .Include(f => f.Catalogs)
//        .Include(f => f.Products)
//        .FirstOrDefault(f => f.Id == catalog.Id);

//    foreach (var item in catalog.Catalogs)
//    {
//        HideAllData(item, context);
//    }

//    foreach (var meme in catalog.Products)
//    {
//        var memeEntity = context.Products.FirstOrDefault(x => x.Id == meme.Id);
//        if (memeEntity != null)
//        {
//            memeEntity.IsActive = false;
//        }
//    }

//    context.SaveChanges();
//    foreach (var folder1 in catalog.Catalogs)
//    {
//        var folderEntity = context.Catalogs.FirstOrDefault(x => x.Id == folder1.Id);
//        if (folderEntity != null)
//        {
//            folderEntity.IsActive = false;

//        }
//    }
//    context.SaveChanges();
//}

//#endregion
