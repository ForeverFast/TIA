using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.Core.DTOClasses;

namespace TIA.EntityFramework.Services
{
    public interface ICatalogDataService : IDataService<CatalogDTO>
    {
        List<ProductDTO> GetCatalogProductsWithFilters(Guid id, string title = "", DateTime? minDate = null, DateTime? maxDate = null, uint? minPrice = null, uint? maxPrice = null);

        List<CatalogDTO> GetCatalogsTree();

        List<CatalogDTO> GetCatalogsLineCollection();
    }
}
