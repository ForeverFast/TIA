﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TIA.Core.Converters;
using TIA.Core.DTOClasses;
using TIA.Core.EfEntities;

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
                    .Include(c=>c.Products)
                    .FirstOrDefaultAsync(c => c.Id == guid);
                await context.SaveChangesAsync();

                return result.ConvertCatalog();
            }
        }

        public virtual async Task<IEnumerable<CatalogDTO>> GetCatalogsTree()
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                List<Catalog> result = await Task.FromResult(context.Catalogs.ToList());
                await context.SaveChangesAsync();

                return result.Where(c => c.ParentCatalogId == null).Select(c => c.ConvertCatalog());
            }
        }

        public virtual async Task<CatalogDTO> Add(CatalogDTO entity)
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                Catalog catalog = entity.ConvertCatalogDTO();

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
                    .Include(c => c.Catalogs)
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(x => x.Id == guid);

                HideAllData(catalog, context);

                catalog.IsActive = false;
                await context.SaveChangesAsync();

                return true;
            }
        }

        #region Вспомогательные методы

        /// <summary>
        /// Рекурсивно помечает видимость всех данных "False" 
        /// </summary>
        /// <param name="catalog"> Скрываемый каталог </param>
        /// <param name="context"> Контекст БД</param>
        private void HideAllData(Catalog catalog, TIADbContext context)
        {
            catalog = context.Catalogs
                .Include(f => f.Catalogs)
                .Include(f => f.Products)
                .FirstOrDefault(f => f.Id == catalog.Id);

            foreach (var item in catalog.Catalogs)
            {
                HideAllData(item, context);
            }

            foreach (var meme in catalog.Products)
            {
                var memeEntity = context.Products.FirstOrDefault(x => x.Id == meme.Id);
                if (memeEntity != null)
                {
                    memeEntity.IsActive = false;
                }
            }
            
            context.SaveChanges();
            foreach (var folder1 in catalog.Catalogs)
            {
                var folderEntity = context.Catalogs.FirstOrDefault(x => x.Id == folder1.Id);
                if (folderEntity != null)
                {
                    folderEntity.IsActive = false;

                }
            }
            context.SaveChanges();
        }

        #endregion

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
