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

        Task<CatalogDTO> AddCatalogAsync(CatalogDTO catalogDTO);

        Task<CatalogDTO> ChangeCatalogAsync(CatalogDTO catalogDTO);

        Task<bool> DeleteCatalogAsync(CatalogDTO catalogDTO);
    }
}
