using System;
using System.Collections.Generic;
using TIA.DataAccessLayer.DTOClasses;
using TIA.DataAccessLayer.Models;

namespace TIA.DataAccessLayer.Repositories
{
    public interface IProductRepository : IRepository<ProductDTO>
    {
        List<ProductDataModel> GetProductsFullData(DateTime? minDate = null, DateTime? maxDate = null, uint? minPrice = null, uint? maxPrice = null);
        bool SafeDelete(Guid guid);
    }
}
