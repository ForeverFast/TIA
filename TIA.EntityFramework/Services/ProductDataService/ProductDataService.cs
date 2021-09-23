using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Reflection;
using System.Threading.Tasks;
using TIA.Core.Converters;
using TIA.Core.DTOClasses;
using TIA.Core.EfEntities;

namespace TIA.EntityFramework.Services
{
    public class ProductDataService : IProductDataService
    {
        protected readonly TIADbContextFactory _contextFactory;

        public virtual async Task<ProductDTO> GetById(Guid guid)
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                Product result = await context.Products
                    .FirstOrDefaultAsync(c => c.Id == guid);

                return result.ConvertProduct();
            }
        }

        public virtual async Task<ProductDTO> Add(ProductDTO entity)
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                Product product = entity.ConvertProductDTO();

                EntityEntry<Product> createdResult = await context.Products.AddAsync(product);
                await context.SaveChangesAsync();

                return createdResult.Entity.ConvertProduct();
            }
        }

        public virtual async Task<ProductDTO> Update(ProductDTO entity, Guid guid)
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                Product product = entity.ConvertProductDTO();

                var original = await context.Products.FirstOrDefaultAsync(e => e.Id == guid);

                foreach (PropertyInfo propertyInfo in original.GetType().GetProperties())
                {
                    if (propertyInfo.GetValue(product, null) == null)
                        propertyInfo.SetValue(product, propertyInfo.GetValue(original, null), null);
                }
                context.Entry(original).CurrentValues.SetValues(product);
                await context.SaveChangesAsync();

                return product.ConvertProduct();
            }
        }

        public virtual async Task<bool> Delete(Guid guid)
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                Product product = await context.Products
                    .FirstOrDefaultAsync(x => x.Id == guid);

                context.Products.Remove(product);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public virtual async Task<bool> SafeDelete(Guid guid)
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                Product product = await context.Products
                    .FirstOrDefaultAsync(x => x.Id == guid);

                product.IsActive = false;
                await context.SaveChangesAsync();

                return true;
            }
        }



        #region Конструкторы

        public ProductDataService()
        {
            _contextFactory = new TIADbContextFactory();
        }

        public ProductDataService(TIADbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #endregion
    }
}
