using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.Core.DTOClasses;

namespace TIA.BusinessLogicBase.Abstractions
{
    public interface ICatalogModel
    {
        Task<CatalogDTO> GetCatalogByIdAsync(Guid id);

        Task<List<ProductDTO>> GetCatalogProductsWithFiltersAsync(Guid id, string title = "", DateTime? minDate = null, DateTime? maxDate = null, uint? minPrice = null, uint? maxPrice = null);

        Task<List<CatalogDTO>> GetCatalogsTreeAsync();

        Task<List<CatalogDTO>> GetCatalogsLineCollectionAsync();

        Task<CatalogDTO> AddCatalogAsync(CatalogDTO catalogDTO);

        Task<CatalogDTO> ChangeCatalogAsync(CatalogDTO catalogDTO);

        Task<bool> DeleteCatalogAsync(Guid id);
    }
}
