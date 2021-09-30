using Microsoft.Data.SqlClient;
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
using TIA.Core.StoredProcedureModels;

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

        public virtual List<ProductDataModel> GetProductsFullData(DateTime? minDate = null, DateTime? maxDate = null, uint? minPrice = null, uint? maxPrice = null)
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                SqlParameter minDateParam = new SqlParameter
                {
                    ParameterName = "minDate",
                    Value = minDate != null ? minDate : DBNull.Value
                };
                SqlParameter maxDateParam = new SqlParameter
                {
                    ParameterName = "maxDate",
                    Value = maxDate != null ? maxDate : DBNull.Value
                };
                SqlParameter minPriceParam = new SqlParameter
                {
                    ParameterName = "minPrice",
                    Value = minPrice != null ? Convert.ToInt32(minPrice) : DBNull.Value
                };
                SqlParameter maxPriceParam = new SqlParameter
                {
                    ParameterName = "maxPrice",
                    Value = maxPrice != null ? Convert.ToInt32(maxPrice) : DBNull.Value
                };

                List<ProductDataModel> productDataModel = context.ProductDataModels.FromSqlRaw("GetProductData @minDate, @maxDate, @minPrice, @maxPrice", minDateParam, maxDateParam, minPriceParam, maxPriceParam).ToList();

                return productDataModel;
            }
        }

        public virtual async Task<ProductDTO> Add(ProductDTO entity)
        {
            using (TIADbContext context = _contextFactory.CreateDbContext(null))
            {
                Product product = entity.ConvertProductDTO();

                if (product.SomeDate == null)
                    product.SomeDate = DateTime.Now;

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
