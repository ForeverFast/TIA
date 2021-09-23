using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.Core.DTOClasses;

namespace TIA.BusinessLogicBase.Abstractions
{
    public interface ICatalogModel
    {
        Task<CatalogDTO> GetCatalogByIdAsync(Guid id);

        Task<List<CatalogDTO>> GetCatalogsTreeAsync();

        Task<List<CatalogDTO>> GetCatalogsLineCollectionAsync();

        Task<CatalogDTO> AddCatalogAsync(CatalogDTO catalogDTO);

        Task<CatalogDTO> ChangeCatalogAsync(CatalogDTO catalogDTO);

        Task<bool> DeleteCatalogAsync(Guid id);
    }
}
