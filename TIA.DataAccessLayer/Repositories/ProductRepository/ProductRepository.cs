using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TIA.DataAccessLayer.Converters;
using TIA.DataAccessLayer.DTOClasses;
using TIA.DataAccessLayer.Models;

namespace TIA.DataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TIA_DBContextFactory _contextFactory;

        public ProductDTO GetById(Guid id)
        {
            using (TIA_DBContext context = _contextFactory.CreateDbContext(null))
            {
                Product result = context.Products
                  .FirstOrDefault(c => c.Id == id);

                return result.ConvertProduct();
            }
        }

        public List<ProductDataModel> GetProductsFullData(DateTime? minDate = null, DateTime? maxDate = null, uint? minPrice = null, uint? maxPrice = null)
        {
            using (TIA_DBContext context = _contextFactory.CreateDbContext(null))
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

        public ProductDTO Add(ProductDTO entity)
        {
            using (TIA_DBContext context = _contextFactory.CreateDbContext(null))
            {
                Product product = entity.ConvertProductDTO();

                if (product.SomeDate == null)
                    product.SomeDate = DateTime.Now;

                EntityEntry<Product> createdResult = context.Products.Add(product);
                context.SaveChanges();

                return createdResult.Entity.ConvertProduct();
            }
        }

        public ProductDTO Update(ProductDTO entity, Guid id)
        {
            using (TIA_DBContext context = _contextFactory.CreateDbContext(null))
            {
                Product product = entity.ConvertProductDTO();

                var original = context.Products.FirstOrDefault(e => e.Id == id);

                foreach (PropertyInfo propertyInfo in original.GetType().GetProperties())
                {
                    if (propertyInfo.GetValue(product, null) == null)
                        propertyInfo.SetValue(product, propertyInfo.GetValue(original, null), null);
                }
                context.Entry(original).CurrentValues.SetValues(product);
                context.SaveChanges();

                return product.ConvertProduct();
            }
        }

        public bool Delete(Guid id)
        {
            using (TIA_DBContext context = _contextFactory.CreateDbContext(null))
            {
                Product product = context.Products
                    .FirstOrDefault(x => x.Id == id);

                context.Products.Remove(product);
                context.SaveChanges();

                return true;
            }
        }

        public bool SafeDelete(Guid id)
        {
            using (TIA_DBContext context = _contextFactory.CreateDbContext(null))
            {
               
                Product product = context.Products
                    .FirstOrDefault(x => x.Id == id);

                product.IsActive = false;
                context.SaveChanges();

                return true;
            }
        }

        public ProductRepository()
        {
            this._contextFactory = new TIA_DBContextFactory();
        }

        public ProductRepository(TIA_DBContextFactory context)
        {
            this._contextFactory = context;
        }
    }
}
