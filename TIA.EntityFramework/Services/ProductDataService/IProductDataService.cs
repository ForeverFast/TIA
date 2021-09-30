using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.Core.DTOClasses;
using TIA.Core.StoredProcedureModels;

namespace TIA.EntityFramework.Services
{
    public interface IProductDataService : IDataService<ProductDTO>
    {
        List<ProductDataModel> GetProductsFullData(DateTime? minDate = null, DateTime? maxDate = null, uint? minPrice = null, uint? maxPrice = null);
        Task<bool> SafeDelete(Guid guid);
    }
}
