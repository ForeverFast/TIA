using System;
using System.Collections.Generic;
using TIA.DataAccessLayer.DTOClasses;

namespace TIA.DataAccessLayer.Repositories
{
    public interface ICatalogRepository : IRepository<CatalogDTO>
    {
        List<ProductDTO> GetCatalogProductsWithFilters(Guid id, string title = "", DateTime? minDate = null, DateTime? maxDate = null, uint? minPrice = null, uint? maxPrice = null);

        List<CatalogDTO> GetCatalogsTree();

        List<CatalogDTO> GetCatalogsLineCollection();
    }
}
